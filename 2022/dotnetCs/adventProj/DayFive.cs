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
            public static IDictionary<string, Stack<Crate>> BuildStacks(string crateStacksTextLines)
            {
                IDictionary<string, Stack<Crate>> stackOfCrates = new Dictionary<string, Stack<Crate>>();

                string[] stackHorizontal = crateStacksTextLines.Split("\n");
                string[] stackLastLine = stackHorizontal.Last().Split(" ");

                // Build initial (empty) set of crate stacks
                foreach (string input in stackLastLine)
                {
                    if (!String.IsNullOrWhiteSpace(input))
                    {
                        stackOfCrates.Add(input, new Stack<Crate>());
                    }
                }

                // Now build the actual stacks
                foreach (string cratesInput in stackHorizontal.SkipLast(1).Reverse())
                {
                    if (!String.IsNullOrWhiteSpace(cratesInput))
                    {
                        // walk horizontal row by # of crates
                        // (this assumes crate keys are in order they were added - check!)
                        int crateInputIndex = 0;
                        foreach (string cratePosition in stackOfCrates.Keys)
                        {
                            if (cratesInput[crateInputIndex] == '[')
                            {
                                // Push this crate onto the appropriate stack
                                var currentCrate = new Crate(cratesInput[crateInputIndex + 1].ToString());
                                stackOfCrates[cratePosition].Push(currentCrate);
                            }

                            // Advance a column
                            // This assumes single character crate names
                            crateInputIndex += 4;
                        }
                    }
                }

                return stackOfCrates;
            }

        }

        internal override object GetAnswer(string testInput)
        {
            string retVal = "";

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1: this result is CMP; test input file result MQTPGLLDN
                // pars 2: this result is MCD; test input file result 
                testInput = "    [D]    \n[N] [C]    \n[Z] [M] [P] \n 1   2   3 \n\n" +
                "move 1 from 2 to 1\n" +
                "move 3 from 1 to 3\n" +
                "move 2 from 2 to 1\n" +
                "move 1 from 1 to 2\n";
            }

            string[] cratesAndMoves = testInput.Split("\n\n");

            IDictionary<string, Stack<Crate>> crateStacks = Crate.BuildStacks(cratesAndMoves[0]);

            string[] moves = cratesAndMoves[1].Split("\n");
            foreach (string moveDetails in moves)
            {
                string[] moveParameters = moveDetails.Split(" ");

                // process moves by popping/pushing
                // index 1 = # crates, index 3 = from stack key, index 5 = to stack key
                if (moveParameters.Count() == 6)
                {
                    int numCrates = 0;
                    if (int.TryParse(moveParameters[1], out numCrates))
                    {
                        string fromStack = moveParameters[3];
                        string toStack = moveParameters[5];

                        for (int i = 1; i < numCrates + 1; i++)
                        {
                            var crateMoved = crateStacks[fromStack].Pop();
                            crateStacks[toStack].Push(crateMoved);
                        }

                        // Get the top crates after moves
                        string tmpVal = "";
                        foreach (string key in crateStacks.Keys)
                        {
                            if (crateStacks[key].Count() == 0)
                            {
                                // empty stack
                                tmpVal += " ";
                            }
                            else
                            {
                                tmpVal += (crateStacks[key].Peek().Name);
                            }

                        }

                        Console.WriteLine($"top of stack = {tmpVal} for action: {moveDetails}");
                    }
                }
            }

            // Get the top crates
            foreach (string key in crateStacks.Keys)
            {
                retVal += (crateStacks[key].Peek().Name);
            }

            return (object)retVal;
        }
    }
}