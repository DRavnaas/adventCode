namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DaySix : DayTemplate
    {

        internal override object GetAnswer(string testInput)
        {
            int retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                // part 1 test input file answer = 1929
                
                //testInput = "mjqjpqmgbljsphdztnvjfqwrcgsmlb"; // part one number = 7
                //testInput = "bvwbplbgvbhsrlpgdmjqwftvncz"; // part one marker = 5
                //testInput = "nppdvjthqldpwncqszvftbrmjlhg"; // part one marker = 6
                //testInput = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"; // part one marker = 10
                testInput = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"; // part one marker = 11
            }

            string[] transmissions = testInput.Split("\n");
            Dictionary<char, int> lastFour = new Dictionary<char, int>();

            foreach (string transmission in transmissions)
            {
                for (int i = 0; i < transmission.Count(); i++)
                {
                    // Keep a Dictionary to contain last four chars
                    // the number of keys is the number of unique characters

                    // get a new character to add...
                    char newChar = transmission[i];

                    if (i >= 4)
                    {
                        // "shift off" char before adding another one = decrement or remove char
                        char startingChar = transmission[i-4];
                        if (lastFour.ContainsKey(startingChar))
                        {
                            // Decrement count
                            lastFour[startingChar]--;

                            if (lastFour[startingChar] == 0)
                            {
                                // If count is zero, remove char/key
                                lastFour.Remove(startingChar);
                            }
                        }
                        else {
                            lastFour.Remove(startingChar);
                        }
                    }
                    
                    
                    // Add in our new char - which could be unique (= new key) or a dupe (increment key reference value)
                    if (lastFour.ContainsKey(newChar))
                    {
                        // This is a dupe within the four chars; increment count
                        lastFour[newChar]++;
                            
                    }
                    else {
                        // Not a dupe, add with reference count 1
                        lastFour.Add(newChar, 1);
                    }

                    // Are we done?  keys = chars = unique characters in our four key dictionary
                    if (lastFour.Keys.Count() == 4)
                    {
                        retVal = i+1;
                        break;
                    }
                    
                }
            }

            return retVal;
        }
    }
}