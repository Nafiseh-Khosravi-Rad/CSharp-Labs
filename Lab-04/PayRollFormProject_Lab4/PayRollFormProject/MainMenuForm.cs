namespace PayRollFormProject;

public sealed class MainMenuForm : Form
{
    public MainMenuForm()
    {
        Text = "Payroll System — Lab 4"; StartPosition = FormStartPosition.CenterScreen; ClientSize = new Size(820, 480);
        var store = new PayrollStore();
        var menu = new MenuStrip();
        var input = new ToolStripMenuItem("Input Data");
        Add(input, "Employee", () => new EmployeeForm(store));
        Add(input, "Department", () => new DepartmentForm(store));
        Add(input, "Position", () => new PositionForm(store));
        Add(input, "Qualification", () => new QualificationForm(store));
        Add(input, "ItemsPay", () => new ItemsPayForm(store));
        var payroll = new ToolStripMenuItem("Output Payroll");
        Add(payroll, "Output PayStub", () => new PayEmployeeForm(store));
        Add(payroll, "List All Employee PayStub", () => new PayrollHistoryForm(store));
        var exit = new ToolStripMenuItem("Exit"); exit.Click += (_, _) => Close();
        menu.Items.AddRange([input, payroll, exit]); MainMenuStrip = menu;
        Controls.Add(new Label { Text = "Welcome to the Payroll Department\n\nUse Input Data to add or search records.\nUse Output Payroll → Output PayStub to calculate and save monthly pay.\n\nData folder:\n" + store.DirectoryPath,
            Dock = DockStyle.Fill, Padding = new Padding(30, 65, 30, 30), Font = new Font("Segoe UI", 13) });
        Controls.Add(menu);
    }
    private void Add(ToolStripMenuItem parent, string text, Func<Form> create)
    {
        var item = new ToolStripMenuItem(text); item.Click += (_, _) => { using var form = create(); form.ShowDialog(this); }; parent.DropDownItems.Add(item);
    }
}
