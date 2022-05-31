using System;
using System.Collections.Generic;

namespace YatzheeExam
{
    
    class OutputDisplay
    {
        public OutputDisplay()
        {

        }

        public void IntroductionDisplay()
        {

            Console.WriteLine("This game is developed by Imad H. Abdallah (studynr:20186335)");
            Console.WriteLine();
            Console.WriteLine("WELCOME TO MY YATHZEE GAME");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("To Start the Game Type: ---------------------------> 'start'");
            Console.WriteLine("To manipulate the ammount of ReRolls Type: --------> 'rerolls [number of wish]'");
            Console.WriteLine();
            Console.WriteLine("Start the game and read the instruction manual to proceed");
            Console.WriteLine();
            Console.WriteLine("                     Good luck & Have Fun  ");
        }

        public void CommandListDisplay()
        {
            Console.Clear();

            Console.WriteLine("---------------COMMAND LIST---------------");
            Console.WriteLine();
            Console.WriteLine("To see the score Type: .............................> 'score'");
            Console.WriteLine("To bias the Dice Type: .............................> 'diceswitch'");
            Console.WriteLine("To manipulate the dice roll ammount Type: ..........> 'rerolls [number of wish]");
            Console.WriteLine();
            Console.WriteLine("---------------InGame Commands------------");
            Console.WriteLine();
            Console.WriteLine("Hold the spefic dices before re rolling Type .......> 'hold [dice number/numbers with space in between]'");
            Console.WriteLine();
            Console.WriteLine("Roll Dices Type: ...................................> 'roll'");
            Console.WriteLine();
            Console.WriteLine("Chosing an option for the roll");
            Console.WriteLine("Example, (you wanna choose opt. 2) Type: ...........> '2'");
            Console.WriteLine();
            Console.WriteLine("If you wish to use the first section");
            Console.WriteLine("you can add points to the specific placeholder");
            Console.WriteLine("Example, (add points to Ones) type:.................> 'ones'");
            Console.WriteLine();
            Console.WriteLine("Use Chance Type: ...................................> 'chance'");
            Console.WriteLine("For 0 points type: .................................> 'zero'");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("To return to the game Type: 'a random key'");
        }

        public void StaticInformationDisplay(int turnNumber, int rollNumber)
        {
            Console.Clear();

            Console.WriteLine(" _____________________________________________ ");
            Console.WriteLine("|Turn number: " + turnNumber + "                               |");
            Console.WriteLine("|Rolls left: " + rollNumber + "                                |");
            Console.WriteLine("|------------------commands-------------------|");
            Console.WriteLine("|Score............................'score'     |");
            Console.WriteLine("|Full command list................'help'      |");
            Console.WriteLine("|_____________________________________________|");
            Console.WriteLine();

        }

        public void DiceRollDisplay(int[] values)
        {
            if (values.Length == 6)
            {
                Console.WriteLine("# 1-----# 2-----# 3-----# 4-----# 5-----# 6");
                Console.WriteLine("---     ---     ---     ---     ---     ---");
                Console.WriteLine(" " + values[0] + "      " + values[1] + "       " + values[2] + "       " + values[3] + "       " + values[4] + "       " + values[5]);
                Console.WriteLine("---     ---     ---     ---     ---     ---");
            }
        }

        public void BiasDiceRegDisplay()
        {
            Console.Clear();

            Console.WriteLine("You can manipulate the dice outcomes, either positivly or negativly (high numbers/low numbers)");
            Console.WriteLine("______________________________________________________________________________________________");
            Console.WriteLine("Bias the dice positivly Type:------------------------------->'pos'");
            Console.WriteLine("Bias the dice negativly Type:------------------------------->'neg'");
        }

        public void BiasDiceRegDegreeDisplay()
        {
            Console.Clear();

            Console.WriteLine("You can regulate the value output range, in the dependence of which direction you've biased the dice.");
            Console.WriteLine();
            Console.WriteLine("Insert a value between 1-5");

        }

