// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public class Importer
{
    private static string _defaultFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Students1.xlsx";

    public static Student[] students;

    static void Main(string[] args)
    {
        ImportStudentsFromFile();
    }

    public static void ImportStudentsFromFile(string fileLocation = "")
    {
        if (fileLocation.Equals("")) fileLocation = _defaultFileLocation;

        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=Excel 12.0;";

        var adapter = new OleDbDataAdapter("SELECT * FROM [Students$]", connectionString);
        var ds = new DataSet();

        adapter.Fill(ds);
        System.Data.DataTable data = ds.Tables[0];

        for (int i = 0; i < data.Rows.Count; i++)
        {
            string info = Regex.Replace(data.Rows[i][0].ToString(), " ", "");
            string[] dataStrings = info.Split(",");
            if (dataStrings.Length > 4)
            {
                Student student = new Student(dataStrings[0], dataStrings[1], dataStrings[2], dataStrings[3], dataStrings[4]);
                Console.WriteLine(student);
            }
        }
    }
}

public class Student
{
    public string Number;
    public string FirstName;
    public string LastName;
    public string Coach;
    public string ClassCode;

    public Student(string number, string firstName, string lastName, string coach, string classCode)
    {
        this.Number = number;
        this.FirstName = firstName; 
        this.LastName = lastName;
        this.Coach = coach;
        this.ClassCode = classCode;
    }

    public override string ToString()
    {
        return $"{Number}, {FirstName}, {LastName}, {Coach}, {ClassCode}";
    }
}