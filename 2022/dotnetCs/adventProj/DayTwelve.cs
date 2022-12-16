namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    internal class DayTwelve : DayTemplate
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
                testInput = ReadInputToText("../../DayTwelveInput.txt");

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

    public class HeightPosition
    {
        public int Height
        { get; set; }

        public GridPosition Position
        { get; set; }

        // Need this to detect the end node
        public bool IsEnd
        {
            get; set;
        }

        public HeightPosition(int row, int column, int height = 0, bool isEnd = false)
        {
            Position = new GridPosition(row, column);
            Height = height;
            IsEnd = isEnd;
        }
        public HeightPosition(int row, int column, char height = 'a', bool isEnd = false)
        {
            Position = new GridPosition(row, column);
            Height = height - (int)'a';  // Height of zero denoted by 'a' on grid
            IsEnd = isEnd;
        }

        public override string ToString()
        {
            return Position.ToString() + $":{Height} {IsEnd}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if ((obj != null) && (obj.GetType() == typeof(HeightPosition)))
            {
                HeightPosition temp = (HeightPosition)obj;
                if (this.Position.Equals(temp.Position) && (this.Height == temp.Height))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class HeightGrid
    {
        HeightPosition[,] heights;

        internal int Rows
        {
            private set; get;
        }

        internal int Columns
        {
            private set; get;
        }

        // Key = ending position so far (= end if done), value = path to that position
        internal Dictionary<HeightPosition, List<HeightPosition>> PossiblePaths
        { private set; get; }

        public HeightPosition Start
        { get; set; }

        public HeightPosition End
        { get; set; }

        internal HeightGrid(int rows, int columns)
        {
            heights = new HeightPosition[rows, columns];
            PossiblePaths = new Dictionary<HeightPosition, List<HeightPosition>>();
            Rows = rows;
            Columns = columns;
        }

        public static HeightGrid ParseFromInput(string input)
        {
            HeightGrid parsedGrid = null;

            if (!String.IsNullOrEmpty(input))
            {
                string[] lines = input.Split('\n');

                int lineCount = 0;
                for (int i = 0; i < lines.Count(); i++)
                {
                    string line = lines[i];
                    if (!String.IsNullOrEmpty(line))
                    {
                        lineCount++;

                        if (parsedGrid == null)
                        {
                            parsedGrid = new HeightGrid(lines.Count(), line.Count());
                        }

                        for (int j = 0; j < line.Count(); j++)
                        {
                            HeightPosition newPoint;

                            if (line[j] == 'S')
                            {
                                newPoint = new HeightPosition(i, j, 'a');
                                parsedGrid.Start = newPoint;
                            }
                            else if (line[j] == 'E')
                            {
                                newPoint = new HeightPosition(i, j, 'z', true);
                                parsedGrid.End = newPoint;
                            }
                            else
                            {
                                // Set the height to 1-27, based on the letter
                                newPoint = new HeightPosition(i, j, line[j]);
                            }

                            parsedGrid.heights[i, j] = newPoint;
                        }

                    }
                }
            }

            return parsedGrid;
        }

        internal bool FindPathToEnd()
        {
            // startList is used for both the path to start and the candidate list
            List<HeightPosition> startList = new List<HeightPosition>();
            startList.Add(this.Start);

            this.PossiblePaths.Add(this.Start, startList);

            FindPathToEnd(new List<HeightPosition>(startList));

            // Did we find at least one path to the end node?
            return (this.PossiblePaths != null) && (this.PossiblePaths.ContainsKey(this.End));
        }

        private bool FindPathToEnd(List<HeightPosition> candidates)
        {
            // Input = list of candidate grid nodes to assess, in order.
            // key is that candidates are added in progressively longer path lengths
            // (So the first time we hit End, it's the shortest path)
            // Keep checking the first candidate (and removing it from the list)
            bool pathFound = false;
            while (candidates.Any())
            {
                HeightPosition current = candidates.First();

                candidates.Remove(current);
                if (current.Equals(this.End))
                {
                    pathFound = true;
                    break;
                }

                int row = current.Position.Row;
                int column = current.Position.Column;
                int height = current.Height;

                // find and add unvisited valid neighbors of current as candidates

                for (int x = 0; x < 4; x++)
                {
                    HeightPosition validNeighbor = null;

                    switch (x)
                    {
                        case 0:
                            // look up
                            if (row > 0)
                            {
                                validNeighbor = this.heights[row - 1, column];
                            }

                            break;
                        case 1:
                            // look down
                            if (row < this.Rows - 1)
                            {
                                validNeighbor = this.heights[row + 1, column];
                            }
                            break;
                        case 2:
                            // look right
                            if (column < this.Columns - 1)
                            {
                                validNeighbor = this.heights[row, column + 1];
                            }
                            break;
                        case 3:
                            // look left
                            if (column > 0)
                            {
                                validNeighbor = this.heights[row, column - 1];
                            }
                            break;
                    }

                    // Is this a new valid candidate?
                    if ((validNeighbor == null) || this.PossiblePaths.ContainsKey(validNeighbor))
                    {
                        // don't need to add anything
                        continue;
                    }
                    if (validNeighbor.Height <= height+1)
                    {
                        // Build and save path for this new candidate
                        var newPath = new List<HeightPosition>(this.PossiblePaths[current]);
                        newPath.Add(validNeighbor);
                        this.PossiblePaths.Add(validNeighbor, newPath);

                        candidates.Add(validNeighbor);
                    }
                }
            }

            return pathFound;
        }
    }
}