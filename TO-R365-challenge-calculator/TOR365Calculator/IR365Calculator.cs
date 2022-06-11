namespace TOCalculator
{
    public interface IR365Calculator
    {
        string[] Delimiters {
            get;
            set;
        }

        public void ResetDelimiters(string[] delimiters);

        public void ParseInput(string customDelimiter, string input);

        public string Calculate();
    }
}
