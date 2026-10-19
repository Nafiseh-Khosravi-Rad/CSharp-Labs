using System.Globalization;

namespace PayRollFormProject;

public sealed class PayEmployeeForm : Form
{
    private readonly PayrollStore store;
    private readonly TextBox employeeId = new() { Name = "EmployeeId" };
    private readonly TextBox firstName = new() { Name = "EmployeeFirstName", ReadOnly = true };
    private readonly TextBox lastName = new() { Name = "EmployeeLastName", ReadOnly = true };
    private readonly TextBox ssn = new() { Name = "EmployeeSSN", ReadOnly = true };
    private readonly TextBox hireDate = new() { Name = "EmployeeHireDate", ReadOnly = true };
    private readonly TextBox positionId = new() { Name = "EmployeePositionId", ReadOnly = true };
    private readonly TextBox qualificationId = new() { Name = "EmployeeQualificationId", ReadOnly = true };
    private readonly TextBox departmentId = new() { Name = "EmployeeDepartmentId", ReadOnly = true };
    private readonly NumericUpDown hours = new() { Name = "Hours", DecimalPlaces = 2, Maximum = 744, Value = 160 };
    private readonly NumericUpDown rate = new() { Name = "Rate", DecimalPlaces = 2, Maximum = 10000, Value = 25 };
    private readonly ComboBox month = new() { Name = "Month", DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown year = new() { Name = "Year", Minimum = 1900, Maximum = 9999, Value = DateTime.Today.Year };
    private readonly TextBox totalIncome = new() { Name = "TotalIncome", ReadOnly = true };
    private readonly TextBox totalNet = new() { Name = "TotalNet", ReadOnly = true };
    private readonly Label status = new() { Name = "Status", AutoSize = true, Dock = DockStyle.Fill };
    private Employee? selected;

    public PayEmployeeForm(PayrollStore payrollStore)
    {
        store = payrollStore;
        Text = "Pay Employee"; StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(1080, 500); MinimumSize = new Size(1020, 510); AutoScaleMode = AutoScaleMode.Dpi;
        month.Items.AddRange(Enumerable.Range(1, 12).Cast<object>().ToArray());
        month.SelectedItem = DateTime.Today.Month;
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 2, RowCount = 4 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50)); layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
        var searchPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3 };
        searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180));
        var search = new Button { Name = "SearchEmployee", Text = "Search Employee", AutoSize = true, Dock = DockStyle.Top };
        searchPanel.Controls.Add(new Label { Text = "Employee ID", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        employeeId.Dock = DockStyle.Top; searchPanel.Controls.Add(employeeId, 1, 0); searchPanel.Controls.Add(search, 2, 0);
        layout.Controls.Add(searchPanel, 0, 0); layout.SetColumnSpan(searchPanel, 2);
        TableLayoutPanel FieldsPanel() {
            var p = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(4, 8, 12, 0) };
            p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58)); p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42)); return p;
        }
        void Field(TableLayoutPanel panel, string label, Control control) {
            int row = panel.RowCount++; panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Left, Padding = new Padding(0, 4, 0, 0) }, 0, row);
            control.Dock = DockStyle.Top; panel.Controls.Add(control, 1, row);
        }
        var identity = FieldsPanel(); var payroll = FieldsPanel();
        Field(identity, "Employee First Name", firstName); Field(identity, "Employee Last Name", lastName);
        Field(identity, "Employee SSN", ssn); Field(identity, "Employee Hire Date", hireDate);
        Field(identity, "Employee Position ID", positionId); Field(identity, "Employee Qualification ID", qualificationId);
        Field(identity, "Employee Department ID", departmentId);
        Field(payroll, "Month", month); Field(payroll, "Year", year);
        Field(payroll, "Employee Working Hour", hours); Field(payroll, "Employee Rate Hour", rate);
        Field(payroll, "Total Income", totalIncome); Field(payroll, "Total Net", totalNet);
        layout.Controls.Add(identity, 0, 1); layout.Controls.Add(payroll, 1, 1);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true };
        var calculate = new Button { Name = "Calculate", Text = "Calculate", AutoSize = true };
        var reset = new Button { Name = "Reset", Text = "Reset", AutoSize = true };
        buttons.Controls.AddRange([calculate, reset]); layout.Controls.Add(buttons, 0, 2); layout.SetColumnSpan(buttons, 2);
        layout.Controls.Add(status, 0, 3); layout.SetColumnSpan(status, 2); Controls.Add(layout);
        employeeId.TextChanged += (_, _) => { ClearEmployee(); ClearTotals(); status.Text = "Enter an employee ID and search."; };
        foreach (var input in new[] { hours, rate, year }) input.ValueChanged += (_, _) => ClearTotals();
        month.SelectedIndexChanged += (_, _) => ClearTotals();
        search.Click += (_, _) => Execute(() => {
            ClearEmployee(); ClearTotals();
            selected = store.FindEmployee(PayrollStore.PositiveId(employeeId.Text.Trim()));
            if (selected == null) { status.Text = "Employee not found."; return; }
            firstName.Text = selected.FirstName; lastName.Text = selected.LastName; ssn.Text = selected.SSN;
            hireDate.Text = selected.HireDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            positionId.Text = selected.PositionId.ToString(CultureInfo.InvariantCulture);
            qualificationId.Text = selected.QualificationId.ToString(CultureInfo.InvariantCulture);
            departmentId.Text = selected.DepartmentId.ToString(CultureInfo.InvariantCulture);
            status.Text = "Employee found. Enter hours, rate and pay period.";
        });
        calculate.Click += (_, _) => Execute(() => {
            if (selected == null) throw new ArgumentException("Search for an employee before calculating pay.");
            var pay = PayrollCalculator.Calculate(store, selected.EmpId, hours.Value, rate.Value, (int)month.SelectedItem!, (int)year.Value);
            PayrollCalculator.Save(store, pay);
            totalIncome.Text = pay.Gross.ToString("C2"); totalNet.Text = pay.Net.ToString("C2");
            status.Text = $"Saved payroll for {(int)month.SelectedItem!:00}/{year.Value} to EmpPayDetail.out.";
        });
        reset.Click += (_, _) => {
            employeeId.Clear(); ClearEmployee(); ClearTotals(); hours.Value = 160; rate.Value = 25;
            month.SelectedItem = DateTime.Today.Month; year.Value = DateTime.Today.Year;
            status.Text = "Enter an employee ID and search.";
        };
    }
    private void ClearEmployee()
    {
        selected = null;
        foreach (var field in new[] { firstName, lastName, ssn, hireDate, positionId, qualificationId, departmentId }) field.Clear();
    }
    private void ClearTotals() { totalIncome.Clear(); totalNet.Clear(); }
    private void Execute(Action action)
    {
        try { action(); }
        catch (Exception ex) when (ex is ArgumentException or IOException or UnauthorizedAccessException or FormatException or OverflowException)
        { status.Text = ex.Message; }
    }
}