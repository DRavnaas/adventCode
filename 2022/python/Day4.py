
class AssignmentRange:

    assignments = set()
    def __init__(self, strToParse):
      self.assignments = set(range(int(strToParse.split('-')[0]), int(strToParse.split('-')[1])+1))

    def overlap(self, target) -> bool:
        #print((self.assignments.start))
        #print(len(self.assignments.intersection(target.assignments)))
        return len(self.assignments.intersection(target.assignments)) > 0

if __name__ == "__main__":

    useTestInput = False
    inputFile = open("../DayFourInput.txt")

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

        # remove line ending
        if line[-1] == '\n':
            line = line[:-1]

        ranges = line.split(',')
        firstRange = AssignmentRange(ranges[0])
        secondRange = AssignmentRange(ranges[1])

        if (firstRange.overlap(secondRange)):
            rangeOverlapCount = rangeOverlapCount + 1

    print(rangeOverlapCount)
