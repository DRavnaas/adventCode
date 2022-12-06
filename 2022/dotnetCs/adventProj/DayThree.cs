namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayThree
    {
        
        internal static uint GetAnswer(string testInput)
        {
            uint overallSum = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1 test input score for first line = 16
                testInput = "vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL";
            }

            // Break up rucksacks and then each rucksack into equal length compartments
            string[] ruckSacks = testInput.Split("\n");
            foreach (string ruckSackContents in ruckSacks)
            {
                if (ruckSackContents.Any())
                {
                    // compartments have same number items.
                    string compartment1 = ruckSackContents.Substring(0, ruckSackContents.Count()/2);
                    string compartment2 = ruckSackContents.Substring(compartment1.Count());

                    if (compartment1.Count() == compartment1.Count())
                    {
                        Dictionary<char, uint> compartment1Priorities = new Dictionary<char, uint>();
                        uint commonItemPriority = 0;

                        // initialize dictionary using 1st compartment items
                        foreach (char item in compartment1)
                        {
                            uint itemPriority = 0;
                            if (item >= 'a' && item <= 'z')
                            {
                                // lower case priority
                                itemPriority = (uint)item - (uint)'a' + 1;
                            }
                            else if (item >= 'A' && item <= 'Z')
                            {
                                // upper case priority
                                itemPriority = (uint)item - (uint)'A' + 27;
                            }

                            if (!compartment1Priorities.ContainsKey(item))
                            {
                                compartment1Priorities.Add(item, itemPriority);
                            }
                        }

                        // Now look for the common item 
                        foreach(char item in compartment2)
                        {
                            if (compartment1Priorities.ContainsKey(item))
                            {
                                commonItemPriority = compartment1Priorities[item];
                                break;
                            }
                        }

                        // Add rucksack commont item priority
                        overallSum += commonItemPriority;
                    }
                }
            }

            return overallSum;
        }
    }
}