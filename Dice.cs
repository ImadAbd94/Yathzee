using System;

namespace YatzheeExam
{
    class Dice
    {
        // Instance Variables-----------------------------------

       // Genererates random values
        protected Random rnd;

        // Properties 
        private int current;
        public int Current
        {
            get { return current; }
            set { current = value; }
        }
       


        //Constructor--------------------------------------------
        
        public Dice()
        {
            rnd = new Random();
            RollDice();
        }

        // Rolls the dice
        public virtual void RollDice()
        {
            Current = rnd.Next(1, 7);
        }

        // returns value to string
        public override string ToString()
        {
            return Current + " ";
        }

    }
}
