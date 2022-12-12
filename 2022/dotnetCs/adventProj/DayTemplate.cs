namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayTemplate
    {
        internal virtual string GetInput()
        {
            return string.Empty;
        }

        internal virtual object GetAnswer(string testInput)
        {
            uint retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                testInput = "";
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