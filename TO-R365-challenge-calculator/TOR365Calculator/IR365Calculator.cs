namespace TOCalculator
{
    public interface IR365Calculator
    {
        string[] Delimiters {
            get;
            set;
        } 

        public void ParseInput(string input);

        public string Add();
    }
}
