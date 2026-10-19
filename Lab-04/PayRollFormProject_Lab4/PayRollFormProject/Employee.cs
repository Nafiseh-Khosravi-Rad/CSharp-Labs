namespace PayRollFormProject
{
    public class Employee
    {
        private int emp_id;
        private string emp_fname = "";
        private string emp_lname = "";
        private string emp_position = "";
        private int d_id;
        private int emp_PositionId;
        private int emp_QualId;
        private double number_whr;
        private double h_rate;
        private string emp_ssn = "";
        private DateTime emp_hiredate;

        public string SSN { get => emp_ssn; set => emp_ssn = value; }
        public DateTime HireDate { get => emp_hiredate; set => emp_hiredate = value; }

        public int EmpId
        {
            get { return emp_id; }
            set { emp_id = value; }
        }

        public string FirstName
        {
            get { return emp_fname; }
            set { emp_fname = value; }
        }

        public string LastName
        {
            get { return emp_lname; }
            set { emp_lname = value; }
        }

        public string Position
        {
            get { return emp_position; }
            set { emp_position = value; }
        }

        public int DepartmentId
        {
            get { return d_id; }
            set { d_id = value; }
        }

        public int PositionId
        {
            get { return emp_PositionId; }
            set { emp_PositionId = value; }
        }

        public int QualificationId
        {
            get { return emp_QualId; }
            set { emp_QualId = value; }
        }

        public double WorkedHours
        {
            get { return number_whr; }
            set { number_whr = value; }
        }

        public double HourRate
        {
            get { return h_rate; }
            set { h_rate = value; }
        }

        public double CalculateSalaryTotal()
        {
            return number_whr * h_rate;
        }
    }
}
