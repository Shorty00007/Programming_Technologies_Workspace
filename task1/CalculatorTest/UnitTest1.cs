namespace CalculatorTest

{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Calculator calculator = new Calculator();
            int sum = calculator.Add(5, 10);
            Assert.AreEqual(15, sum);
        }
    }
}