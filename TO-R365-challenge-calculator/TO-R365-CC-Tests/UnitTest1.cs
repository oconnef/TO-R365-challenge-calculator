namespace TO_R365_CC_Tests
{
    public class Tests
    {
        private TOCalculator.Calculator _calc;
        private string[] _delimiters = { "\n", "," };
        private string _dummyCustomDelimiter = ",";
        private string delimiterAnyLength = "//[";

        [SetUp]
        public void Setup()
        {
            _calc = new TOCalculator.Calculator();
            _calc.Delimiters = _delimiters;
            _calc.DelimiterTrimChar = '/';
            _calc.DelimiterAnyLength = delimiterAnyLength;
            _calc.MaxVal = 1000;
        }

        [Test]
        public void TestNewlineDelimeterIsSuccess()
        {
            string input = "10\n";
            _calc.ParseInput(_dummyCustomDelimiter, input);

            Assert.That(_calc.Add(), Is.EqualTo("10+0 = 10"));
            
            input = "1\n2,3";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("1+2+3 = 6"));            
        }

        [Test]
        public void TestInvalidInputsHandledIsSuccess()
        {
            string input = "20,";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("20+0 = 20"));

            input = ",20";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("0+20 = 20"));

            input = "5,tytyt";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("5+0 = 5"));

            input = "";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("0 = 0"));
        }

        [Test]
        public void TestSumsIsSuccess()
        {
            string input = "20";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("20 = 20"));
                        
            input = "1,2,3,4,5,6,7,8,9,10,11,12";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("1+2+3+4+5+6+7+8+9+10+11+12 = 78"));
        }

        [Test]
        public void TestNegativeArgumentsIsFailure()
        {
            string input = "4,-3";
            Assert.Throws<ArgumentException>(() => _calc.ParseInput(_dummyCustomDelimiter, input));
        }

        [Test]
        public void TestSumsWithMaxValIsSuccess()
        {
            string input = "1,5000";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("1+0 = 1"));

            input = "2,1001,6";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("2+0+6 = 8"));
            
        }

        [Test]
        public void TestCustomDelimiterSingleCharIsSuccess()
        {
            string customDelimiter = "//#"; 
            string input = "2#5";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("2+5 = 7"));

            customDelimiter = "//,";
            input = "2,ff,100";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("2+0+100 = 102"));

            customDelimiter = "//[";
            input = "2[15";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("2+15 = 17"));

        }

        [Test]
        public void TestCustomDelimiterSingleStringIsSuccess()
        {
            string customDelimiter = "//[***]";
            string input = "11***22***33";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("11+22+33 = 66"));
        }

        [Test]
        public void TestCustomDelimiterMultipleStringsIsSuccess()
        {
            string customDelimiter = "//[*][!!][r9r]";
            string input = "11r9r22*hh*33!!44";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("11+22+0+33+44 = 110"));
        }
    }
}