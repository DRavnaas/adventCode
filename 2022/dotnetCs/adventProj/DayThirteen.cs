// Template only, needs guts
namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    internal class DayThirteen : DayTemplate
    {

        internal override string GetInput()
        {
            string testInput = string.Empty;
            bool useTestInput = false;  // test input answer is 10605 for part 1 / 2713310158 for part two
            if (useTestInput)
            {
                testInput =       // part one answer is 31 steps
                   "Sabqponm \n" +
                   "abcryxxl \n" +
                   "accszExk \n" +
                   "acctuvwj \n" +
                   "abdefghi ";
            }
            else
            {
                // 
                testInput = ReadInputToText("../../DayThirteenInput.txt");

            }

            return testInput;
        }

        internal override object GetAnswer(string testInput)
        {
            // read in height map, find S and E
            HeightGrid grid = HeightGrid.ParseFromInput(testInput);

            // keep track of various paths (some will deadend)
            if (grid.FindPathToEnd())
            {
                var pathToEnd = grid.PossiblePaths[grid.End];
                return pathToEnd.Count() - 1;  // count steps = nodes - 1
            }

            return -1;  // not found
        }
    }

