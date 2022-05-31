namespace YatzheeExam
{
    class BiasDice : Dice
    {
        // Instance Variables----------------------
        public bool IsBetterDice { get; set; }

        public int BiasDegree { get; set; }
        //-----------------------------------------


        // Constructors
        public BiasDice(bool bias, int degree)
        {
            IsBetterDice = bias;
            BiasDegree = degree;
        }
        //-----------------------------------------


        // Override the roll from the base class---
        public override void RollDice()
        {
            Current = rnd.Next(1, 7);
            int biasval = BiasDegree;

            while (biasval > 0)
            {
                if (IsBetterDice && Current < 4)
                {
                    Current = rnd.Next(1, 7);
                }

                else if (!IsBetterDice && Current >= 4)
                {
                    Current = rnd.Next(1, 7);
                }
                biasval--;
            }
        }
    }   //------------------------------------------
}
