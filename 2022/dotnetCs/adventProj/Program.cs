﻿namespace adventProj
{
    class Program
    {
        static void Main(string[] args)
        {
            string testInput = "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000";
            uint testAnswer = 24000;

            uint mostCalories = DayOne.CountElfCalories(testInput);

            Console.WriteLine("\nMost calories: {0}\nExpected answer: {1}", mostCalories, testAnswer);
        }
    }
}