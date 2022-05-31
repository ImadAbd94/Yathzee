

namespace YatzheeExam
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputDisplay output = new OutputDisplay();
            Game game = new Game(output);

            output.IntroductionDisplay();

            game.SetUp();

        }

       
            
        }

        

    }

