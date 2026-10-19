namespace PayRollFormProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double number_whr = Convert.ToDouble(txtWorkedHours.Text);
            double h_rate = Convert.ToDouble(txtHourRate.Text);

            double totalIncome = number_whr * h_rate;

            double prvTax = totalIncome * 0.09;
            double fedTax = totalIncome * 0.07;
            double qpIns = totalIncome * 0.0055;
            double eIns = totalIncome * 0.014;
            double qpp = totalIncome * 0.045;
            double unionDues = totalIncome * 0.0165;

            double totalDeductions =
                prvTax + fedTax + qpIns + eIns + qpp + unionDues;

            double netAmount = totalIncome - totalDeductions;

            PayStubForm payStubForm = new PayStubForm(
                txtEmployeeName.Text,
                txtSSN.Text,
                totalIncome,
                fedTax,
                prvTax,
                qpIns,
                eIns,
                qpp,
                unionDues,
                totalDeductions,
                netAmount
            );

            payStubForm.ShowDialog(this);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Department department = new Department();
            Position position = new Position();
            Qualification qualification = new Qualification();

            department.DepartmentName = txtDepartment.Text;
            position.Description = txtPosition.Text;
            qualification.Description = txtQualification.Text;

            employee.Position = txtPosition.Text;

            employee.FirstName = txtEmployeeName.Text;
            employee.WorkedHours = Convert.ToDouble(txtWorkedHours.Text);
            employee.HourRate = Convert.ToDouble(txtHourRate.Text);

            double totalIncome = employee.CalculateSalaryTotal();

            double prvTax = totalIncome * 0.09;
            double fedTax = totalIncome * 0.07;
            double qpIns = totalIncome * 0.0055;
            double eIns = totalIncome * 0.014;
            double qpp = totalIncome * 0.045;
            double unionDues = totalIncome * 0.0165;

            double deductions = prvTax + fedTax + qpIns + eIns + qpp + unionDues;

            double netAmount = totalIncome - deductions;

            txtTotalIncome.Text = totalIncome.ToString("C2");
            txtNetAmount.Text = netAmount.ToString("C2");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblPosition_Click(object sender, EventArgs e)
        {

        }
    }
}
