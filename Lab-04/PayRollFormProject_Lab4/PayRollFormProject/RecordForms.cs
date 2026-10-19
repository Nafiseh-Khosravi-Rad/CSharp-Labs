using System.Globalization;

namespace PayRollFormProject;

public class RecordForm : Form
{
    protected readonly PayrollStore Store;
    protected readonly TextBox[] Fields;
    private readonly string file;
    private readonly Label status = new();
    public RecordForm(PayrollStore store, string entity, string fileName, params string[] labels)
    {
        Store = store; file = fileName;
        Text = entity + " Information"; Name = entity + "Form";
        StartPosition = FormStartPosition.CenterParent; AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(650, 130 + labels.Length * 40);
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(18), ColumnCount = 2, RowCount = labels.Length + 2 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38)); panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
        Fields = new TextBox[labels.Length];
        for (int i = 0; i < labels.Length; i++)
        {
            panel.Controls.Add(new Label { Text = labels[i], AutoSize = true, Anchor = AnchorStyles.Left }, 0, i);
            Fields[i] = new TextBox { Name = "Field" + i, Dock = DockStyle.Fill, MaxLength = 150 };
            panel.Controls.Add(Fields[i], 1, i);
        }
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
        var add = new Button { Text = "Add " + entity, Name = "AddRecord", AutoSize = true };
        var search = new Button { Text = "Search " + entity, Name = "SearchRecord", AutoSize = true };
        add.Click += (_, _) => Execute(() => { AddRecord(Fields.Select(f => f.Text.Trim()).ToArray()); status.Text = entity + " added successfully."; });
        search.Click += (_, _) => Execute(() => {
            var row = Store.Find(file, Fields.Length, PayrollStore.PositiveId(Fields[0].Text.Trim()));
            if (row == null) { foreach (var field in Fields.Skip(1)) field.Clear(); status.Text = "No matching " + entity.ToLowerInvariant() + " found."; return; }
            for (int i = 0; i < row.Length; i++) Fields[i].Text = row[i];
            status.Text = entity + " found.";
        });
        buttons.Controls.AddRange([add, new Button { Text = "Delete " + entity, Enabled = false, AutoSize = true }, search,
            new Button { Text = "Update " + entity, Enabled = false, AutoSize = true }]);
        panel.Controls.Add(buttons, 0, labels.Length); panel.SetColumnSpan(buttons, 2);
        status = new Label { Text = "Search by ID. Delete/Update are reserved for a later iteration.", Name = "Status", Dock = DockStyle.Fill, AutoSize = true };
        panel.Controls.Add(status, 0, labels.Length + 1); panel.SetColumnSpan(status, 2);
        Controls.Add(panel);
    }
    protected virtual void AddRecord(string[] row) => Store.Add(file, row);
    protected void Execute(Action action)
    {
        try { action(); }
        catch (Exception ex) when (ex is ArgumentException or IOException or UnauthorizedAccessException or FormatException or OverflowException)
        { status.Text = ex.Message; }
    }
}
public sealed class EmployeeForm : RecordForm
{
    private readonly DataGridView employeeRecords = new()
    {
        Name = "EmployeeRecords",
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        RowHeadersVisible = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false
    };

    public EmployeeForm(PayrollStore s) : base(s, "Employee", "Employee.in", "Employee ID", "Last name", "First name", "Position ID", "SSN (9 digits)", "Hire date (yyyy-MM-dd)", "Department ID", "Qualification ID")
    {
        var inputPanel = Controls.OfType<TableLayoutPanel>().Single();
        Controls.Remove(inputPanel);
        ClientSize = new Size(980, 720);
        MinimumSize = new Size(800, 680);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 450));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(inputPanel, 0, 0);
        layout.Controls.Add(new Label { Text = "Employee.in records (read-only)", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(8, 4, 0, 0) }, 0, 1);
        layout.Controls.Add(employeeRecords, 0, 2);
        Controls.Add(layout);
        foreach (var heading in new[] { "Employee ID", "Last name", "First name", "Position ID", "SSN", "Hire date", "Department ID", "Qualification ID" })
            employeeRecords.Columns.Add(heading, heading);
        Load += (_, _) => Execute(RefreshEmployeeRecords);
    }

    protected override void AddRecord(string[] row)
    {
        Store.AddEmployee(row);
        RefreshEmployeeRecords();
    }

    private void RefreshEmployeeRecords()
    {
        var records = Store.Read("Employee.in", 8);
        employeeRecords.Rows.Clear();
        foreach (var record in records)
            employeeRecords.Rows.Add(record.Cast<object>().ToArray());
    }
}
public sealed class DepartmentForm : RecordForm
{
    private readonly DataGridView departmentRecords = new()
    {
        Name = "DepartmentRecords",
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        RowHeadersVisible = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false
    };

    public DepartmentForm(PayrollStore s) : base(s, "Department", "Department.in", "Department ID", "Department Name", "Location")
    {
        var inputPanel = Controls.OfType<TableLayoutPanel>().Single();
        Controls.Remove(inputPanel);
        ClientSize = new Size(800, 540);
        MinimumSize = new Size(760, 500);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(inputPanel, 0, 0);
        layout.Controls.Add(new Label { Text = "Department.in records (read-only)", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(8, 4, 0, 0) }, 0, 1);
        layout.Controls.Add(departmentRecords, 0, 2);
        Controls.Add(layout);
        foreach (var heading in new[] { "Department ID", "Department Name", "Location" })
            departmentRecords.Columns.Add(heading, heading);
        Load += (_, _) => Execute(RefreshDepartmentRecords);
    }

