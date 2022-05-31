// This Program is developed by Imad H. Abdallah - Studynumber : 20186335.
// 
// 
// The overall structure:
//      The game take use in 7 classes, which each have an impact on the whole. 
//      Each class has an aspect to manage;
//          - "Program" Works as the entrypoint for the execution through the Main.
//          - "OutputDisplay" manages the interaction, return to the user in textual form. 
//          - "Options" contain the different options and makes sure that the option will be attached to a valid choice
//          - "Dice" and "BiasDice" manage the rolls for the game, and biasdice inherits from Dice.
//          - "Turn" manages the rounds, which also updates the avialibility for the options.
//          - "Game" is the core of the game with 2 different constructors, one is for a default game and the other for a biased one.
//          - "Exception" manages the problems that arrises.
//
//          


// namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using YatzheeExam.Exceptions;


namespace YatzheeExam
{

    class Game
    {    // InstanceVariables -------------------------------------------------------------------------------
        private readonly Dice[] gameDice = new Dice[6];

        private readonly List<Turn> turns = new List<Turn>();

        private bool isBiased;

        private int reRolls;

        private bool isRunning;

        private bool turnActive;

        private int reRollCount;

        private readonly OutputDisplay outputDisplay;

        // Game Constructors--------------------------------------------------------------------------------

        //default
        public Game(OutputDisplay outDisplay)
        {
            outputDisplay = outDisplay;
            reRolls = 2;
            isBiased = false;
            MakeDices(false, 0);
        }

        //manipulated
        public Game(OutputDisplay outDisplay, int reRollCount, bool isBetter, bool biased, int biasDegree)
        {
            outputDisplay = outDisplay;
            reRolls = reRollCount;
            isBiased = biased;
            MakeDices(isBetter, biasDegree);
        }


        //Game Control--------------------------------------------------------------------------------------
        public void Start()
        {
            isRunning = true;
            int[] values = new int[6];
            int turn = 1;

            // dowhile loop for the rounds
            do
            {
                reRollCount = reRolls;
                turnActive = true;

                RollAll();

                do
                {

                    for (int i = 0; i < 6; i++)
                    {
                        values[i] = gameDice[i].Current;
                    }

                    outputDisplay.StaticInformationDisplay(turn, reRollCount);

                    outputDisplay.DiceRollDisplay(values);

                    _ = new List<Options>();

                    List<Options> options = ResultCheck();

                    options = OptionListUpdater(options, turns);

                    ResultDisplay(options);

                    _ = ConvertScoreBoardToString();


                    try
                    {
                        InputHandeling(options);
                    }

                    catch (OutOfRollsException)
                    {
                        outputDisplay.OutOfRollsExceptionDisplay();
                    }


                }
                while (turnActive);
                turn++;

                if (turn > 15) isRunning = false;

            }
            while (isRunning);
            outputDisplay.GameEndDisplay();
            Console.ReadLine();
            CreateScoreBoard();
        }



        //sets rerolls
        public void ReRollSetting(int x)
        {
            reRolls = x;
        }


        //setup game function.
        public void SetUp()
        {
            bool isRunning = false;
            while (!isRunning)
            {
                string input = Console.ReadLine();
                string[] command = input.Split(' ');

                if (input.ToLower() == "start")
                {
                    Start();
                }

                else if (command.Length > 1)
                {
                    if (command[0].ToLower() == "rerolls")
                    {
                        bool reroll = int.TryParse(command[1], out int rerollnumber);
                        if (reroll)
                        {
                            ReRollSetting(rerollnumber);
                        }
                    }
                }
            }
        }


        // Rollall function
        private void RollAll()
        {
            foreach (Dice ds in gameDice)
            {
                ds.RollDice();
            }

            SortDices();
        }


        // ascends the dice array 
        private void SortDices()
        {
            Dice[] temp = new Dice[6];
            int i = 0;

            foreach (Dice d in gameDice)
            {
                temp[i] = d;
                i++;
            }

            temp = temp.OrderBy(a => (a.Current)).ToArray();
            i = 0;

            foreach (Dice d in temp)
            {
                gameDice[i] = temp[i];
                i++;
            }

        }

