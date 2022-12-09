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

                var testInput2 = 
                            "99099\n" +  
                            "99399\n" +
                            "99399\n" +
                            "99299\n" +
                            "99009" 
                            ;
            }

            TreeGrid grid = TreeGrid.BuildTreeGrid(testInput);

            retVal = grid.GetScenicScoresInGrid();

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
                if (row < this.rows && column < this.columns)
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
            public uint GetScenicScoresInGrid()
            {
                uint maxScenicScore = 0;

                // Get scenic score for all interior trees, keep track of highest score
                // Edge trees have a score of zero, no need to calculate those

                for (uint i=1; i < rows-1; i++)
                {
                    for (uint j=1; j < columns-1; j++)
                    {
                        uint scenicScore = 0;  
                        
                        scenicScore = 
                            ScenicToLeft(i,j) *
                            ScenicToRight(i,j) *
                            ScenicToBottom(i,j) *
                            ScenicToTop(i,j);
                    

                        if (scenicScore > maxScenicScore)
                        {
                            maxScenicScore = scenicScore;
                        }
                    }
                    
                }
                return maxScenicScore;
            }

            public uint ScenicToLeft(uint row, uint column)
            {
                if (column == 0)
                {
                    // zero trees to left
                    return 0;
                }

                uint scenicScoreSoFar = 1;   // not on edge, we can see at least 1 tree
                uint sourceTreeHeight = grid[row,column].Height;

                for (uint j=column-1; j>0; j--)
                {
                    uint targetHeight = grid[row, j].Height;
                    if (targetHeight >= sourceTreeHeight)
                    {
                        // We found our view blocker
                        return scenicScoreSoFar;
                    }

                    scenicScoreSoFar++;
                }

                return scenicScoreSoFar;
            }

            public uint ScenicToRight(uint row, uint column)
            {
                if (column == columns - 1)
                {
                    // Zero trees to right
                    return 0;
                }

                uint scenicScoreSoFar = 1;   // not on edge, we can see at least 1 tree
                uint sourceTreeHeight = grid[row,column].Height;

                for (uint j=column+1; j<columns-1; j++)
                {
                    uint targetHeight = grid[row, j].Height;
                    if (targetHeight >= sourceTreeHeight)
                    {
                        // We found our view blocker
                        return scenicScoreSoFar;
                    }

                    scenicScoreSoFar++;
                }

                return scenicScoreSoFar;
                
            }

            public bool IsOnEdge(uint row, uint column)
            {
                if (column == 0 || row == 0 || row == rows-1 || column == columns -1)
                {
                    return true;
                }
                return false;
            }
            public uint ScenicToTop(uint row, uint column)
            {
                if (row == 0)
                {
                    return 0;
                }

                uint scenicScoreSoFar = 1;   // not on edge, we can see at least 1 tree
                uint sourceTreeHeight = grid[row,column].Height;

                for (uint i=row-1; i>0; i--)
                {
                    uint targetHeight = grid[i, column].Height;
                    if (targetHeight >= sourceTreeHeight)
                    {
                        // We found our view blocker
                        return scenicScoreSoFar;
                    }

                    scenicScoreSoFar++;
                }

                return scenicScoreSoFar;
            }

            public uint ScenicToBottom(uint row, uint column)
            {
                if (row == row - 1)
                {
                    return 0;
                }

                uint scenicScoreSoFar = 1;   // not on edge, we can see at least 1 tree
                uint sourceTreeHeight = grid[row,column].Height;

                for (uint i=row+1; i<rows-1; i++)
                {
                    uint targetHeight = grid[i, column].Height;
                    if (targetHeight >= sourceTreeHeight)
                    {
                        // We found our view blocker
                        return scenicScoreSoFar;
                    }

                    scenicScoreSoFar++;
                }

                return scenicScoreSoFar;
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