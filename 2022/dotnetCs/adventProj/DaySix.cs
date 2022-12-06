namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DaySix : DayTemplate
    {

        internal override object GetAnswer(string testInput)
        {
            uint retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                testInput = "bvwbjplbgvbhsrlpgdmjqwftvncz"; // part one marker = 5
                // testInput = "nppdvjthqldpwncqszvftbrmjlhg"; // part one marker = 6
                // testInput = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg"; // part one marker = 10
                // testInput = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"; // part one marker = 11
            }

            return retVal;
        }
    }
}