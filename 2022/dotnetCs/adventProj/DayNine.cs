namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    internal class DayNine : DayTemplate
    {
        internal override string GetInput()
        {
            string testInput = string.Empty;
            bool useTestInput = true;  // test input answer is 13
            if (useTestInput)
            {
                testInput = "R 4 \n" +
                    "U 4 \n" +
                    "L 3 \n" +
                    "D 1 \n" +
                    "R 4 \n" +
                    "D 1 \n" +
                    "L 5 \n" +
                    "R 2 \n";
            }
            else
            {
                testInput = ReadInputToText("../../DayNineInput.txt");
            }

            return testInput;
        }

        internal override object GetAnswer(string testInput)
        {
            Grid grid = new Grid(5,6);

            string[] lines = testInput.Split('\n');
            foreach(string move in lines)
            {
                grid.PrintGrid();

                if (!string.IsNullOrWhiteSpace(move) && move.Count() >= 3)
                {
                    Console.WriteLine($"\n About to process next move {move}...");

                    char direction = move[0];
                    int numSteps = int.Parse(move[2].ToString());
                    grid.Move(direction, numSteps);
                }
            }

            return grid.GetNumberTailPositions();
        }
    }

    public struct GridPosition
    {
        public int Row;
        public int Column;

        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public GridPosition(GridPosition pos)
        {
            Row = pos.Row;
            Column = pos.Column;
        }

        public override string ToString()
        {
            return "(" + Row.ToString() + "," + Column.ToString() + ")";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if ((obj != null) && (obj.GetType() == typeof(GridPosition)))
            {
                GridPosition temp = (GridPosition)obj;
                if ((this.Row == temp.Row) && (this.Column == temp.Column))
                {
                    return true;
                }
            }
            return false;
        }
    }

    internal enum Direction
    {
        None = 0,
        Right = (int)'R',
        Left = (int)'L',
        Up = (int)'U',
        Down = (int)'D'
    }
    internal class Grid
    {

        int numRows;
        int numColumns;
        GridPosition Start;
        GridPosition Head;
        GridPosition Tail;

        HashSet<string> TailPositions;

        public int GetNumberTailPositions()
        {
            return TailPositions.Count();
        }

        internal Grid(int rows, int columns)
        {
            // Grid starts in lower left corner?
            Start = new GridPosition(rows-1, 0);
            Head = new GridPosition(Start);
            Tail = new GridPosition(Head);
            numRows = rows;
            numColumns = columns;
            TailPositions = new HashSet<string>();

            // Add the first position
            TailPositions.Add(Tail.ToString());
        }

        internal bool InGrid(int row, int column)
        {
            if (row >= 0 && column >= 0 && row < this.numRows && column < this.numColumns)
            {
                return true;
            }
            else {
                return false;  // Not expected from input, here for debugging.
            }
        }

        // Move the head, and track the tail following as appropriate
        internal void Move(char direction, int numPositions)
        {
            for (int i = 0; i < numPositions; i++)
            {
                var oldTail = Tail;

                switch (direction)
                {
                    // Move the head one position (only cardinal directions, no diagonals)
                    case 'R':
                        if (InGrid(Head.Row, Head.Column+1))
                        {
                            Head.Column = Head.Column+1;
                        }
                        break;
                    case 'L':
                        if (InGrid(Head.Row, Head.Column-1))
                        {
                            Head.Column = Head.Column-1;
                        }
                        break;
                    case 'U':
                        if (InGrid(Head.Row-1, Head.Column))
                        {
                            Head.Row = Head.Row-1;
                        }
                        break;
                    case 'D':
                        if (InGrid(Head.Row+1, Head.Column))
                        {
                            Head.Row = Head.Row+1;
                        }
                        break;
                    default:
                        Console.WriteLine("Warning: check move input");
                        break;
                }

                // Now assess tail move
                int rowDistance = int.Abs(Head.Row - Tail.Row);
                int colDistance =  int.Abs(Head.Column - Tail.Column);
                bool diagonalCell = (rowDistance == 1 && colDistance == 1);
                
                if (diagonalCell || (rowDistance + colDistance <= 1))
                {
                    // No need to move the tail, we are touching
                    continue;
                }
                else
                {
                    // Allowed (prefer?) a diagonal move?  Otherwise move row or column only.
                    bool moveDiagonally = (Head.Row != Tail.Row) && (Head.Column != Tail.Column);
 
                    if ((rowDistance == 2) || moveDiagonally)
                    {
                        // move up or down
                        if (Head.Row > Tail.Row)
                        {
                            // move down
                            Tail.Row = Tail.Row + 1;
                        }
                        else {
                            // move up
                            Tail.Row = Tail.Row -1;
                        }
                    }
                    if ((colDistance == 2) || moveDiagonally)
                    {
                        // move left or right
                        if (Head.Column > Tail.Column)
                        {
                            // Move right
                            Tail.Column = Tail.Column + 1;
                        }
                        else {
                            // Move left
                            Tail.Column = Tail.Column - 1;
                        }
                    }
                }


                // Keep track of tail positions (note that adding an already visited value is ok)
                TailPositions.Add(Tail.ToString());
            }
        }

        public void PrintGrid()
        {
            for (int i=0; i< numRows; i++)
            {
                Console.Write($"[{i}]: ");
                int tailPositions = 0;
                for (int j=0; j< numColumns; j++)
                {
                    GridPosition pos = new GridPosition(i, j);
                    bool previousTail = TailPositions.Contains(pos.ToString());
                    if (previousTail)
                    {
                        tailPositions++;
                    }

                    if (pos.Equals(Head))
                    {
                        Console.Write('H');
                    }
                    else if (pos.Equals(Tail))
                    {
                        Console.Write('T');
                    }
                    else if (pos.Equals(Start))
                    {
                        Console.Write('s');
                    }
                    else if (previousTail)
                    {
                        Console.Write('#');
                    }
                    else Console.Write('.');
                }

                Console.WriteLine($" = {tailPositions}");
            }
        }
    }
}