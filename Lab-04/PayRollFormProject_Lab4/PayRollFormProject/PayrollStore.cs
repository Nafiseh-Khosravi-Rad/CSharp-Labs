using System.Globalization;

namespace PayRollFormProject;

// Tab-separated records preserve spaces in names and descriptions.
public sealed class PayrollStore
{
    public string DirectoryPath { get; }
    public PayrollStore(string? directory = null)
    {
        DirectoryPath = directory ?? Path.Combine(AppContext.BaseDirectory, "Data");
        Directory.CreateDirectory(DirectoryPath);
    }
    public string FilePath(string file) => Path.Combine(DirectoryPath, file);
    public List<string[]> Read(string file, int columns)
    {
        if (!File.Exists(FilePath(file))) return new();
        var result = new List<string[]>();
        int lineNumber = 0;
        foreach (var line in File.ReadLines(FilePath(file)))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line)) continue;
            var row = line.Split('\t');
            if (row.Length != columns) throw new InvalidDataException($"{file}, line {lineNumber}: expected {columns} tab-separated fields.");
            result.Add(row);
        }
        return result;
    }
    public string[]? Find(string file, int columns, int id) => Read(file, columns).FirstOrDefault(r => r[0] == id.ToString(CultureInfo.InvariantCulture));
    public void Add(string file, string[] row)
    {
        foreach (var field in row) ValidateText(field);
        if (Find(file, row.Length, PositiveId(row[0])) != null) throw new ArgumentException("This ID already exists. Use a different ID.");
        File.AppendAllText(FilePath(file), string.Join('\t', row) + Environment.NewLine);
    }
    public static void ValidateText(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.IndexOfAny(['\t', '\r', '\n']) >= 0)
            throw new ArgumentException("Every field is required and must not contain tabs or line breaks.");
    }
    public static int PositiveId(string value)
    {
        if (!int.TryParse(value, out int id) || id <= 0) throw new ArgumentException("IDs must be positive whole numbers.");
        return id;
    }
    public Employee? FindEmployee(int id)
    {
        var r = Find("Employee.in", 8, id);
        if (r == null) return null;
        return new Employee { EmpId = id, LastName = r[1], FirstName = r[2], PositionId = PositiveId(r[3]), SSN = r[4],
            HireDate = DateTime.ParseExact(r[5], "yyyy-MM-dd", CultureInfo.InvariantCulture), DepartmentId = PositiveId(r[6]), QualificationId = PositiveId(r[7]) };
    }
    public void AddEmployee(string[] r)
    {
        if (r.Length != 8) throw new ArgumentException("Eight employee fields are required.");
        r[4] = r[4].Replace("-", "").Replace(" ", "");
        if (r[4].Length != 9 || !r[4].All(char.IsAsciiDigit)) throw new ArgumentException("SSN must contain exactly nine digits.");
        if (!DateTime.TryParseExact(r[5], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var hireDate) || hireDate.Date > DateTime.Today)
            throw new ArgumentException("Enter a valid hire date on or before today (yyyy-MM-dd).");
        foreach (var reference in new[] { ("Position.in", 2, 3), ("Department.in", 3, 6), ("Qualification.in", 2, 7) })
            if (Find(reference.Item1, reference.Item2, PositiveId(r[reference.Item3])) == null)
                throw new ArgumentException($"The ID in {reference.Item1} does not exist. Add that record first.");
        if (Read("Employee.in", 8).Any(e => e[4] == r[4])) throw new ArgumentException("This SSN already belongs to an employee.");
        Add("Employee.in", r);
    }
}

public sealed record ItemPay(int Number, string Title, decimal Rate);
public sealed record PayDetail(int EmployeeId, int ItemPayNo, decimal Hours, decimal Rate, decimal Amount, int Month, int Year);
public sealed record PayrollResult(decimal Gross, decimal Deductions, decimal Net, List<PayDetail> Details);

public static class PayrollCalculator
{
    public static PayrollResult Calculate(PayrollStore store, int employeeId, decimal hours, decimal rate, int month, int year)
    {
        if (store.FindEmployee(employeeId) == null) throw new ArgumentException("Search for an existing employee first.");
        if (hours <= 0 || hours > 744 || rate <= 0 || rate > 10000 || month < 1 || month > 12 || year < 1900 || year > 9999)
            throw new ArgumentException("Enter positive hours (up to 744), rate (up to 10000), and a valid month/year.");
        var items = store.Read("ItemsPay.in", 3).Select(r => new ItemPay(PayrollStore.PositiveId(r[0]), r[1],
            r[2] == "H_rate" ? 0 : decimal.Parse(r[2], CultureInfo.InvariantCulture))).ToList();
        int[] required = [100, 101, 200, 201, 202, 203, 204, 205];
        if (items.Select(i => i.Number).Distinct().Count() != items.Count || required.Any(n => !items.Any(i => i.Number == n)))
            throw new InvalidDataException("ItemsPay.in must contain unique item IDs and all eight required payroll items.");
        // Only the six Lab 4 deduction items affect payroll. Custom items remain available for Add/Search.
        var deductions = items.Where(i => i.Number >= 200 && i.Number <= 205).ToList();
        if (deductions.Any(i => i.Rate < 0 || i.Rate > 100) || deductions.Sum(i => i.Rate) > 100)
            throw new InvalidDataException("Deduction percentages must total no more than 100%.");
        decimal gross = decimal.Round(hours * rate, 2, MidpointRounding.AwayFromZero);
        var details = new List<PayDetail> { new(employeeId, 100, hours, rate, gross, month, year) };
        foreach (var item in deductions.OrderBy(i => i.Number))
            details.Add(new(employeeId, item.Number, hours, item.Rate, decimal.Round(gross * item.Rate / 100, 2, MidpointRounding.AwayFromZero), month, year));
        decimal total = details.Skip(1).Sum(d => d.Amount);
        details.Add(new(employeeId, 101, hours, rate, gross - total, month, year));
        return new(gross, total, gross - total, details);
    }
    public static void Save(PayrollStore store, PayrollResult result)
    {
        var first = result.Details[0];
        if (store.Read("EmpPayDetail.out", 7).Any(r => r[0] == first.EmployeeId.ToString(CultureInfo.InvariantCulture) &&
            r[5] == first.Month.ToString(CultureInfo.InvariantCulture) && r[6] == first.Year.ToString(CultureInfo.InvariantCulture)))
            throw new ArgumentException("Payroll for this employee and month/year has already been saved.");
        var lines = result.Details.Select(d => string.Join('\t', new[] { d.EmployeeId.ToString(CultureInfo.InvariantCulture), d.ItemPayNo.ToString(CultureInfo.InvariantCulture),
            d.Hours.ToString(CultureInfo.InvariantCulture), d.Rate.ToString(CultureInfo.InvariantCulture), d.Amount.ToString("F2", CultureInfo.InvariantCulture),
            d.Month.ToString(CultureInfo.InvariantCulture), d.Year.ToString(CultureInfo.InvariantCulture) }));
        File.AppendAllText(store.FilePath("EmpPayDetail.out"), string.Join(Environment.NewLine, lines) + Environment.NewLine);
    }
}
