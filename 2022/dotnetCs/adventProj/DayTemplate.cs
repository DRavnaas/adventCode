namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayTemplate
    {
        internal virtual string GetInput()
        {
            string testInput = string.Empty;
            bool useTestInput = false;  // test input answer is 13
            if (useTestInput)
            {
                testInput = "\n" +
                             "";
            }
            else
            {
                //testInput = ReadInputToText("../../DayXInput.txt");
            }

            return testInput;  
        }

        internal virtual object GetAnswer(string testInput)
        {
            uint retVal = 0;

            string[] lines = testInput.Split('\n');
            foreach(string line in lines)
            {
            }

            return retVal;
        }

        internal static string ReadInputToText(string filename)
        {
            string retVal = String.Empty;

            if (!String.IsNullOrWhiteSpace(filename) && System.IO.File.Exists(filename))
            {
                retVal = System.IO.File.ReadAllText(filename);
            }

            return retVal;
        }
    }
}