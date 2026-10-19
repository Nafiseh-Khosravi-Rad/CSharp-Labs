namespace PayRollFormProject;

public sealed class PayrollHistoryForm : Form
{
    public PayrollHistoryForm(PayrollStore store)
    {
        Text = "All Employee Pay Details"; ClientSize = new Size(960, 560); StartPosition = FormStartPosition.CenterParent;
        var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AllowUserToDeleteRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false };
        foreach (var heading in new[] { "Employee ID", "ItemPay No", "Hours", "Item Rate", "Amount", "Month", "Year" }) grid.Columns.Add(heading, heading);
        try { foreach (var row in store.Read("EmpPayDetail.out", 7)) grid.Rows.Add(row.Cast<object>().ToArray()); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException) { Controls.Add(new Label { Text = ex.Message, Dock = DockStyle.Top, AutoSize = true }); }
        Controls.Add(grid);
    }
}
