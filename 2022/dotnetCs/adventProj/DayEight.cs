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

            //if (String.IsNullOrEmpty(testInput))
            {
                testInput = "30373" +
                             "25512" +
                             "65332" +
                             "33549" +
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
                    tree.IsEdge = (row == 0) || column == 0 || row == rows - 1 || column == columns - 1;
                    tree.IsVisible = tree.IsEdge;
                }
            }

            public static TreeGrid BuildTreeGrid(string input)
            {
                TreeGrid grid = new TreeGrid(0,0);  // default return value if we couldn't read input
                string[] gridInputLines = input.Split("\n");
                if (gridInputLines != null && gridInputLines.Any() && gridInputLines[0].Count() > 0)
                {
                    grid = new TreeGrid((uint)gridInputLines.Count(), (uint)gridInputLines[0].Count());
                    foreach (string gridInput in gridInputLines)
                    {

                    }
                }

                return grid;
            }
            public uint SetVisibleTreesInGrid()
            {
                uint visibleCount = 0;
                return visibleCount;
            }
        }

        public class Tree
        {
            public bool IsVisible
            {
                get; set;
            }

            public bool IsEdge
            {
                get; set;
            }
            public uint Height
            {
                get; set;
            }
        }
    }
}