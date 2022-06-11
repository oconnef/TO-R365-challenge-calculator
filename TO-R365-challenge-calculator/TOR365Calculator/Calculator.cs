namespace TOCalculator
{
    public class Calculator
    {
        private IR365Calculator _calculator = null;

        public Calculator(IR365Calculator calculator)
        {
            _calculator = calculator;
        }

        public void ResetDelimiters(string[] delimiters)
        {
            _calculator.ResetDelimiters(delimiters);
        }
        public void ParseInput(string customDelimiter, string input)
        {
            _calculator.ParseInput(customDelimiter, input);
        }

        public string Calculate()
        { 
            return _calculator.Calculate();
        }
    }
}
