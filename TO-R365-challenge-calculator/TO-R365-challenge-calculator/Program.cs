using System;
using TOCalculator;

namespace TO_R365_challenge_calculator
{
    internal class Program
    {
        private const int maxArgs = 2;

        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            
            Console.WriteLine("R365 Calculator Challenge");

            Console.WriteLine("Enter the data to be calculated in the format \"x,x\": ");

            string inputData = Console.ReadLine();

            try
            {
                c.ParseInput(inputData);
                
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
