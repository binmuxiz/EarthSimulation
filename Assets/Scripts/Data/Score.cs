namespace Data
{
    public class Score
    {
        private int environment = 20;
        private int society = 20;
        private int technology = 20;
        private int economy = 20;

        public int Environment
        {
            get => environment;
            set => environment = value;
        }

        public int Society
        {
            get => society;
            set => society = value;
        }

        public int Technology
        {
            get => technology;
            set => technology = value;
        }

        public int Economy
        {
            get => economy;
            set => economy = value;
        }
    }
}