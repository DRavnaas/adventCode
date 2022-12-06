namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;

    internal class DayOne
    {
        internal static uint GetAnswer(string testInput)
        {
            if (String.IsNullOrEmpty(testInput))
            {
                testInput = "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000";
            }

            uint calorieAnswer = DayOne.CountElfCalories(testInput);

            Console.WriteLine("\nMost calories: {0}", calorieAnswer);   

            return calorieAnswer;
        }

        internal static uint CountElfCalories(string elfCalorieInput, uint maxLinesToRead=uint.MaxValue)
        {
            // sorted top four uints of calories
            // elfThreshold - if elf total > elfThreshold, set #4 to new elf total, and sort. 

            // read block of numbers, keep track of the largest block
            string [] elfCalorieLines = elfCalorieInput.Split("\n");
            uint maxCalories = 0, currentCalories = 0, currLine = 0;
            
            List<uint> elfCalorieTotals = new List<uint>();

            foreach (string input in elfCalorieLines)
            {
                if (currLine >= maxLinesToRead)
                {
                    Console.WriteLine("line max hit at {0}, exiting...", currLine);
                    break;
                }
                currLine++;

                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Should be a number.  Ignore if not (ie: zero calories)
                    uint calorieValue = 0;
                    if (uint.TryParse(input, out calorieValue))
                    {
                        currentCalories += calorieValue;
                    }
                }

                if (string.IsNullOrWhiteSpace(input) || currLine == elfCalorieLines.Count())
                {
                    // End of block or end of file
                    // Save this elf's total; clear out current elf calorie counter for the next one
                    // This assumes elves are always delimited by exactly one blank line (not more)
                    elfCalorieTotals.Add(currentCalories);
                    currentCalories = 0;
                }

                //if (currentCalories > maxCalories)
                //{
                    //Console.WriteLine("new max = {0}", currentCalories);
                //    maxCalories = currentCalories;
                //}
            }

            // Now sort the elfCalorie list and take the top 3 . (could refactor for top X)
            maxCalories = 0;

            if (elfCalorieTotals.Any())
            {
                elfCalorieTotals.Sort();
                int elfCount = elfCalorieTotals.Count;

                // Example: count of 4, elfIndex = 4-3 = 1; add up index 1, 2, 3
                for (int elfIndex = elfCount - 3; elfIndex >=0 && elfIndex < elfCount; elfIndex ++)
                {
                    Console.WriteLine("counting index {0}, calories {1}", elfIndex, elfCalorieTotals[elfIndex]);
                    maxCalories = maxCalories + elfCalorieTotals[elfIndex];
                }
             }
   
            return maxCalories;
        }
    }
}