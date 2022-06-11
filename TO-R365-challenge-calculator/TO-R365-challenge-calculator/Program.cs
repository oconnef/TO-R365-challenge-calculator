using System;
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

            string customDelimiter = Console.ReadLine();
            string inputData = Console.ReadLine();

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

            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
}
