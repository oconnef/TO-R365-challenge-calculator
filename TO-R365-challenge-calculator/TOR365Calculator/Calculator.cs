namespace TOCalculator
{
    public class Calculator
    {
        private readonly IR365Calculator _calculator = null;

        public Calculator(IR365Calculator calculator, 
            int maxVal,
            char delimiterTrimChar,
            string delimiterAnyLength,
            bool allowNegatives,
            string[] delimiters)
        {
            _calculator = calculator;
            _calculator.MaxVal = maxVal;
            _calculator.DelimiterTrimChar = delimiterTrimChar;
            _calculator.DelimiterAnyLength = delimiterAnyLength;
            _calculator.AllowNegatives = allowNegatives;
            _calculator.Delimiters = delimiters;
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
