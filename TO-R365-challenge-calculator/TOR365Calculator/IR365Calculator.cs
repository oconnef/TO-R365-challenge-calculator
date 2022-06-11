namespace TOCalculator
{
    public interface IR365Calculator
    {
        int MaxArgs {
            get;
        }

        public void ParseInput(string input);

        public string Add();
    }
}
