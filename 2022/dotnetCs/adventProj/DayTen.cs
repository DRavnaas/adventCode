namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayTen : DayTemplate
    {

        internal override string GetInput()
        {
            string testInput = string.Empty;
            bool useTestInput = false;  // test input answer is 13
            if (useTestInput)
            {
                testInput = "addx 15 \n" +
                            "addx -11 \n" +
                            "addx 6 \n" +
                            "addx -3 \n" +
                            "addx 5 \n" +
                            "addx -1 \n" +
                            "addx -8 \n" +
                            "addx 13 \n" +
                            "addx 4 \n" +
                            "noop \n" +
                            "addx -1";
            }
            else
            {
                // output of crt is EHZFZHCZ
                testInput = ReadInputToText("../../DayTenInput.txt");
            }

            return testInput;
        }

        internal override object GetAnswer(string testInput)
        {
            int sums = 0;

            int cycles = 0;
            int regX = 1;       // Register starting value is 1

            if (!String.IsNullOrEmpty(testInput))
            {
                string[] lines = testInput.Split('\n');
                foreach (string line in lines)
                {
                    // Read in instruction
                    int instructionCycles = 0;
                    int regXDelta = 0;

                    string[] instructionDetails = line.Split(' ');
                    if (instructionDetails[0] == "noop")
                    {
                        instructionCycles = 1;
                    }
                    else if (instructionDetails[0] == "addx")
                    {
                        instructionCycles = 2;
                        regXDelta = int.Parse(instructionDetails[1]);
                    }

                    // Iterate on cycles
                    for(int i=0; i< instructionCycles; i++)
                    {
                        int crtPosition = cycles % 40;
                        if ((crtPosition >= regX-1) && (crtPosition <= regX+1))
                        {
                            Console.Write('#');
                        }
                        else {
                            Console.Write('.');
                        }

                        cycles++;
                        if (crtPosition == 39)  // 40 characters written
                        {
                            // New line
                            Console.WriteLine();

                            if (cycles > 240)
                            {
                                // part 1
                                //int signal = cycles * regX;
                                //sums = sums + signal;

                                break;

                            }
                        }
                    }

                    // Calculate new register value
                    regX = regX + regXDelta;

                }

            }

            return sums;
        }
    }
}