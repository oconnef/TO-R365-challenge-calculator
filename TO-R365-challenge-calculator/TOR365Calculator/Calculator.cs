using System;
using System.Linq;

namespace TOCalculator
{
    public class Calculator : IR365Calculator
    {
        private int[] _addends = { 0 };
        private int _maxVal;
        private string[] _delimiters;

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

        public void ParseInput(string input)
        {
            //check for valid input
            if (string.IsNullOrEmpty(input))
                input = "0";

            //remove any white space
            input.Replace(" ","");

            //parse input on delimeters
            string[] stringArgs = input.Split(_delimiters, StringSplitOptions.None);

            _addends = Array.ConvertAll(stringArgs, 
                s => (int.TryParse(s, out int intResult) ? intResult : 0))  //replace invalid, null or empty string with 0
                .Where(t => t <= _maxVal).ToArray();                        //filter 'invalid' numbers > _maxVal
        }

        public string Add()
        {
            return _addends.Sum().ToString();
        }
    }
}
