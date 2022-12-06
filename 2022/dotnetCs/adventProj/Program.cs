namespace adventProj
{
    class Program
    {
        static void Main(string[] args)
        {
            string testInput = String.Empty;
            uint currentDay = 4; // which day are we working on

            if (args.Any())
            {
                uint result = 0;

                switch (currentDay)
                {
                    case 1:
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        result = DayOne.GetAnswer(testInput);

                        // test answer part 2 is 45000, input file is 206152
                        Console.WriteLine($"\nMost calories: {result}");
                        break;
                    case 2:
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        result = DayTwo.GetAnswer(testInput);

                        Console.WriteLine($"\nScore: {result}");
                        break;
                    case 3:
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        result = DayThree.GetAnswer(testInput);

                        Console.WriteLine($"\nSum: {result}");
                        break;
                    case 4:
                        Console.WriteLine($"Reading input from {args[0]}");
                        testInput = ReadInputToText(args[0]);
                        result = DayFour.GetAnswer(testInput);

                        Console.WriteLine($"\nSum: {result}");
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