namespace PayRollFormProject
{
    public class Position
    {
        private int PositionId;
        private string PosDesc = "";

        public int Id
        {
            get { return PositionId; }
            set { PositionId = value; }
        }

        public string Description
        {
            get { return PosDesc; }
            set { PosDesc = value; }
        }
    }
}