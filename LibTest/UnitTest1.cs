namespace LibTest
{
    [TestClass]
    public class UnitTest1
    {

        StringMathParcer mathParcer;
        [TestMethod]
        public void SumTest()
        {
            mathParcer = new StringMathParcer("1+2");
            var result = mathParcer.Result();
            var expected = 3;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void DifferenceTest()
        {
            mathParcer = new StringMathParcer("1-2");
            var result = mathParcer.Result();
            var expected = -1;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void DivisionTest()
        {
            {
                mathParcer = new StringMathParcer("1/2");
                var result = mathParcer.Result();
                var expected = 0.5;

                Assert.AreEqual(result, expected);
            }
        }
    }
}