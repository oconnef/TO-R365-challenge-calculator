using System;
using System.Text.RegularExpressions;
using TOCalculator;

namespace TO_R365_challenge_calculator
{
    internal class Program
    {
        private const int maxVal = 1000;
        private static string[] delimiters = new string[] { "\n", "," };
        private static char delimiterTrimChar = '/';
        private static string delimiterAnyLength = "//[";

        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            c.Delimiters = delimiters;
            c.DelimiterTrimChar = delimiterTrimChar;
            c.DelimiterAnyLength = delimiterAnyLength;
            c.MaxVal = maxVal;

            Console.WriteLine("R365 Calculator Challenge");
            Console.WriteLine();
            Console.WriteLine("Enter the data to be calculated in the format \"//{character_delimiter}\\n{numbers}\": ");
            Console.WriteLine("*Custom string delimiter can be specified in the format \"//[{delimiter}]\\n{numbers}\": ");
            Console.WriteLine("**Maximum value is: " + maxVal.ToString());

            while (true)
            {
                c.Delimiters = delimiters; //reset delimiters in case someone uses a number as delimiter

                string readInput = Console.ReadLine();
                string customDelimiter = ""; 
                string inputData="";
                MatchCollection matches = Regex.Matches(readInput, @"\\n");
                
                //check as single line input or '\n' as delimiter input 
                if (readInput.StartsWith(delimiterTrimChar) && matches.Count > 0)
                {
                    // test for \n char is specified as single char delimiter... 
                    if (matches.Count > 1 && readInput.StartsWith((delimiterTrimChar.ToString() + delimiterTrimChar.ToString() + "\\n")))
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
                    c.ParseInput(customDelimiter, inputData);

                    Console.WriteLine("The result is:" + c.Add());
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
