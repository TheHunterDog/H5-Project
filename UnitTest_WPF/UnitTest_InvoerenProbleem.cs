using NUnit.Framework;
using WPF;

namespace UnitTest_WPF_Inplannen
{
    internal class UnitTest_InvoerenProbleem
    {
        ProblemSubmitting problemSubmitting;
        [SetUp]
        public void Setup()
        {
            problemSubmitting = new ProblemSubmitting();
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 2)]
        [TestCase("-2", 2)]
        public void PriorityTextToInt(string value, int expected)
        {
            Assert.IsTrue(problemSubmitting.PriorityTextToInt(value) == expected);
        }
    }
}
