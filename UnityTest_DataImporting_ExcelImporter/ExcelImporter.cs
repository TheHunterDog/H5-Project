using System.Data;
using System.Diagnostics;
using NUnit.Framework;
using WPF.Model;


namespace UnityTest_DataImporting_ExcelImporter
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase((string)"")]
        [TestCase((string)"nr1")]
        [TestCase((string)"nr1, henk")]
        [TestCase((string)"nr1, henk, test")]
        [TestCase((string)"nr1, henk, test, doc01")]
        [TestCase((string)"nr1, henk, test, doc01, klas1")]
        public void ConvertStringToStudent(string value) // tests creating a student from string
        {
            Student student = ExcelImporter.CreateStudentFromDataString(value);
            
            if(value.Split(',').Length > 4) Assert.IsNotNull(student);
            else Assert.IsNull(student);
        }

        [TestCase((string)"nr1, henk, test, doc01, klas1")]
        public void ConvertStringToStudentValueAssignment(string value) // tests values of a student created from from string
        {
            Student student = ExcelImporter.CreateStudentFromDataString(value);
            Assert.IsNotNull(student);
            Assert.IsTrue(student.Studentnummer.Equals("nr1"));
            Assert.IsTrue(student.Voornaam.Equals("henk"));
            Assert.IsTrue(student.Achternaam.Equals("test"));
            Assert.IsTrue(student.Klasscode.Equals("klas1"));
        }

        [TestCase((string)"")]
        [TestCase((string)"henk")]
        [TestCase((string)"henk, bhk17")]
        public void ConvertStringToCoach(string value) // tests creating a coach from string
        {
            StudentBegeleider coach = ExcelImporter.CreateCoachFromDataString(value);

            if (value.Split(',').Length > 1) Assert.IsNotNull(coach);
            else Assert.IsNull(coach);
        }

        [TestCase((string)"henk, bhk17")]
        public void ConvertStringToCoachValueAssignment(string value) // tests values of a student created from from string
        {
            StudentBegeleider coach = ExcelImporter.CreateCoachFromDataString(value);
            Assert.IsNotNull(coach);
            Assert.IsTrue(coach.Naam.Equals("henk"));
            Assert.IsTrue(coach.Docentcode.Equals("bhk17"));
        }

        [TestCase((string)"", (string)"")]
        [TestCase((string)"d", (string)"")]
        [TestCase((string)"@\"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Students1.xlsx", (string)"")]
        [TestCase((string)"@\"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Coaches1.xlsx", (string)"")]
        public void GetDataTableFromFile(string location, string sheetname)
        {
            DataTable data = ExcelImporter.GetDataTableFromFile(location, sheetname);
            if (location.Equals(ExcelImporter._defaultCoachFileLocation) || location.Equals(ExcelImporter._defaultStudentFileLocation)) Assert.IsNotNull(data);
            else
            {
                Assert.IsNull(data);
            }
        }
        [TestCase((string)"")]
        [TestCase((string)"d")]
        [TestCase((string)"henk")]
        [TestCase((string)"Coaches")]
        public void ReadDataFromDataTable(string sheetname)
        {
            string[] result = ExcelImporter.ReadDataFromDataTable(ExcelImporter.GetDataTableFromFile(@"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Coaches1.xlsx", sheetname));
            if (sheetname.Equals("Coaches"))
            {
                Assert.IsTrue(result.Length > 0);
            }
            else
            {
                Assert.IsTrue(result.Length == 0);
            }
        }
    }
}