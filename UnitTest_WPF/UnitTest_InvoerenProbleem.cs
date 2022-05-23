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

        [TestCase("0", 0), Apartment(System.Threading.ApartmentState.STA)]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 2)]
        [TestCase("-2", 2)]
        public void PriorityTextToInt(string value, int expected)
        {
            Assert.IsTrue(problemSubmitting.PriorityTextToInt(value) == expected);
        }

        [TestCase(-1, 0, true), Apartment(System.Threading.ApartmentState.STA)]
        [TestCase(0, 0, true)]
        [TestCase(105, -1, true)]
        [TestCase(105, 1, false)]
        public void CreateStudentProblem(int studentId, int teacherId, bool isNull)
        {
            if (isNull) Assert.IsNull(problemSubmitting.CreateStudentProblem(studentId, teacherId));
            else Assert.IsNotNull(problemSubmitting.CreateStudentProblem(studentId, teacherId));
        }

        [TestCase(100, "S0000000"), Apartment(System.Threading.ApartmentState.STA)]
        [TestCase(101, "S0000001")]
        [TestCase(90, "")]
        public void GetStudentNumberFromStudentId(int studentId, string studentNumber)
        {
            Assert.IsTrue(problemSubmitting.GetStudentNumberFromStudentId(studentId).Equals(studentNumber));
        }
    }
}