    protected override void AddRecord(string[] row)
    {
        base.AddRecord(row);
        RefreshDepartmentRecords();
    }

    private void RefreshDepartmentRecords()
    {
        var records = Store.Read("Department.in", 3);
        departmentRecords.Rows.Clear();
        foreach (var record in records)
            departmentRecords.Rows.Add(record.Cast<object>().ToArray());
    }
}
public sealed class PositionForm : RecordForm
{
    private readonly DataGridView positionRecords = new()
    {
        Name = "PositionRecords",
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        RowHeadersVisible = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false
    };

    public PositionForm(PayrollStore s) : base(s, "Position", "Position.in", "Position ID", "Description")
    {
        var inputPanel = Controls.OfType<TableLayoutPanel>().Single();
        Controls.Remove(inputPanel);
        ClientSize = new Size(760, 500);
        MinimumSize = new Size(740, 460);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 210));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(inputPanel, 0, 0);
        layout.Controls.Add(new Label { Text = "Position.in records (read-only)", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(8, 4, 0, 0) }, 0, 1);
        layout.Controls.Add(positionRecords, 0, 2);
        Controls.Add(layout);
        foreach (var heading in new[] { "Position ID", "Description" })
            positionRecords.Columns.Add(heading, heading);
        Load += (_, _) => Execute(RefreshPositionRecords);
    }

    protected override void AddRecord(string[] row)
    {
        base.AddRecord(row);
        RefreshPositionRecords();
    }

    private void RefreshPositionRecords()
    {
        var records = Store.Read("Position.in", 2);
        positionRecords.Rows.Clear();
        foreach (var record in records)
            positionRecords.Rows.Add(record.Cast<object>().ToArray());
    }
}
public sealed class QualificationForm : RecordForm
{
    private readonly DataGridView qualificationRecords = new()
    {
        Name = "QualificationRecords",
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        RowHeadersVisible = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false
    };

    public QualificationForm(PayrollStore s) : base(s, "Qualification", "Qualification.in", "Qualification ID", "Description")
    {
        var inputPanel = Controls.OfType<TableLayoutPanel>().Single();
        Controls.Remove(inputPanel);
        ClientSize = new Size(760, 500);
        MinimumSize = new Size(740, 460);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 210));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(inputPanel, 0, 0);
        layout.Controls.Add(new Label { Text = "Qualification.in records (read-only)", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(8, 4, 0, 0) }, 0, 1);
        layout.Controls.Add(qualificationRecords, 0, 2);
        Controls.Add(layout);
        foreach (var heading in new[] { "Qualification ID", "Description" })
            qualificationRecords.Columns.Add(heading, heading);
        Load += (_, _) => Execute(RefreshQualificationRecords);
    }

    protected override void AddRecord(string[] row)
    {
        base.AddRecord(row);
        RefreshQualificationRecords();
    }

    private void RefreshQualificationRecords()
    {
        var records = Store.Read("Qualification.in", 2);
        qualificationRecords.Rows.Clear();
        foreach (var record in records)
            qualificationRecords.Rows.Add(record.Cast<object>().ToArray());
    }
}
public sealed class ItemsPayForm : RecordForm
{
    private readonly DataGridView itemPayRecords = new()
    {
        Name = "ItemPayRecords",
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        RowHeadersVisible = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false
    };

    public ItemsPayForm(PayrollStore s) : base(s, "ItemPay", "ItemsPay.in", "ItemPay Number", "Title", "Deduction % (H_rate for 100/101)")
    {
        var inputPanel = Controls.OfType<TableLayoutPanel>().Single();
        Controls.Remove(inputPanel);
        ClientSize = new Size(900, 620);
        MinimumSize = new Size(840, 540);
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(inputPanel, 0, 0);
        layout.Controls.Add(new Label { Text = "ItemsPay.in records (read-only)", AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(8, 4, 0, 0) }, 0, 1);
        layout.Controls.Add(itemPayRecords, 0, 2);
        Controls.Add(layout);
        foreach (var heading in new[] { "ItemPay Number", "Title", "Deduction Percentage/Rate" })
            itemPayRecords.Columns.Add(heading, heading);
        itemPayRecords.Columns[1].FillWeight = 180;
        Load += (_, _) => Execute(RefreshItemPayRecords);
    }

    private void RefreshItemPayRecords()
    {
        var records = Store.Read("ItemsPay.in", 3);
        itemPayRecords.Rows.Clear();
        foreach (var record in records)
            itemPayRecords.Rows.Add(record.Cast<object>().ToArray());
    }
    protected override void AddRecord(string[] row)
    {
        int id = PayrollStore.PositiveId(row[0]);
        if (id == 100 || id == 101)
        {
            if (row[2] != "H_rate") throw new ArgumentException("Income/net items use H_rate.");
        }
        else
        {
            if (!decimal.TryParse(row[2], NumberStyles.Number, CultureInfo.CurrentCulture, out var rate) || rate < 0 || rate > 100)
                throw new ArgumentException("Deduction rate must be a percentage from 0 to 100.");
            row[2] = rate.ToString(CultureInfo.InvariantCulture);
        }
        Store.Add("ItemsPay.in", row);
        RefreshItemPayRecords();
    }
}
