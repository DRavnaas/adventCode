namespace adventProj
{
    internal class DayOne
    {
        internal static uint CountElfCalories(string elfCalorieInput)
        {
            Console.WriteLine("counting calories from:\n{0}", elfCalorieInput);

            // read block of numbers, keep track of the largest block
            string [] elfCalorieLines = elfCalorieInput.Split("\n");
            uint maxCalories = 0, currentCalories = 0;
            foreach (string input in elfCalorieLines)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Should be a number.  Ignore if not (ie: zero calories)
                    uint calorieValue = 0;
                    if (uint.TryParse(input, out calorieValue))
                    {
                        currentCalories += calorieValue;
                    }
                }
                else
                // elf calorie block separator?
                {
                    if (currentCalories > maxCalories)
                    {
                        Console.WriteLine("new max = {0}", currentCalories);
                        maxCalories = currentCalories;
                    }

                    // Clear out current elf calorie counter for the next one
                    currentCalories = 0;
                }
            }

            return maxCalories;
        }
    }
}