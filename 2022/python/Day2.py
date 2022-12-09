inputFile = open("../DayTwoInput.txt")

inputLines = inputFile.readlines()
inputFile.close()

# test input score is 15, file input score is 11767
testInput = "A Y\nB X\nC Z"
inputLines = testInput.split('\n')

runningScore = 0
inputBreak = True

for line in inputLines:

    mymove = 0
    theirmove = 0
    score = 0

    if line == '' or line == '\n':
        inputBreak = True
        continue

    # get the opponent move 
    if line[0] == 'A':      # Rock
        theirMove = 1
    elif line[0] == 'B':    # Paper
        theirMove = 2
    elif line[0] == 'C':    # Scissors
        theirMove = 3

    if line[2] == 'X':      # loss
        score = 0
        if theirMove == 1:  # their rock breaks scissors
            mymove = 3  
        elif theirMove == 2: # their paper covers rock
            mymove = 1
        else:               # their scissor cut paper
            mymove = 2
    elif line[2] == 'Y':    # draw
        score = 3
        mymove = theirMove  #same move as theirs
    elif line[2] == 'Z':    # win
        score = 6
        if theirMove == 1:  # their rock covered by paper
            mymove = 2  
        elif theirMove == 2: # their paper cut by scissors
            mymove = 3
        else:               # their scissors broken by rock
            mymove = 1

    runningScore = runningScore + mymove + score

print(runningScore)