        // Initialize dices / called through constructor 
        private void MakeDices(bool isBetter, int biasDegree)
        {
            if (isBiased)
            {
                for (int i = 0; i < 6; i++)
                {
                    gameDice[i] = new BiasDice(isBetter, biasDegree);
                }
            }

            else
            {
                for (int i = 0; i < 6; i++)
                {
                    gameDice[i] = new Dice();
                }
            }

        }


        // Regulated the dice to default, from biased
        private void RegDiceChange()
        {

            _ = new Dice[6];
            int[] tVal = new int[6];

            for (int i = 0; i < 6; i++)
            {
                tVal[i] = gameDice[i].Current;
                gameDice[i] = new Dice
                {
                    Current = tVal[i]
                };
            }
        }

        // Change all the dices to biased.
        private void BiasDiceChange(bool isBetter, int biasDegree)
        {
            int[] tVal = new int[6];

            for (int i = 0; i < 6; i++)
            {
                tVal[i] = gameDice[i].Current;
                gameDice[i] = new BiasDice(isBetter, biasDegree)
                {
                    Current = tVal[i]
                };
            }
        }


        // Return functions ----------------------------------------------------------------------------------

        //Returns the sum of the dices (chance value)
        private int TotalValue()
        {
            int tVal = 0;

            foreach (Dice d in gameDice)
            {
                tVal += d.Current;
            }

            return tVal;
        }

