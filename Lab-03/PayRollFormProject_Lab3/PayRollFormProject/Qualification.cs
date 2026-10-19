namespace PayRollFormProject
{
    public class Qualification
    {
        private int QualId;
        private string QualDesc = "";

        public int Id
        {
            get { return QualId; }
            set { QualId = value; }
        }

        public string Description
        {
            get { return QualDesc; }
            set { QualDesc = value; }
        }
    }
}