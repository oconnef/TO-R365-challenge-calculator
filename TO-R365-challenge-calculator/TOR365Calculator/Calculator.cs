using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TOCalculator
{
    public class Calculator : IR365Calculator
    {
        private int[] _addends = { 0 };
        private int _maxVal;
        private string[] _delimiters;
        private char _delimiterTrimChar;
        private string _delimiterAnyLength;

        public int MaxVal 
        { 
            get => _maxVal;
            set => _maxVal = value;
        }
        public string[] Delimiters
        {
            get => _delimiters;
            set => _delimiters = value;
        }
        public char DelimiterTrimChar
        {
            get => _delimiterTrimChar;
            set => _delimiterTrimChar = value;
        }
        public string DelimiterAnyLength
        {
            get => _delimiterAnyLength;
            set => _delimiterAnyLength = value;
        }


        public void ParseInput(string customDelimiter, string input)
        {
            //check for valid input
            if (string.IsNullOrEmpty(input))
                input = "0";

            //remove any white space
            input.Replace(" ","");

            //add custom delimiter
            string d = Regex.Match(customDelimiter, @"\[([^]]*)\]").Groups[1].Value;
            if (d.Length == 0) 
            {
                //delimiter is only a single character
                d = customDelimiter.TrimStart(DelimiterTrimChar);
            }
            _delimiters = _delimiters.Append(d).ToArray();


            //parse input on delimeters
            string[] stringArgs = input.Split(_delimiters, StringSplitOptions.None);

            _addends = Array.ConvertAll(stringArgs, 
                s => (int.TryParse(s, out int intResult) ? intResult : 0))  //replace invalid, null or empty string with 0
                .Where(t => t <= _maxVal).ToArray();                        //filter 'invalid' numbers > _maxVal

            if (_addends.Any(t => t < 0))
            {
                StringBuilder sb = new StringBuilder();
                _addends.Where(t => t < 0).ToList().ForEach(t => sb.Append(t.ToString() + ","));
                throw new ArgumentException("Negative arguments are not allowed! Invalid arguments are: " + sb.ToString().TrimEnd(','));
            }
        }

        public string Add()
        {
            return _addends.Sum().ToString();
        }
    }
}