        // returns an array with strings and "zero" input
        private string[] ConvertScoreBoardToString()
        {
            string[] scores = new string[15];

            for (int i = 0; i < 15; i++)
            {
                scores[i] = "   ";
            }

            foreach (Turn t in turns)
            {
                switch (t.Options.Type)
                {
                    case "Ones":
                        if (t.Options.Score != 0)
                        {
                            scores[0] = t.Options.Score.ToString();
                            if (scores[0].Length == 1)
                                scores[0] = " " + scores[0];

                            if (scores[0].Length == 2)
                                scores[0] = " " + scores[0];
                        }
                        else
                            scores[0] = "  0";
                        break;


                    case "Twos":
                        if (t.Options.Score != 0)
                        {
                            scores[1] = t.Options.Score.ToString();
                            if (scores[1].Length == 1)
                                scores[1] = " " + scores[1];

                            if (scores[1].Length == 2)
                                scores[1] = " " + scores[1];
                        }
                        else
                            scores[1] = "  0";
                        break;

                    case "Threes":
                        if (t.Options.Score != 0)
                        {
                            scores[2] = t.Options.Score.ToString();
                            if (scores[2].Length == 1)
                                scores[2] = " " + scores[2];

                            if (scores[2].Length == 2)
                                scores[2] = " " + scores[2];
                        }
                        else
                            scores[2] = "  0";
                        break;

                    case "Fours":
                        if (t.Options.Score != 0)
                        {
                            scores[3] = t.Options.Score.ToString();
                            if (scores[3].Length == 1)
                                scores[3] = " " + scores[3];

                            if (scores[3].Length == 2)
                                scores[3] = " " + scores[3];
                        }
                        else
                            scores[3] = "  0";
                        break;

                    case "Fives":
                        if (t.Options.Score != 0)
                        {
                            scores[4] = t.Options.Score.ToString();
                            if (scores[4].Length == 1)
                                scores[4] = " " + scores[4];

                            if (scores[4].Length == 2)
                                scores[4] = " " + scores[4];
                        }
                        else
                            scores[4] = "  0";
                        break;

                    case "Sixes":
                        if (t.Options.Score != 0)
                        {
                            scores[5] = t.Options.Score.ToString();
                            if (scores[5].Length == 1)
                                scores[5] = " " + scores[5];

                            if (scores[5].Length == 2)
                                scores[5] = " " + scores[5];
                        }
                        else
                            scores[5] = "  0";
                        break;



                    case "P":
                        if (t.Options.Score != 0)
                        {
                            scores[6] = t.Options.Score.ToString();
                            if (scores[6].Length == 1)
                                scores[6] = " " + scores[6];

                            if (scores[6].Length == 2)
                                scores[6] = " " + scores[6];
                        }
                        else
                            scores[6] = "  0";
                        break;


                    case "TP":
                        if (t.Options.Score != 0)
                        {
                            scores[7] = t.Options.Score.ToString();
                            if (scores[7].Length == 1)
                                scores[7] = " " + scores[7];

                            if (scores[7].Length == 2)
                                scores[7] = " " + scores[7];
                        }
                        else
                            scores[7] = "  0";
                        break;


                    case "T":
                        if (t.Options.Score != 0)
                        {
                            scores[8] = t.Options.Score.ToString();
                            if (scores[8].Length == 1)
                                scores[8] = " " + scores[8];

                            if (scores[8].Length == 2)
                                scores[8] = " " + scores[8];
                        }
                        else
                            scores[8] = "  0";
                        break;


                    case "F":
                        if (t.Options.Score != 0)
                        {
                            scores[9] = t.Options.Score.ToString();
                            if (scores[9].Length == 1)
                                scores[9] = " " + scores[9];

                            if (scores[9].Length == 2)
                                scores[9] = " " + scores[9];
                        }
                        else
                            scores[9] = "  0";
                        break;


                    case "SS":
                        if (t.Options.Score != 0)
                        {
                            scores[0] = t.Options.Score.ToString();
                            if (scores[10].Length == 1)
                                scores[10] = " " + scores[10];

                            if (scores[10].Length == 2)
                                scores[10] = " " + scores[10];
                        }
                        else
                            scores[10] = "  0";
                        break;


                    case "LS":
                        if (t.Options.Score != 0)
                        {
                            scores[11] = t.Options.Score.ToString();
                            if (scores[11].Length == 1)
                                scores[11] = " " + scores[11];

                            if (scores[11].Length == 2)
                                scores[11] = " " + scores[11];
                        }
                        else
                            scores[11] = "  0";
                        break;


                    case "FH":
                        if (t.Options.Score != 0)
                        {
                            scores[12] = t.Options.Score.ToString();
                            if (scores[12].Length == 1)
                                scores[12] = " " + scores[12];

                            if (scores[12].Length == 2)
                                scores[12] = " " + scores[12];
                        }
                        else
                            scores[12] = "  0";
                        break;


                    case "C":
                        if (t.Options.Score != 0)
                        {
                            scores[13] = t.Options.Score.ToString();
                            if (scores[13].Length == 1)
                                scores[13] = " " + scores[13];

                            if (scores[13].Length == 2)
                                scores[13] = " " + scores[13];
                        }
                        else
                            scores[13] = "  0";
                        break;


                    case "Y":
                        if (t.Options.Score != 0)
                        {
                            scores[14] = t.Options.Score.ToString();
                            if (scores[14].Length == 1)
                                scores[14] = " " + scores[14];

                            if (scores[14].Length == 2)
                                scores[14] = " " + scores[14];
                        }
                        else
                            scores[14] = "  0";
                        break;
                }
            }

            return scores;
        }


        // returns a list of all available options
        private List<Options> FullOptionList()
        {
            List<Options> resList = new List<Options>();

            Options op0 = new Options("Ones"); resList.Add(op0);
            Options op1 = new Options("Twos"); resList.Add(op1);
            Options op2 = new Options("Threes"); resList.Add(op2);
            Options op3 = new Options("Fours"); resList.Add(op3);
            Options op4 = new Options("Fives"); resList.Add(op4);
            Options op5 = new Options("Sixes"); resList.Add(op5);
            Options op6 = new Options("P"); resList.Add(op6);
            Options op7 = new Options("TP"); resList.Add(op7);
            Options op8 = new Options("T"); resList.Add(op8);
            Options op9 = new Options("F"); resList.Add(op9);
            Options op10 = new Options("SS"); resList.Add(op10);
            Options op11 = new Options("LS"); resList.Add(op11);
            Options op12 = new Options("FH"); resList.Add(op12);
            Options op13 = new Options("C"); resList.Add(op13);
            Options op14 = new Options("Y"); resList.Add(op14);

            return resList;

        }

