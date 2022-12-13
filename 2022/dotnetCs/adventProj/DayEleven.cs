namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal class DayEleven : DayTemplate
    {

        internal override string GetInput()
        {
            string testInput = string.Empty;
            bool useTestInput = true;  // test input answer is 13
            if (useTestInput)
            {
                testInput = 
                   "Monkey 0: \n" +
                   "  Starting items: 79, 98 \n" +
                   "  Operation: new = old * 19 \n" +  // worry level before = old, new = after inspection
                   "  Test: divisible by 23 \n" +      // worry test
                   "    If true: throw to monkey 2 \n" +
                   "    If false: throw to monkey 3 \n" +
                   "\n" +
                    "Monkey 1:  \n" +
                    "  Starting items: 54, 65, 75, 74  \n" +
                    "  Operation: new = old + 6  \n" +
                    "  Test: divisible by 19  \n" +
                    "    If true: throw to monkey 2  \n" +
                    "    If false: throw to monkey 0 \n" +
                    "\n" +
                    "Monkey 2:  \n" +
                    "  Starting items: 79, 60, 97  \n" +
                    "  Operation: new = old * old  \n" +
                    "  Test: divisible by 13  \n" +
                    "    If true: throw to monkey 1  \n" +
                    "    If false: throw to monkey 3 \n" +
                    "\n" +
                    "Monkey 3:  \n" +
                    "  Starting items: 74  \n" +
                    "  Operation: new = old + 3  \n" +
                    "  Test: divisible by 17  \n" +
                    "    If true: throw to monkey 0  \n" +
                    "    If false: throw to monkey 1 \n";
            }
            else
            {
                // output of crt is EHZFZHCZ
                testInput = ReadInputToText("../../DayElevenInput.txt");
            }

            return testInput;
        }

        internal override object GetAnswer(string testInput)
        {
            Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();

            long overallMonkeyBusiness = 0;

            if (!String.IsNullOrEmpty(testInput))
            {
                string[] lines = testInput.Split('\n');
                int lineCount = 0;
                string[] monkeyInput = new string[6];
                foreach (string line in lines)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        // Gather six lines
                        monkeyInput[lineCount] = line;
                        lineCount++;

                        if (lineCount == 6)
                        {
                            Monkey newMonkey = (Monkey.ParseInputLines(monkeyInput));
                            monkeys.Add(newMonkey.Id, newMonkey);
                            lineCount = 0;

                        }
                     }
                }

                // We have our starting monkeys, now start cycling through them
                int cycles = 0;
                do {
                    
                    foreach(Monkey monkey in monkeys.Values)
                    {
                        foreach (int item in monkey.items)
                        {
                            int result = monkey.operation(item, monkey.operand);  // take a look at next item's worry level in array

                            result = result / 3;  // divide worry by three
                            monkey.numInspections++;

                            // throw item to next monkey with current worry level
                            if (monkey.divisorTest(result, monkey.divisor))
                            {
                                monkeys[monkey.nextMonkeyTrue].items.Add(result);
                            }
                            else {
                                monkeys[monkey.nextMonkeyFalse].items.Add(result);
                            }
                        }

                    }

                    cycles ++;
                } while (cycles < 20);
            }

            // Get the top two
            var sortedInspections = monkeys.Select( x => x.Value.numInspections ).ToList();
            sortedInspections.Sort();
            sortedInspections = sortedInspections.TakeLast(2).ToList();
            overallMonkeyBusiness = sortedInspections[0] * sortedInspections[1];
                                   
            return overallMonkeyBusiness;
        }
    }

    internal class Monkey
    {
        public ArrayList items = new ArrayList();

        public int Id
        {
            get; set;
        }

        public int numInspections = 0;

        public System.Func<int, int, int> operation = (x, y) => x;  // additional worry factor
        
        public System.Func<int, int, bool> divisorTest = (x, y) => x % 1 == 0;  // Divisor condition lambda

        public int operand = 0;
        public int divisor = 1;
        public int nextMonkeyTrue;
        public int nextMonkeyFalse;

        public static Monkey ParseInputLines(string[] textInput)
        {
            Monkey parsedMonkey = new Monkey();

            string itemText = textInput[0].Substring("Monkey ".Count());
            parsedMonkey.Id = int.Parse(itemText[0].ToString());

            itemText = textInput[1].Substring("  Starting items:".Count());
            string[] inputText = itemText.Split(", ");
            foreach(string item in inputText)
            {
                if (String.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                int newItem = int.Parse(item);
                parsedMonkey.items.Add(newItem);
            }

            string operationText = textInput[2].Substring("  Operation: new = ".Count());
            inputText = operationText.Split(' ');
            object operand1 = null;
            object operand2 = null;
            bool isAddition = true;
            foreach(string operands in inputText)
            {
                if (String.IsNullOrWhiteSpace(operands))
                {
                    continue;
                }     
                else {

                    // fill in lambda function - we need an operator ('*' or '+')
                    // old * 7
                    // old + old
                    // old + 5
                    switch(operands) {
                        case "old": 
                            if (operand1 == null)
                            {
                                operand1 = operands;
                            }
                            else {
                                operand2 = operands;
                            }
                            break;
                        case "+":
                            isAddition = true;
                            break;
                        case "*":
                            isAddition = false;
                            break;
                         default:
                            // a number
                            int tempNum = int.Parse(operands);
                            
                            if (operand1 == null)
                            {
                                operand1 = tempNum;  // not sure this is in input, usually old first
                            }
                            else {
                                operand2 = tempNum;
                            }
                            break;

                    }           
  
                }           
            }

            int parsedNum = 0;
            if ((operand1.ToString() == "old") && (operand2.ToString() == "old"))
            {
                if (isAddition)
                {
                    parsedMonkey.operand = 0;  // this value won't be used
                    parsedMonkey.operation = (x, y) => 
                                                   x + x;
                }
                else {
                    parsedMonkey.operand = 0; // this value won't be used
                    parsedMonkey.operation = (x, y) => 
                                                    x * x;
                }
            }
            else {
                parsedMonkey.operand = (int)operand2;
                if (isAddition)
                {
                    parsedMonkey.operation = (x, y) => 
                                                x + y;
                }
                else
                {
                    parsedMonkey.operation = (x, y) => 
                                                x * y;
                }
            }

            string testText = textInput[3].Substring("  Test: divisible by ".Count());
            parsedNum = int.Parse(testText);
            parsedMonkey.divisor = parsedNum;
            parsedMonkey.divisorTest = (x, y) => 
                                x % y == 0;

            testText = textInput[4].Substring("    If true: throw to monkey ".Count());
            parsedMonkey.nextMonkeyTrue = int.Parse(testText);

            
            testText = textInput[5].Substring("    If false: throw to monkey ".Count());
            parsedMonkey.nextMonkeyFalse = int.Parse(testText);

            return parsedMonkey;
        }
    }
}