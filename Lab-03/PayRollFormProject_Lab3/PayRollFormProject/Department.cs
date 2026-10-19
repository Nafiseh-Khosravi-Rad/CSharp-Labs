namespace PayRollFormProject
{
    public class Department
    {
        private int d_id;
        private string d_name = "";
        private string d_location = "";

        public int DepartmentId
        {
            get { return d_id; }
            set { d_id = value; }
        }

        public string DepartmentName
        {
            get { return d_name; }
            set { d_name = value; }
        }

        public string DepartmentLocation
        {
            get { return d_location; }
            set { d_location = value; }
        }
    }
}