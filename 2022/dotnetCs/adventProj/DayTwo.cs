namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;

    internal class DayTwo
    {
        // outcome score = 0, 3, 6 = Loss, Draw, Win

        // shapes with mappings: Rock Paper Scissors

        internal enum RoundScore
        {
            X = 0,
            Y = 3,
            Z = 6
        }

         internal enum BaseShape
         {

             NotSet = 0,
             Rock = 1,
             Paper = 2,
             Scissors = 3
         }

         internal enum OpposingShape
         {
             A = BaseShape.Rock,
             B = BaseShape.Paper,
             C = BaseShape.Scissors,

             NotSet = BaseShape.NotSet
         }

        // Used for part 1, not needed for part 2
        //  internal enum PlayedShape
        //  {
        //      X = BaseShape.Rock,
        //      Y = BaseShape.Paper,
        //      Z = BaseShape.Scissors,
        //      NotSet = BaseShape.NotSet
        //  };

        internal static uint GetAnswer(string testInput)
        {
            uint overallScore = 0;

            if (String.IsNullOrEmpty(testInput))
            {
                // overallScore should be 15, test input should be 11767
                testInput = "A Y\nB X\nC Z";
            }

            string[] playedRounds = testInput.Split("\n");

            foreach (string roundDetails in playedRounds)
            {
                if (string.IsNullOrWhiteSpace(roundDetails))
                {
                    continue;
                }

                string[] playedMoves = roundDetails.Split(" ");

                if (playedMoves != null && playedMoves.Count() == 2)
                {
                     OpposingShape opposingMove = OpposingShape.NotSet;
                     RoundScore thisRoundResult = (RoundScore) 0;
                     uint playedMove = 0, thisRoundScore = 0;

                    if (Enum.TryParse(playedMoves[0], out opposingMove))
                    {
                        if (Enum.TryParse(playedMoves[1], out thisRoundResult))
                        {

                            // Was the result a draw?
                            if (thisRoundResult == RoundScore.Y)
                            {   
                                // A draw, we have the same shape as the opposition
                                playedMove = (uint)opposingMove;
                            }
                            else {

                                // Draw already handled, so figure out win/loss moves
                                switch (thisRoundResult) {
                                    case (RoundScore.X):
                                        
                                        // We need to lose
                                        if ((BaseShape)opposingMove == BaseShape.Rock)
                                        {
                                            // Their rock breaks scissors
                                            playedMove = (uint)BaseShape.Scissors;
                                        }
                                        else if ((BaseShape)opposingMove == BaseShape.Paper)
                                        {
                                            // Their paper covers rock
                                            playedMove = (uint)BaseShape.Rock;
                                        }
                                        else {
                                            // Their scissors cut paper
                                            playedMove = (uint)BaseShape.Paper;
                                        }
                                    break;
                                    case (RoundScore.Z):

                                        // We need to win
                                        if ((BaseShape)opposingMove == BaseShape.Rock)
                                        {
                                            // Our paper covers rock
                                            playedMove = (uint)BaseShape.Paper;
                                        }
                                        else if ((BaseShape)opposingMove == BaseShape.Paper)
                                        {
                                            // Our scissors cuts paper
                                            playedMove = (uint)BaseShape.Scissors;
                                        }
                                        else {
                                            // Our rock breaks scissors
                                            playedMove = (uint) BaseShape.Rock;
                                        }
                                    break;
                                }

                            }

                            thisRoundScore = (uint)thisRoundResult + (uint)playedMove;
                            overallScore += thisRoundScore;
                        }
                    }
                }

            }


            return overallScore;
        }

        internal static uint CalculateScore(int playedShape, int opponentShape)
        {
            return 0;
        }
    }
}