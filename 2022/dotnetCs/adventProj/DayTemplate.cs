namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayTemplate
    {

        internal virtual object GetAnswer(string testInput)
        {
            uint retVal = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                testInput = "";
            }

            return retVal;
        }
    }
}