        public void ScoreBoardDisplay(string[] score, string totalSec1, string scoreBonus, int totalScore) 
        {
            Console.Clear();

            Console.WriteLine("---------------SCOREBOARD---------------");
            Console.WriteLine();
            Console.WriteLine("Section 1:");
            Console.WriteLine("Ones:                     " + score[0]);
            Console.WriteLine("Twos:                     " + score[1]);
            Console.WriteLine("Threes:                   " + score[2]);
            Console.WriteLine("Fours:                    " + score[3]);
            Console.WriteLine("Fives:                    " + score[4]);
            Console.WriteLine("Sixes:                    " + score[5]);
            Console.WriteLine();
            Console.WriteLine("TOTAL:                    " + totalSec1);
            Console.WriteLine();
            Console.WriteLine("Bonus:                    " + scoreBonus);
            Console.WriteLine("________________________________________");
            Console.WriteLine();
            Console.WriteLine("Section 2:");
            Console.WriteLine("One Pair:                 " + score[6]);
            Console.WriteLine("Two Pairs:                " + score[7]);
            Console.WriteLine("Three Pairs:              " + score[8]);
            Console.WriteLine("Four of a Kind:           " + score[9]);
            Console.WriteLine("Small Straight:           " + score[10]);
            Console.WriteLine("Large Straight:           " + score[11]);
            Console.WriteLine("Full House:               " + score[12]);
            Console.WriteLine("Chance:                   " + score[13]);
            Console.WriteLine("YATHZEE:                  " + score[14]);
            Console.WriteLine();
            Console.WriteLine("TOTAL SCORE:               " + totalScore);

        }

        public void GameEndDisplay()
        {
            Console.Clear();
            Console.WriteLine("Congratzzz - You Won");
            Console.WriteLine();
            Console.WriteLine("To see final score, press any key");
            Console.ReadLine();
        }

        public void PairDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have a pair of: " + options.DVal1);
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void TwoPairDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You hace Two Pairs: - A Pair of |" + options.DVal1 + "|  and a pair of |" + options.DVal2 + "|");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void ThreeAKindDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have Three of a kind of |" + options.DVal1 +"|");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void FourAKindDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have Four of a kind of |" + options.DVal1 +"|");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void SmallStrDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have Small Straight");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void LargeStrDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have Large Straight");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void FullHDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You Have Full House: Of 3 |" +options.DVal1 + "|  and 2 of |" + options.DVal2 + "|");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public void YathzeeDisplay(Options options)
        {
            Console.WriteLine("Option: " + options.OptionNum);
            Console.WriteLine("You have Yathzee");
            Console.WriteLine("Points to add: " + options.Score);
            Console.WriteLine("----------------------------------");
        }

        public bool ConfirmChanceDisplay(int value)
        {
            string input;
            Console.WriteLine("By using Chance, you will add " + value + " points to the section");
            Console.WriteLine("Confirm by typing : 'confirm'");
            input = Console.ReadLine();

            if (input.ToLower() == "confirm")
            {
                return true;
            }
            else return false;
        }

        public void OutOfRollsExceptionDisplay()
        {
            Console.WriteLine(" You're out of rolls. Select an option");
            Console.ReadLine();
        }

        public void OptionsToRemoveDisplay(List<Options> options)
        {
            Console.Clear();
            Console.WriteLine("Use an option to add 0 points to:");
            Console.WriteLine();
            
            foreach (Options option in options)
            {
                string typeName = "";
                switch (option.Type)
                {
                    case "Ones":
                        typeName = " Section 1: Ones ";
                        break;

                    case "Twos":
                        typeName = " Section 1: Twos ";
                        break;

                    case "Threes":
                        typeName = " Section 1: Threes ";
                        break;

                    case "Fours":
                        typeName = " Section 1: Fours ";
                        break;

                    case "Fives":
                        typeName = " Section 1: Fives ";
                        break;

                    case "Sixes":
                        typeName = " Section 1: Sixes ";
                        break;


                    case "P":
                        typeName = " Section 2: Pairs ";
                        break;

                    case "TP":
                        typeName = " Section 2: Two Pairs ";
                        break;

                    case "T":
                        typeName = " Section 2: Three of a kind ";
                        break;

                    case "F":
                        typeName = " Section 2: Four of a kind ";
                        break;

                    case "SS":
                        typeName = " Section 2: Small Straight ";
                        break;

                    case "LS":
                        typeName = " Section 2: Large Straight ";
                        break;

                    case "FH":
                        typeName = " Section 2: Full House ";
                        break;

                    case "C":
                        typeName = " Section 2: Chance ";
                        break;

                    case "Y":
                        typeName = " Section 2: Yathzee ";
                        break;
                }

                Console.WriteLine(option.OptionNum + ":" +typeName);
            }

        }

        public void ChanceIsUsedDisplay()
        {
            Console.WriteLine("Chance is already filled");
            Console.ReadLine();
        }

        public void Section1InvalidDisplay(string s)
        {
            Console.WriteLine(s + " is not available for use");
            Console.ReadLine();
        }
    }
}
