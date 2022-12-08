namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayEight : DayTemplate
    {

        internal override object GetAnswer(string testInput)
        {
            uint retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                testInput = "30373\n" +
                             "25512\n" +
                             "65332\n" +
                             "33549\n" +
                             "35390";
            }

            TreeGrid grid = TreeGrid.BuildTreeGrid(testInput);

            // IsVisible = on edge (first or last row, first or last colummn) or higher number than rest in row or column

            // count visible trees
            retVal = grid.SetVisibleTreesInGrid();

            return retVal;
        }

        public class TreeGrid
        {
            public Tree[,] grid;
            public uint rows;
            public uint columns;

            public TreeGrid(uint inRows, uint inColumns)
            {
                grid = new Tree[inRows, inColumns];
                rows = inRows;
                columns = inColumns;
            }
            public void SetTree(uint height, uint row, uint column)
            {
                if (row <= this.rows && column <= this.columns)
                {
                    Tree tree = new Tree();
                    tree.Height = height;
                    this.grid[row, column] = tree;
                }
            }

            public static TreeGrid BuildTreeGrid(string input)
            {
                TreeGrid grid = new TreeGrid(0, 0);  // default return value if we couldn't read input
                string[] gridInputLines = input.Split("\n");
                if (gridInputLines != null && gridInputLines.Any() && gridInputLines[0].Count() > 0)
                {
                    grid = new TreeGrid((uint)gridInputLines.Count(), (uint)gridInputLines[0].Count());

                    uint row = 0, column = 0;
                    foreach (string gridInput in gridInputLines)
                    {
                        column = 0;
                        foreach (char treeHeightValue in gridInput)
                        {
                            uint treeHeight = 0;
                            if (uint.TryParse(treeHeightValue.ToString(), out treeHeight))
                            {
                                grid.SetTree(treeHeight, row, column);
                            }
                            column++;
                        }
                        row++;

                    }
                }

                return grid;
            }
            public uint SetVisibleTreesInGrid()
            {
                uint visibleCount = 0;

                // Could calculate visible first (a little more efficient)
                // and then just count up what was set.

                for (uint i=0; i < rows; i++)
                {
                    for (uint j=0; j < columns; j++)
                    {
                        bool isVisible = 
                            IsVisibleFromRight(i,j) ||
                            IsVisibleFromLeft(i,j) ||
                            IsVisibleFromTop(i,j) ||
                            IsVisibleFromBottom(i,j);

                        if (isVisible)
                        {
                            visibleCount++;
                            continue;
                        }
                    }
                }
                return visibleCount;
            }

            public bool IsVisibleFromRight(uint row, uint column)
            {
                if (row == 0)
                {
                    // Edge tree is always visible from right
                    return true;
                }

                uint maxHeightSoFar = 0;
                for (uint j=0; j<column; j++)
                {
                    uint currentHeight = grid[row, j].Height;
                    if (currentHeight > maxHeightSoFar)
                    {
                        maxHeightSoFar = currentHeight;
                    }

                    if (grid[row, column].Height <= maxHeightSoFar)
                    {
                        // Already obscured by taller or equal height tree, don't need to continue
                        return false;
                    }
                }

                return true;
            }

            public bool IsVisibleFromLeft(uint row, uint column)
            {
                if (row == rows - 1)
                {
                    return true;
                }

                uint maxHeightSoFar = 0;
                for (uint j=columns-1; j>column; j--)
                {
                    uint currentHeight = grid[row, j].Height;
                    if (currentHeight > maxHeightSoFar)
                    {
                        maxHeightSoFar = currentHeight;
                    }

                    if (grid[row, column].Height <= maxHeightSoFar)
                    {
                        // Already obscured by taller or equal height tree, don't need to continue
                        return false;
                    }
                }

                return true;
            }

            public bool IsVisibleFromTop(uint row, uint column)
            {
                if (column == 0)
                {
                    return true;
                }

                uint maxHeightSoFar = 0;
                for (uint i=0; i<row; i++)
                {
                    uint currentHeight = grid[i, column].Height;
                    if (currentHeight > maxHeightSoFar)
                    {
                        maxHeightSoFar = currentHeight;
                    }

                    if (grid[row, column].Height <= maxHeightSoFar)
                    {
                        // Already obscured by taller or equal height tree, don't need to continue
                        return false;
                    }
                }

                return true;
            }

            public bool IsVisibleFromBottom(uint row, uint column)
            {
                if (column == columns - 1)
                {
                    return true;
                }

                uint maxHeightSoFar = 0;
                for (uint i=row; i<rows-1; i--)
                {
                    uint currentHeight = grid[i, column].Height;
                    if (currentHeight > maxHeightSoFar)
                    {
                        maxHeightSoFar = currentHeight;
                    }

                    if (grid[row, column].Height <= maxHeightSoFar)
                    {
                        // Already obscured by taller or equal height tree, don't need to continue
                        return false;
                    }
                }

                return true;
            }
        }

        public class Tree
        {
            public uint Height
            {
                get; set;
            }
        }
    }
}