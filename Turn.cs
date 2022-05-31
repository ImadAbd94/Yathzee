

namespace YatzheeExam
{
    class Turn
    {
        private static int turnCounter;

        private int turnNum;
        public int TurnNumber { get { return turnNum; } }

        public Options Options { get;  }

        public  Turn(Options op)
        {
            Options = op;

            turnCounter++;
            turnNum = turnCounter;
        }

    }
}
