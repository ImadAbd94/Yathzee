using YatzheeExam.Exceptions;

namespace YatzheeExam
{
    class Options
    {
        private int optionNum;
        public int OptionNum
        {
            get { return optionNum; }
            set { optionNum = value; }
        }

        private int score;
        public int Score { get { return score; } }

        private readonly int dVal1;
        public int DVal1
        {
            get { return dVal1; }
        }

        private readonly int dVal2;
        public int DVal2
        {
            get { return dVal2; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (value == "Ones" || value == "Twos" || value == "Threes" || value == "Fours" || value == "Fives" || value == "Sixes" ||

                    value == "P" || value == "TP" || value == "T" || value == "F" || value == "SS" || value == "LS" || value == "FH" || value == "C" || value == "Y")
                {
                    type = value;
                }
             
            }
        }

        public void ZeroPoints()
        {
            score = 0;
        }

        public Options(string type)
        {
            switch (type)
            {
                case "SS":
                    Type = type;
                    score = 15;
                    break;

                case "LS":
                    Type = type;
                    score = 20;
                    break;

                case "Y":
                    Type = type;
                    score = 50;
                    break;

                default:
                    Type = type;
                    score = 0;
                    break;
            }
        }

        public Options(int Score, string Type)
        {
            if(type == "C")
            {
                score = Score;
                type = Type;
            }
        }

        public Options(int Score, int DVal1, string Type)
        {
            dVal1 = DVal1;
            score = Score;
            type = Type;
        }

        public Options(int Score, int DVal1, int DVal2, string Type)
        {
            dVal1 = DVal1;
            dVal2 = DVal2;
            type = Type;
            score = Score;
        }

    }

}
