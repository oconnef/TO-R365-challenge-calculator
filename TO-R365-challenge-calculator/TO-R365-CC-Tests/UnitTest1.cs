namespace TO_R365_CC_Tests
{
    public class Tests
    {
        private TOCalculator.Calculator _calc;
        private string[] _delimiters = { "\n", "," };
        private string _customDelimiter = ",";

        [SetUp]
        public void Setup()
        {
            _calc = new TOCalculator.Calculator();
            _calc.Delimiters = _delimiters;
            _calc.DelimiterTrimChar = '/';
            _calc.MaxVal = 1000;
        }

        [Test]
        public void TestNewlineDelimeterIsSuccess()
        {
            string input = "10\n";
            _calc.ParseInput(_customDelimiter, input);

            Assert.That(_calc.Add(), Is.EqualTo("10"));
            
            input = "1\n2,3";
            _calc.ParseInput(_customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("6"));            
        }

        [Test]
        public void TestInvalidInputsHandledIsSuccess()
        {
            string input = "20,";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("20"));

            input = ",20";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("20"));

            input = "5,tytyt";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("5"));

            input = "";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("0"));
        }

        [Test]
        public void TestSumsIsSuccess()
        {
            string input = "20";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("20"));
                        
            input = "1,2,3,4,5,6,7,8,9,10,11,12";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("78"));
        }

        [Test]
        public void TestNegativeArgumentsIsFailure()
        {
            string input = "4,-3";
            Assert.Throws<ArgumentException>(() => _calc.ParseInput(_customDelimiter,input));
        }

        [Test]
        public void TestSumsWithMaxValIsSuccess()
        {
            string input = "1,5000";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("1"));

            input = "2,1001,6";
            _calc.ParseInput(_customDelimiter,input);
            Assert.That(_calc.Add(), Is.EqualTo("8"));
            
        }

        [Test]
        public void TestCustomDelimiterIsSuccess()
        {
            string customDelimiter = "//#"; 
            string input = "2#5";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("7"));

            customDelimiter = "//,";
            input = "2,ff,100";
            _calc.ParseInput(_customDelimiter, input);
            Assert.That(_calc.Add(), Is.EqualTo("102"));

        }
    }
}