        // returns a list of options not used 
        private List<Options> OptionRemainingList()
        {
            List<Options> resList = FullOptionList();

            foreach (Turn t in turns)
            {
                resList.RemoveAll(p => p.Type == t.Options.Type);
            }

            return resList;
        }

        // 
        private List<Options> OptionNumSet(List<Options> options)
        {
            int i = 0;

            foreach (Options o in options)
            {
                options.ElementAt(i).OptionNum = i + 1;
                i++;
            }

            return options;
        }


        // Calculates the score of Section 1
        private int Section1Calc(string[] score)
        {
            int totalSec1 = 0;

            for (int i = 0; i < 6; i++)
            {
                if (int.TryParse(score[i], out int r))
                {
                    totalSec1 += r;
                }
            }

            return totalSec1;
        }

        // Determine the bonus for section 1.
        private bool BonusCheck(int totalSec1)
        {
            return totalSec1 >= 63;
        }

        // Calculates the total score
        private int TotalScoreCalc(string[] score, int bonus)
        {
            int tVal = 0;

            foreach (string Score in score)
            {
                if (int.TryParse(Score, out int result))
                {
                    tVal += result;
                }
            }

            return tVal + bonus;

        }

        // Calculates the score of values 
        private int Section1ChoiceCalc(int i)
        {
            int tVal = 0;

            foreach (Dice d in gameDice)
            {
                if (d.Current == i)
                {
                    tVal += d.Current;
                }
            }

            return tVal;

        }

        // Checks if section 1 choice is available or chance
        private bool SelectionAvailability(string s)
        {
            foreach (Turn t in turns)
            {
                if (t.Options.Type == s)
                {
                    return false;
                }
            }
            return true;
        }

        // Option functions ------------------------------------------------------------------------------

        // Checks all results, called after every roll
        private List<Options> ResultCheck()
        {
            int[] res = new int[7];

            var options = new List<Options>();

            int pair = 0;
            int threepair = 0;

            bool SsPos = true;
            bool LsPos = true;

            foreach (Dice d in gameDice)
            {
                res[d.Current]++;
            }

            for (int i = 1; i < 7; i++)
            {

                if (res[i] >= 3 && pair != 0)
                {
                    int score = i * 3 + pair * 2;
                    Options op = new Options(score, i, pair, "FH");
                    options.Add(op);
                }

                if (res[i] == 2 && +threepair != 0)
                {
                    int score = i * 2 + threepair * 3;
                    Options op = new Options(score, threepair, i, "FH");
                    options.Add(op);
                }

                if (pair != 0 && res[i] >= 2)
                {
                    int score = i * 2 + pair * 2;
                    Options op = new Options(score, pair, i, "TP");
                    options.Add(op);
                }

                if (res[i] >= 2)
                {
                    if (pair == 0) pair = i;
                    else _ = i;

                    int score = i * 2;
                    Options op = new Options(score, i, "P");
                    options.Add(op);
                }

                if (res[i] >= 3)
                {
                    threepair = i;
                    int score = i * 3;
                    Options op = new Options(score, i, "T");
                    options.Add(op);
                }

                if (res[i] >= 4)
                {
                    int score = i * 4;
                    Options op = new Options(score, i, "F");
                    options.Add(op);
                }

                if (res[i] == 6)
                {
                    Options op = new Options("Y");
                    options.Add(op);
                }


                if (SsPos && res[i] == 0 && i != 6)
                {
                    SsPos = false;
                }

                if (LsPos && res[i] == 0 && i != 1)
                {
                    LsPos = false;
                }
            }

            if (SsPos)
            {
                Options op = new Options("SS");
                options.Add(op);
            }

            if (LsPos)
            {
                Options op = new Options("LS");
                options.Add(op);
            }

            return options;
        }

