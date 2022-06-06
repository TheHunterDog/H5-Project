using Database.Model;
using NUnit.Framework;
using WPF;

namespace UnitTest_WPF_Inplannen
{
    public class Tests
    {
        AddSubject addSubject;
        [SetUp]
        public void Setup()
        {
            addSubject = new AddSubject();
        }

        [Test, Apartment(System.Threading.ApartmentState.STA)]
        public void CreateSubject()
        {
            Subject subject = addSubject.CreateSubject();
            Assert.IsNotNull(subject);
        }
    }
}