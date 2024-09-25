namespace Data
{
    public static class Score
    {
        private static int _environment = 15;
        private static int _society = 15;
        private static int _technology = 15;
        private static int _economy = 15;
        
        public static int Environment
        {
            get => _environment;
            set
            {
                if (value is >= 0 and <= 30)
                {
                    _environment = value;
                }
            }
        }

        public static int Society
        {
            get => _society;
            set
            {
                if (value is >= 0 and <= 30)
                {
                    _society = value;
                }
            }
        }

        public static int Technology
        {
            get => _technology;
            set
            {
                if (value is >= 0 and <= 30)
                {
                    _technology = value;
                }
            }
        }

        public static int Economy
        {
            get => _economy;
            set
            {
                if (value is >= 0 and <= 30)
                {
                    _economy = value;
                }
            }
        }
    }
}