        // sorts and updates the available option list 
        private List<Options> OptionListUpdater(List<Options> options, List<Turn> turns)
        {
            foreach (Turn t in turns)
            {
                options.RemoveAll(p => p.Type == t.Options.Type);
            }

            List<Options> optionSorter = options.OrderByDescending(o => o.Score).ToList();

            optionSorter = OptionNumSet(optionSorter);

            return optionSorter;
        }


        // Command Handeling -----------------------------------------------------------------------------


        private void InputHandeling(List<Options> options)
        {
            string input = Console.ReadLine();

            string[] command = input.Split(' ');

            if (command.Length > 1) RollInputHandeling(command);

            else if (input.ToLower() == "help")
            {
                outputDisplay.CommandListDisplay();
                Console.ReadLine();
            }

            else if (input.ToLower() == "roll") FullRerollHandeling();

            else if (input.ToLower() == "diceswitch") CheatInputHandeling();

            else if (input.ToLower() == "zero") NoInputHandeling();

            else if (input.ToLower() == "score") CreateScoreBoard();

            else if (input.ToLower() == "chance")
            {
                if (SelectionAvailability("C"))
                {
                    Options option = new Options(TotalValue(), "C");
                    SelectChance(option);
                }

                else outputDisplay.ChanceIsUsedDisplay();

            }

            else if (int.TryParse(input, out int i)) OptionInputHandeling(options, i);

            else { SectionInputHandeling(input); }

        }

        // Handles the input of rolls
        private void RollInputHandeling(string[] command)
        {
            string input = command[0];
            List<int> intcommand = new List<int>();
            command = command.Skip(1).ToArray();
            int count = 0;

            if (command.Length < 7)
            {
                foreach (string s in command)
                {
                    if (int.TryParse(s, out int i) && i > 6 || i < 1)
                    {

                    }
                    else if (i != 0)
                    {
                        intcommand.Add(i);
                        count++;
                    }
                    else if (s == " ") { }

                }
            }

            if (input.ToLower() == "hold")
            {
                if (reRollCount > 0)
                {
                    InputHoldHandeling(intcommand);
                }
                else throw new OutOfRollsException();
            }
        }

