namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayThree  : DayTemplate
    {

        internal override object GetAnswer(string testInput)
        {
            uint overallSum = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1 test input score for first line = 16 (p), input summ 8349
                // part 2 test input score for first three lines = 18 (r), input text file answer 2681
                testInput = "vJrwpWtwJgWrhcsFMMfFFhFp\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\nPmmdzqPrVvPwwTWBwg";
            }

            // Process three rucksacks at a time for a group, finding the common item
            string[] ruckSacks = testInput.Split("\n");
            for (uint i = 0; i < ruckSacks.Count(); i = i + 3)
            {
                Dictionary<char, bool> potentialCommonItems = new Dictionary<char, bool>();

                for (uint currentRuckSackIndex = i; currentRuckSackIndex < i + 3; currentRuckSackIndex++)
                {
                    // For the first rucksack, build found item list = potential badge list for others
                    if (currentRuckSackIndex == i)
                    {
                        foreach (char item in ruckSacks[currentRuckSackIndex])
                        {
                            if (!potentialCommonItems.ContainsKey(item))
                            {
                                potentialCommonItems.Add(item, true);
                            }
                        }
                    }
                    else
                    {
                        // For both second and third, store items that we find again
                        Dictionary<char, bool> thisRoundCommon = new Dictionary<char, bool>();
                        foreach (char item in ruckSacks[currentRuckSackIndex])
                        {
                            // If found in last round, then add to "found again" list
                            if (potentialCommonItems.ContainsKey(item))
                            {
                                if (!thisRoundCommon.ContainsKey(item))
                                {
                                    thisRoundCommon.Add(item, true);
                                }
                            }
                        }

                        // Common items we found this round = candidates to track for next round
                        potentialCommonItems = thisRoundCommon;
                    }
                }


                // Should only be one left = the badge.
                uint badgePriority = 0;
                if (potentialCommonItems.Count() == 1)
                {
                    char badge = potentialCommonItems.Keys.First();

                    if (badge >= 'a' && badge <= 'z')
                    {
                        // lower case priority
                        badgePriority = (uint)badge - (uint)'a' + 1;
                    }
                    else if (badge >= 'A' && badge <= 'Z')
                    {
                        // upper case priority
                        badgePriority = (uint)badge - (uint)'A' + 27;
                    }

                }

                // Add rucksack common item priority
                overallSum += badgePriority;
            }

            return (object)overallSum;
        }
    }
}