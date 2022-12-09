
# for a given character, calculate its badge priority
def getBadgePriority(badge):
    badgeOrdinal = ord(badge)
    if badge >= 'a' and badge <= 'z':
        return badgeOrdinal - ord('a') + 1
    elif badge >= 'A' and badge <= 'Z':
        return badgeOrdinal - ord('A') + 26 + 1
    else:
        return 0


useTestInput = False
inputFile = open("../DayThreeInput.txt")

inputLines = inputFile.readlines()
inputFile.close()

if (useTestInput):
    # test input score is 70, file input result is 2681
    testInput = "vJrwpWtwJgWrhcsFMMfFFhFp\n" \
                 "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\n" \
                 "PmmdzqPrVvPwwTWBwg\n" \
                 "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\n" \
                 "ttgJtRGJQctTZtZT\n" \
                 "CrZsJsPPZsGzwwsLwLmpwMDw"
    inputLines = testInput.split('\n')

ruckSackCount = 1
candidates = {}
lastCandidates = {}
badgeSum = 0


for line in inputLines:

    if line == '' or line == '\n':
        continue

    # remove line ending
    if line[-1] == '\n':
        line = line[:-1]

    # get set of characters common between 1 and 2, or 1 intersected wit 2 and 3
    lastCandidates = candidates
    if ruckSackCount > 1:
        candidates = set(line).intersection(lastCandidates)
    else:
        candidates = line

    ruckSackCount = ruckSackCount + 1
    if ruckSackCount > 3:
        # start new set of 3
        print(candidates);

        # There should only be one item left in the set - pop it
        # and calculate it's priority
        badgeSum = badgeSum + getBadgePriority(candidates.pop())
        if len(candidates) >= 1:
            print("warning: more than one character overlap")

        ruckSackCount = 1
        lastCandidates.clear()
        candidates.clear()

# print out running total
print(badgeSum)