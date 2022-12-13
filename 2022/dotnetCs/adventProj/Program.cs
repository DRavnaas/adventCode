namespace adventProj
{
    class Program
    {
        static void Main(string[] args)
        {    
            uint currentDay = 11; // which day are we working on

            var solutions = new DayTemplate[] 
                {new DayOne(), 
                new DayTwo(), 
                new DayThree(), 
                new DayFour(), 
                new DayFive(), 
                new DaySix(),
                new DaySeven(),
                new DayEight(),
                new DayNine(),
                new DayTen(),
                new DayEleven()
                };

            string testInput = String.Empty;

            testInput = solutions[currentDay-1].GetInput();
                        
            var result = solutions[currentDay-1].GetAnswer(testInput);

            Console.WriteLine($"\nResult: {result}");
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