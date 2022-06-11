using System;
using System.Linq;
using System.Text;

namespace TOCalculator
{
    public class Calculator : IR365Calculator
    {
        // removed per feature 2
        // private int _maxArgs;
        private int[] _addends = { 0 };

        //public int MaxArgs
        //{
        //    get => _maxArgs;
        //    set => _maxArgs = value;
        //}

        private string[] _delimiters;
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
            //if(stringArgs.Length > _maxArgs)
            //    throw new ArgumentException("Too many arguments passed to Calculator. Max number of arguments is " + _maxArgs.ToString() + " Try Again...");

            _addends = Array.ConvertAll(stringArgs,
                s => (int.TryParse(s, out int intResult) ? intResult : 0)); //replace invalid, null or empty string with 0

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