        // Handles the roll command
        private void InputHoldHandeling(List<int> commands)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!commands.Contains(i + 1)) gameDice[i].RollDice();
            }
            SortDices();
            reRollCount--;
        }

        // Handles input for section 1
        private void SectionInputHandeling(string input)
        {
            if (input.ToLower() == "ones")
            {
                if (SelectionAvailability("Ones"))
                {
                    int tVal = Section1ChoiceCalc(1);
                    Options options = new Options(tVal, 1, "Ones");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Ones");
            }

            else if (input.ToLower() == "twos")
            {
                if (SelectionAvailability("Twos"))
                {
                    int tVal = Section1ChoiceCalc(2);
                    Options options = new Options(tVal, 2, "Twos");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Twos");
            }


            else if (input.ToLower() == "threes")
            {
                if (SelectionAvailability("Threes"))
                {
                    int tVal = Section1ChoiceCalc(3);
                    Options options = new Options(tVal, 3, "Threes");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Threes");
            }


            else if (input.ToLower() == "fours")
            {
                if (SelectionAvailability("Fours"))
                {
                    int tVal = Section1ChoiceCalc(4);
                    Options options = new Options(tVal, 4, "Fours");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Fours");
            }


            else if (input.ToLower() == "fives")
            {
                if (SelectionAvailability("Fives"))
                {
                    int tVal = Section1ChoiceCalc(5);
                    Options options = new Options(tVal, 5, "Fives");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Fives");
            }


            else if (input.ToLower() == "sixes")
            {
                if (SelectionAvailability("Sixes"))
                {
                    int tVal = Section1ChoiceCalc(6);
                    Options options = new Options(tVal, 6, "Sixes");
                    SelectSec1Option(options);
                }
                else outputDisplay.Section1InvalidDisplay("Sixes");

            }

        }


        private void OptionInputHandeling(List<Options> options, int i)
        {
            foreach (Options o in options)
            {
                if (o.OptionNum == i)
                {
                    SelectOption(o);

                    break;
                }
            }
        }


        private void FullRerollHandeling()
        {
            if (reRollCount > 0)
            {
                RollAll();
                reRollCount -= 1;
            }
        }


        private void CheatInputHandeling()
        {
            isBiased = true;

            if (isBiased)
            {
                bool betterDice = true;
                string kbia;

                do
                {
                    outputDisplay.BiasDiceRegDisplay();
                    string temp = Console.ReadLine();

                    if (temp == "pos")
                    {
                        betterDice = true;
                        kbia = temp;
                    }
                    else if (temp == "neg")
                    {
                        betterDice = false;
                        kbia = temp;
                    }
                    else kbia = "?";
                }

                while (kbia == "?");
                int biasDegree;

                do
                {
                    outputDisplay.BiasDiceRegDegreeDisplay();
                    biasDegree = Int32.Parse(Console.ReadLine());
                }

                while (biasDegree == 0);
                BiasDiceChange(betterDice, biasDegree);

            }
            else RegDiceChange();
        }

        // function, when there is no options left
        private void NoInputHandeling()
        {
            string sInput;
            List<Options> removeableOptions = OptionRemainingList();
            removeableOptions = OptionNumSet(removeableOptions);
            outputDisplay.OptionsToRemoveDisplay(removeableOptions);
            sInput = Console.ReadLine();

            if (int.TryParse(sInput, out int iInput))
            {
                foreach (Options op in removeableOptions)
                {
                    if (op.OptionNum == iInput)
                    {
                        op.ZeroPoints();
                        SelectOption(op);
                        break;
                    }
                }
            }
        }


        //Select functions ---------------------------------------------------------------------------------------

        // Adds the selected option to the turn list
        private void SelectOption(Options option)
        {
            turns.Add(new Turn(option));
            turnActive = false;
        }


        private void SelectChance(Options options)
        {
            if (outputDisplay.ConfirmChanceDisplay(TotalValue()))
            {
                turns.Add(new Turn(options));
                turnActive = false;
            }
        }


        private void SelectSec1Option(Options options)
        {
            turns.Add(new Turn(options));
            turnActive = false;
        }

        // Display Functions -------------------------------------------------------------------------------------

        // collects information to the scoreboard
        private void CreateScoreBoard()
        {
            string[] scores = ConvertScoreBoardToString();

            string s1Total = Section1Calc(scores).ToString();
            if (s1Total.Length < 3) s1Total = "  " + s1Total;
            if (s1Total.Length < 3) s1Total = "  " + s1Total;

            string bonus = "  0";

            if (BonusCheck(int.Parse(s1Total))) bonus = "50";
            int totalScore = TotalScoreCalc(scores, int.Parse(bonus));
            outputDisplay.ScoreBoardDisplay(scores, s1Total, bonus, totalScore);
            Console.ReadLine();
        }

        // display available results
        private void ResultDisplay(List<Options> options)
        {
            foreach (Options op in options)
            {
                switch (op.Type)
                {
                    case "P":
                        outputDisplay.PairDisplay(op);
                        break;

                    case "TP":
                        outputDisplay.TwoPairDisplay(op);
                        break;

                    case "T":
                        outputDisplay.ThreeAKindDisplay(op);
                        break;

                    case "F":
                        outputDisplay.FourAKindDisplay(op);
                        break;

                    case "SS":
                        outputDisplay.SmallStrDisplay(op);
                        break;

                    case "LS":
                        outputDisplay.LargeStrDisplay(op);
                        break;

                    case "FH":
                        outputDisplay.FullHDisplay(op);
                        break;

                    case "Y":
                        outputDisplay.YathzeeDisplay(op);
                        break;

                }
            }
        }




    }




}
