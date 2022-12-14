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
            bool useTestInput = true;  // test input answer is 10605 for part 1 / 2713310158 for part two
            if (useTestInput)           
            {
                testInput =       // part one answer is 31 steps
                   "Sabqponm \n" +
                   "abcryxxl \n" +
                   "accszExk \n" +
                   "acctuvwj \n" +
                   "abdefghi ";
            }
            else {
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
                int minPathSize = int.MaxValue;
                List<int> pathSizes = grid.PossiblePaths.Values.Select( x => x.Count()).ToList<int>();
                pathSizes.Sort();
                return pathSizes.TakeLast(1).First();
                
            }

            // Work out which path was shortest

            return 0;
        }
    }

    public class HeightPosition
    {
        public int Height
        { get; set;}

        public GridPosition Position
        { get; set; }

        public bool Visited
        {
            get; set;
        }

        public HeightPosition(int row, int column, int height=0)
        {
            Position = new GridPosition(row, column);
            Height = height;
        }
        public HeightPosition(int row, int column, char height='a')
        {
            Position = new GridPosition(row, column);
            Height = height - (int)'a';
        }

        public override string ToString()
        {
            return Position.ToString() + $":{Height}";
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
        { private set; get;}

        public HeightPosition Start
        { get; set;}

        public HeightPosition End
        { get; set; }
        
        internal HeightGrid(int rows, int columns)
        {
            heights = new HeightPosition[rows,columns];
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
                for(int i=0; i< lines.Count(); i++)
                {
                    string line = lines[i];
                    if (!String.IsNullOrEmpty(line))
                    {
                        lineCount++;

                        if (parsedGrid == null)
                        {
                            parsedGrid = new HeightGrid(lines.Count(), line.Count());
                        }

                        for(int j=0; j< line.Count(); j++)
                        {
                            HeightPosition newPoint;
                            
                            if (line[j] == 'S')
                            {
                                newPoint = new HeightPosition(i,j,0);
                                parsedGrid.Start = newPoint;
                            }
                            else if (line[j] == 'E')
                            {
                                newPoint = new HeightPosition(i,j,0);
                                parsedGrid.End = newPoint;        
                            }
                            else
                            {
                                // Set the height to 1-27, based on the letter
                                newPoint =new HeightPosition(i,j, line[j]-'a'+1);
                            }
                                
                            parsedGrid.heights[i,j] = newPoint;
                        }
                        
                    }
                }
            }

            return parsedGrid;
        }

        internal bool FindPathToEnd()
        {
            List<HeightPosition> startPath = new List<HeightPosition>();
            startPath.Add(this.Start);

            FindPathToEnd(startPath);

            return (this.PossiblePaths != null) && (this.PossiblePaths.Count() > 0);
        }

        private bool FindPathToEnd(List<HeightPosition> pathSoFar)
        {
            // input = a valid path so far

            HeightPosition lastPoint = pathSoFar.Last();
            lastPoint.Visited = true;
            int row = lastPoint.Position.Row;
            int column = lastPoint.Position.Column;
            int height = lastPoint.Height;

            if (lastPoint.Equals(this.End))
            {
                this.PossiblePaths.Add(this.End, pathSoFar);
                return true;
            }

            // keep looking
            bool anyFound = false;

            for(int x=0; x < 4; x++)
            {
                HeightPosition nextPos = null;

                switch(x)
                {
 
                    case 0:
                        // look up
                        if (row > 0)
                        {
                            nextPos = this.heights[row-1, column];
                        }
                        
                        break;
                    case 1:
                        // look down
                        if (row < this.Rows -1)
                        {
                            nextPos = this.heights[row+1, column];
                        }
                        break;
                    case 2:
                        // look right
                        if (column < this.Columns - 1)
                        {
                            nextPos = this.heights[row, column+1];
                        }
                        break;
                    case 3:
                        // look left
                        if (column > 0)
                        {
                            nextPos = this.heights[row, column-1];
                        }
                        break;
                }

                if ((nextPos != null) && (!nextPos.Visited))
                {
                    if (nextPos.Height <= height + 1)
                    {
                        pathSoFar.Add(nextPos);
                        anyFound = anyFound || FindPathToEnd(pathSoFar);
                    }
                }

            }

            return anyFound;
        }
    }
}