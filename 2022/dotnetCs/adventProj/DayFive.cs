namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayFive : DayTemplate
    {
        internal class Crate {

            public static IDictionary<string, Stack<Crate>> BuildStacks(string crateStacksTextLines)
            {
                IDictionary<string, Stack<Crate>> stackOfCrates = new Dictionary<string, Stack<Crate>>();

                string [] stackHorizontal = crateStacksTextLines.Split("\n");
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
                        //stackOfCrates.Add(input, new Stack<Crate>());
                    }
                }

                return stackOfCrates;
            }

        }

        internal override object GetAnswer(string testInput)
        {
            uint retVal = 0;

            //if (String.IsNullOrEmpty(testInput))
            {
                testInput = "    [D]    \n[N] [C]    \n[Z] [M] [P] \n 1   2   3 \n\n" +
                "move 1 from 2 to 1\n" +
                "move 3 from 1 to 3\n" +
                "move 2 from 2 to 1\n" +
                "move 1 from 1 to 2\n";
            }

            string[] cratesAndMoves = testInput.Split("\n\n");

            IDictionary<string, Stack<Crate>>crateStacks = Crate.BuildStacks(cratesAndMoves[0]);

            return (object)retVal;
        }
    }
}