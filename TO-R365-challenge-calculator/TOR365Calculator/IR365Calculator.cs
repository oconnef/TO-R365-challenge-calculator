﻿namespace TOCalculator
{
    public interface IR365Calculator
    {
        //removed per feature 2
        //int MaxArgs {
        //    get;
        //}

        public void ParseInput(string input);

        public string Add();
    }
}
