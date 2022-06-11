namespace TO_R365_CC_Tests
{
    public class Tests
    {
        private TOCalculator.Calculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new TOCalculator.Calculator();
            //calc.MaxArgs = 2;
        }

        //[Test]
        //public void TestMaximumArgumentsIsSuccess()
        //{
        //    string input = "20,20";

        //    Assert.DoesNotThrow(() => calc.ParseInput(input));
        //}

        //[Test]
        //public void TestMaximumArgumentsIsFailure()
        //{
        //    string input = "20,20,20";
        //    Assert.Throws<ArgumentException>(() => calc.ParseInput(input));
        //}

        [Test]
        public void TestInvalidInputsHandledIsSuccess()
        {
            string input = "20,";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("20"));

            input = ",20";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("20"));

            input = "5,tytyt";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("5"));

            input = "";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("0"));
        }

        [Test]
        public void TestSumsIsSuccess()
        {
            string input = "20";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("20"));

            input = "1,5000";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("5001"));

            input = "4,-3";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("1"));

            input = "1,2,3,4,5,6,7,8,9,10,11,12";
            calc.ParseInput(input);
            Assert.That(calc.Add(), Is.EqualTo("78"));

        }
    }
}