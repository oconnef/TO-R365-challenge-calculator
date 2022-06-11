namespace TOCalculator
{
    public interface IR365Calculator
    {
        public int MaxVal
        {
            get;set;
        }
        public string[] Delimiters
        {
            get; set;
        }
        public char DelimiterTrimChar
        {
            get; set;
        }
        public string DelimiterAnyLength
        {
            get; set;
        }
        public bool AllowNegatives
        {
            get; set;
        }

        public void ResetDelimiters(string[] delimiters);

        public void ParseInput(string customDelimiter, string input);

        public string Calculate();
    }
}
