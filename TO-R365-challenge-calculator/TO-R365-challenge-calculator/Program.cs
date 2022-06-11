using System;
using System.Text;
using System.Text.RegularExpressions;
using TOCalculator;
using Unity;

namespace TO_R365_challenge_calculator
{
    internal class Program
    {
        private static int maxVal = 1000;
        private static string[] delimiters = new string[] { "\\n", "," };
        private static char delimiterTrimChar = '/';
        private static string delimiterAnyLength = "//[";
        private static bool allowNegatives = false;
        private const string usageMsg = "usage: TO-R365-challenge-calculator.exe [-maxVal {maxVal}] [-allowNegtives {true|false}] [-addDelimiter {delimiter}]";

        static void Main(string[] args)
        {
            Console.WriteLine("R365 Calculator Challenge");
            Console.WriteLine();

            // Test if input arguments were supplied.
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments specified, using default values:");
            }
            else //parse any supplied args
            {
                for (int a=0; a<args.Length; a+=2)
                {
                    switch (args[a]) {
                        case "-maxVal":
                            if (!int.TryParse(args[a+1], out maxVal))
                            {
                                Console.WriteLine("ERROR: -maxVal: " + args[a+1] + " is an invalid value! Try again!");
                                Console.WriteLine(usageMsg);
                                return; 
                            }
                            break;
                        case "-allowNegatives":
                            if (!Boolean.TryParse(args[a + 1], out allowNegatives))
                            {
                                Console.WriteLine("ERROR: -allowNegatives: " + args[a + 1] + " is an invalid value! Try again!");
                                Console.WriteLine(usageMsg);
                                return;
                            }
                            break;
                        case "-addDelimiter":
                            string[] tempDelimiters = new string[delimiters.Length + 1];
                            tempDelimiters[tempDelimiters.Length - 1] = args[a+1];
                            delimiters = tempDelimiters;
                            break;
                        default:
                            Console.WriteLine("ERROR: " + args[a] + " is an invalid argument! Try again!");
                            Console.WriteLine(usageMsg);
                            return;
                    }                
                }                
            }
            Console.WriteLine("Maximum Allowed Value: " + maxVal.ToString());
            Console.WriteLine("Allow Negative Values: " + allowNegatives.ToString());
            Console.Write("Delimiters: {");
            StringBuilder sb = new StringBuilder();
            foreach (string s in delimiters) { sb.Append("\"" + s + "\","); }
            Console.Write(sb.ToString().Substring(0, sb.Length - 1) + "}\r\n\n");

            Console.WriteLine(usageMsg);
            Console.WriteLine("");

            IUnityContainer container = new UnityContainer();
            container.RegisterType<IR365Calculator, Adder>();

            container.RegisterInstance<Adder>(new Adder(maxVal, delimiterTrimChar, delimiterAnyLength, allowNegatives, delimiters));
            
            var calc = container.Resolve<Calculator>();
            
            Console.WriteLine("Enter the data to be calculated in the format \"//{character_delimiter}\\n{numbers}\": ");
            Console.WriteLine("*Custom string delimiter can be specified in the format \"//[{delimiter}]\\n{numbers}\": ");

            while (true)
            {

                calc.ResetDelimiters(delimiters); //reset delimiters in case someone uses a number as delimiter

                string readInput = Console.ReadLine();
                string customDelimiter = ""; 
                string inputData="";
                MatchCollection matches = Regex.Matches(readInput, @"\\n");
                
                //check as single line input or '\n' as delimiter input 
                if (readInput.StartsWith(delimiterTrimChar) && matches.Count > 0)
                {
                    if (matches.Count == 1) // is single line of input...
                    {
                        inputData = readInput.Substring(readInput.IndexOf("\\n") + 2); //start after "//\n"
                        customDelimiter = readInput.Substring(0, readInput.LastIndexOf("\\n"));
                    }
                    // test for \n char is specified as single char delimiter... 
                    else if (matches.Count > 1 && readInput.StartsWith((delimiterTrimChar.ToString() + delimiterTrimChar.ToString() + "\\n")))
                    {
                        inputData = readInput.Substring(6); //start after "//\n"
                        customDelimiter = readInput.Substring(0, 4); //delimeter is //\n
                    }
                    else if (matches.Count > 1 && readInput.StartsWith((delimiterTrimChar.ToString() + delimiterTrimChar.ToString() + "[")))
                    {
                        // \n is one of string delimeters in brackets
                        inputData = readInput.Substring(readInput.LastIndexOf("]\\n") + 3); //start after "]\n"
                        customDelimiter = readInput.Substring(0, readInput.LastIndexOf("]\\n") + 1); //string delimeter(s) has //\n
                    }
                    else //input is multi-line, get data
                    {
                        customDelimiter = readInput;
                        inputData = Console.ReadLine();
                    }
                }
                else {
                    inputData = readInput;
                }

                try
                {
                    calc.ParseInput(customDelimiter, inputData);

                    Console.WriteLine("The result is:" + calc.Calculate());
                }
                catch (ArgumentException argEx)
                {
                    Console.WriteLine(argEx.Message);
                }
                catch (Exception ex)
                {
                   Console.Write(ex.Message);
                }
            }
        }
    }
}
