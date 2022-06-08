#region

using System.Threading;
using Database.Model;
using NUnit.Framework;
using WPF.Screens;

#endregion

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

        [Test, Apartment(ApartmentState.STA)]
        public void CreateSubject()
        {
            Subject subject = addSubject.CreateSubject();
            Assert.IsNotNull(subject);
        }
    }
}