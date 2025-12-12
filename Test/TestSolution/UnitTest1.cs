namespace TestSolution
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int result = 1 + 1;
            Assert.Equal(2, result);
        }
    }
}
