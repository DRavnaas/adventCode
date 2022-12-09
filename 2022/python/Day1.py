import os
import numpy as np
from numpy import loadtxt

#cwd = os.getcwd()
#print(cwd)

# part two answer is 206152
caloriesFile = open("../DayOneInput.txt")

calorieLines = caloriesFile.readlines()
caloriesFile.close()

# top three are 10000 11000 24000, sum is 45000
#testInput = "1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000"
#calorieLines = testInput.split('\n')

elfCalories = []
newelf = True

for line in calorieLines:

    if line == '' or line == '\n':
        newelf = True
        continue

    if newelf == True:
        elfCalories.append(0)
        newelf = False

    calories = int(line)
    elfCalories[-1] = elfCalories[-1] + calories
    

elfCalories.sort()
print(sum(elfCalories[-3:]))

    


