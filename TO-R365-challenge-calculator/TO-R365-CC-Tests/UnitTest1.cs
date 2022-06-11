using TOCalculator;
using Unity;
using Unity.Injection;

namespace TO_R365_CC_Tests
{
    public class Tests
    {
        private UnityContainer container;
        private string[] _delimiters = { "\n", "," };
        private string _dummyCustomDelimiter = ",";
        private char _delimiterTrimChar = '/';
        private string _delimiterAnyLength = "//[";
        private bool _allowNegatives = false;
        private int _maxVal = 1000;

        [SetUp]
        public void Setup()
        {
            container = new UnityContainer();
            container.AddExtension(new Diagnostic());
        }

        [Test]
        public void TestNewlineDelimeterIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "10\n";
            _calc.ParseInput(_dummyCustomDelimiter, input);

            Assert.That(_calc.Calculate(), Is.EqualTo("10+0 = 10"));
            
            input = "1\n2,3";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("1+2+3 = 6"));            
        }

        [Test]
        public void TestInvalidInputsHandledIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "20,";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("20+0 = 20"));

            input = ",20";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("0+20 = 20"));

            input = "5,tytyt";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("5+0 = 5"));

            input = "";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("0 = 0"));
        }

        [Test]
        public void TestSumsIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "20";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("20 = 20"));

            input = "1,2,3,4,5,6,7,8,9,10,11,12";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("1+2+3+4+5+6+7+8+9+10+11+12 = 78"));
        }

        [Test]
        public void TestNegativeArgumentsIsFailure()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "4,-3";
            Assert.Throws<ArgumentException>(() => _calc.ParseInput(_dummyCustomDelimiter, input));
        }

        [Test]
        public void TestNegativeArgumentsIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, true /*allow negatives*/, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "4,-3";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("4+-3 = 1"));
        }

        [Test]
        public void TestSumsWithMaxValIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string input = "1,5000";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("1+0 = 1"));

            input = "2,1001,6";
            _calc.ParseInput(_dummyCustomDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("2+0+6 = 8"));

        }

        [Test]
        public void TestCustomDelimiterSingleCharIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string customDelimiter = "//#";
            string input = "2#5";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("2+5 = 7"));

            customDelimiter = "//,";
            input = "2,ff,100";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("2+0+100 = 102"));

            customDelimiter = "//[";
            input = "2[15";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("2+15 = 17"));

        }

        [Test]
        public void TestCustomDelimiterSingleStringIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);
            
            string customDelimiter = "//[***]";
            string input = "11***22***33";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("11+22+33 = 66"));
        }

        [Test]
        public void TestCustomDelimiterMultipleStringsIsSuccess()
        {
            container.RegisterType<Calculator>("adder", new InjectionConstructor(new object[] { new Adder(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            var _calc = container.Resolve<Calculator>("adder");
            _calc.ResetDelimiters(_delimiters);

            string customDelimiter = "//[*][!!][r9r]";
            string input = "11r9r22*hh*33!!44";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("11+22+0+33+44 = 110"));
        }

        [Test]
        public void TestDifferentMathOperationsIsSuccess()
        {
            container.RegisterType<Calculator>("subtractor", new InjectionConstructor(new object[] { new Subtractor(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, _allowNegatives, _delimiters }));
            container.RegisterType<Calculator>("multiplier", new InjectionConstructor(new object[] { new Multiplier(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, true, _delimiters }));
            container.RegisterType<Calculator>("divider", new InjectionConstructor(new object[] { new Divider(), _maxVal, _delimiterTrimChar, _delimiterAnyLength, true, _delimiters }));

            var _calc = container.Resolve<Calculator>("subtractor");
            _calc.ResetDelimiters(_delimiters);

            string customDelimiter = "//[*][!!][r9r]";
            string input = "11r9r22*hh*33!!44";
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("11-22-0-33-44 = -88"));

            //Test multiply by zero
            _calc = container.Resolve<Calculator>("multiplier");
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("11*22*0*33*44 = 0"));

            input = "11r9r22";
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("11*22 = 242"));

            input = "-11r9r22";
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("-11*22 = -242"));

            //Test division by zero
            _calc = container.Resolve<Calculator>("divider");
            input = "-22r9r0";
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("Attempted to divide by zero. Try Again!"));

            _calc = container.Resolve<Calculator>("divider");
            input = "-22r9r11";
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("-22/11 = -2"));

            _calc = container.Resolve<Calculator>("divider");
            input = "444r9r4*11";
            _calc.ResetDelimiters(_delimiters);
            _calc.ParseInput(customDelimiter, input);
            Assert.That(_calc.Calculate(), Is.EqualTo("444/4/11 = 10"));

        }

    }
}