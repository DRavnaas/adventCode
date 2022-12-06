namespace adventProj
{
    class Program
    {
        static void Main(string[] args)
        {
            string testInput = String.Empty;
            uint currentDay = 3; // which day are we working on

            if (args.Any())
            {
                switch (currentDay) {
                    case 1:
                        uint daysAnswer = 0;
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        daysAnswer = DayOne.GetAnswer(testInput);

                        // test answer part 2 is 45000, input file is 206152
                        Console.WriteLine($"\nMost calories: {daysAnswer}");
                        break;
                    case 2:
                        uint playerScore = 0;
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        playerScore = DayTwo.GetAnswer(testInput);

                        Console.WriteLine($"\nScore: {playerScore}");
                        break;
                    case 3:
                        uint overallSum = 0;
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        overallSum = DayThree.GetAnswer(testInput);

                        Console.WriteLine($"\nSum: {overallSum}");
                        break;
                    default:
                        Console.WriteLine("Unknown day");
                        break;
                }
            }      
        }

     

        static string ReadInputToText(string filename)
        {
            string retVal = String.Empty;

            if (!String.IsNullOrWhiteSpace(filename) && System.IO.File.Exists(filename))
            {
                retVal = System.IO.File.ReadAllText(filename);
            }

            return retVal;
        }
    }
}