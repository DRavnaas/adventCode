namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayFive : DayTemplate
    {
        internal class Crate
        {

            public string Name
            {
                get; set;
            }

            Crate(string name)
            {
                Name = name;
            }
            public static List<Crate>[] BuildStacks(string crateStacksTextLines)
            {
                List<Crate>[] stackOfCrates;

                string[] stackHorizontal = crateStacksTextLines.Split("\n");
                string[] stackLastLine = stackHorizontal.Last().Split(" ");

                // Build initial (empty) set of crate stacks
                int columnCounts = 0;
                foreach (string input in stackLastLine)
                {
                    if (!String.IsNullOrWhiteSpace(input))
                    {
                        columnCounts++;
                    }
                }

                // Now build the actual stacks
                stackOfCrates = new List<Crate>[columnCounts];
                foreach (string cratesInput in stackHorizontal.SkipLast(1).Reverse())
                {
                    if (!String.IsNullOrWhiteSpace(cratesInput))
                    {
                        // walk horizontal row by # of crates
                        // (this assumes crate keys are in order they were added - check!)
                        int crateInputIndex = 0;
                        for (int i = 0; i < columnCounts; i++)
                        {
                            // Start each stack off with an empty list (no crates)
                            if (stackOfCrates[i] == null)
                            {
                                stackOfCrates[i] = new List<Crate>();
                            }

                            if (cratesInput[crateInputIndex] == '[')
                            {
                                // Add this crate to the appropriate stack
                                var currentCrate = new Crate(cratesInput[crateInputIndex + 1].ToString());

                                stackOfCrates[i].Add(currentCrate);
                            }

                            // Advance a column
                            // This assumes single character crate names
                            crateInputIndex += 4;
                        }
                    }
                }

                return stackOfCrates;
            }

            public static string GetTopCrateNames(List<Crate>[] crateStacks)
            {
                // Get the top crates after moves
                string tmpVal = "";
                foreach (List<Crate> stack in crateStacks)
                {
                    if (stack.Count() == 0)
                    {
                        // empty stack
                        tmpVal += " ";
                    }
                    else
                    {
                        tmpVal += (stack.Last().Name);
                    }

                }

                return tmpVal;

            }

        }

        internal override object GetAnswer(string testInput)
        {
            string retVal = "";

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1: this result is CMP; test input file result MQTPGLLDN
                // part 2: this result is MCD; test input file result 
                testInput = "    [D]    \n[N] [C]    \n[Z] [M] [P] \n 1   2   3 \n\n" +
                "move 1 from 2 to 1\n" +
                "move 3 from 1 to 3\n" +
                "move 2 from 2 to 1\n" +
                "move 1 from 1 to 2\n";
            }

            string[] cratesAndMoves = testInput.Split("\n\n");

            List<Crate>[] crateStacks = Crate.BuildStacks(cratesAndMoves[0]);

            string[] moves = cratesAndMoves[1].Split("\n");
            foreach (string moveDetails in moves)
            {
                string[] moveParameters = moveDetails.Split(" ");

                // process moves by popping/pushing
                // index 1 = # crates, index 3 = from stack key, index 5 = to stack key
                if (moveParameters.Count() == 6)
                {
                    var tmpVal = Crate.GetTopCrateNames(crateStacks);

                    Console.WriteLine($"top of stack = {tmpVal} before action: {moveDetails}");

                    int numCrates = 0;
                    if (int.TryParse(moveParameters[1], out numCrates))
                    {
                        int fromStack = int.Parse(moveParameters[3]) - 1;
                        int toStack = int.Parse(moveParameters[5]) - 1;

                        // Copy crates from one stack to other, then delete from original stack
                        //for (int i= crateStacks[fromStack].Count()-numCrates; i<crateStacks[fromStack].Count(); i++)
                        //{
                        var cratesToMove = crateStacks[fromStack].GetRange(crateStacks[fromStack].Count()-numCrates, numCrates);
                        crateStacks[toStack].AddRange(cratesToMove);
                        crateStacks[fromStack].RemoveRange(crateStacks[fromStack].Count()-numCrates, numCrates);
                        //for (int i = 1; i < numCrates + 1; i++)
                        //{
                        //    var crateMoved = crateStacks[fromStack].Pop();
                        //    crateStacks[toStack].Push(crateMoved);
                        //}
                    }
                }
            }

            // Get the top crates
            retVal = Crate.GetTopCrateNames(crateStacks);

            return (object)retVal;
        }
    }
}