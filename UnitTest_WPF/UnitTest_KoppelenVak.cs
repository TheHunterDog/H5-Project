using Database.Model;
using NUnit.Framework;
using WPF;

namespace UnitTest_WPF_Inplannen
{
    public class SubjectAssigningTests
    {
        SubjectAssigning subjectAssigning;
        [SetUp]
        public void Setup()
        {
            subjectAssigning = new SubjectAssigning();
        }

        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void InitializationNotNull()
        {
            Assert.IsNotNull(subjectAssigning);
            Assert.IsNotNull(subjectAssigning._stagedStudents);
            Assert.IsNotNull(subjectAssigning._stagedSubjects);
        }
    }
}