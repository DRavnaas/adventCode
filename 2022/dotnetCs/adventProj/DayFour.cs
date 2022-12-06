namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayFour
    {
        private struct AssignmentRange
        {
            public uint Start;
            public uint End;

            public readonly uint Count
            {
                get { return End - Start + 1; }
            }

            public AssignmentRange()
            {
                Start = 0;
                End = 0;
            }

            public AssignmentRange(uint start, uint end)
            {
                Start = start;
                End = end;
            }

            public static bool TryParse(string stringToParse, string separator, out AssignmentRange parsed)
            {
                string[] range = stringToParse.Split(separator);
                parsed.Start = parsed.End = 0;
                uint temp1, temp2;

                if (!string.IsNullOrWhiteSpace(range[0]) &&
                    !string.IsNullOrWhiteSpace(range[1]) &&
                    uint.TryParse(range[0], out temp1) &&
                    uint.TryParse(range[1], out temp2))
                {
                    parsed.Start = temp1;
                    parsed.End = temp2;
                    return true;
                }

                return false;
            }
        }

        internal static uint GetAnswer(string testInput)
        {
            uint retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1: Test input answer is 2; real input answer is 534
                // part 2: test input answer is 4; real input answer is 841
                testInput = "2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8";
            }

            // Process one line = two assignment ranges

            // Set shorter and longer range
            // Is the shorter contained in the larger
            //   ie: if shorter start >= larger start and shorter end <= larger end

            string[] assignmentPairs = testInput.Split("\n");
            foreach (string assignmentPair in assignmentPairs)
            {
                AssignmentRange larger, smaller;

                string[] ranges = assignmentPair.Split(",");

                AssignmentRange current;
                AssignmentRange.TryParse(ranges[0], "-", out larger);
                AssignmentRange.TryParse(ranges[1], "-", out current);

                if (current.Count <= larger.Count)
                {
                    // Less (or same number) of items in smaller range 
                    smaller = current;
                }
                else
                {
                    // Ensure larger range is in larger object for subrange comparison
                    smaller = larger;
                    larger = current;
                }

                //if ((smaller.Start >= larger.Start) && (smaller.End <= larger.End)) part 1m range contained within the other
                // part two - does the smaller range overlap at all with the larger?
                if (((smaller.Start >= larger.Start) && (smaller.Start <= larger.End)) ||
                    ((smaller.End >= larger.Start) && (smaller.End <= larger.End)))
                {
                    // if start is within range or if end is within range
                    // One assignment range overlaps the other, increment count.
                    retVal++;
                }

            }

            return retVal;
        }
    }
}