if __name__ == "__main__":

    useTestInput = False
    inputFile = open("../DayFiveInput.txt")

    inputLines = inputFile.readlines()
    inputFile.close()

    if (useTestInput):
        # test input score is 4, file input result is 841
        testInput = "2-4,6-8 \n" \
                    "2-3,4-5 \n" \
                    "5-7,7-9 \n" \
                    "2-8,3-7 \n" \
                    "6-6,4-6 \n" \
                    "2-6,4-8"
        inputLines = testInput.split('\n')

    rangeOverlapCount = 0
    for line in inputLines:

        if line == '' or line == '\n':
            continue
