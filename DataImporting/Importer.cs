using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public class Importer
{
    private static string _defaultStudentFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Students1.xlsx";
    private static string _defaultCoachFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Coaches1.xlsx";

    public static Student[] students;
    public static Coach[] coaches;

    static void Main(string[] args)
    {
        ImportStudentsFromFile();
        ImportCoachesFromFile();
    }

    public static void ImportStudentsFromFile(string fileLocation = "")
    {
        if (fileLocation.Equals("")) fileLocation = _defaultStudentFileLocation;
        DataTable data = GetDataTableFromFile(fileLocation, "Students");
        string[] studentStrings = ReadDataFromDataTable(data);
        List<Student> studentList = new List<Student>();
        for (int i = 0; i < studentStrings.Length; i++)
        {
            Student student = CreateStudentFromDataString(studentStrings[i]);
            if (student != null) studentList.Add(student);
            Console.WriteLine(student);
        }

        students = studentList.ToArray();
    }

    public static void ImportCoachesFromFile(string fileLocation = "")
    {
        if (fileLocation.Equals("")) fileLocation = _defaultCoachFileLocation;
        DataTable data = GetDataTableFromFile(fileLocation, "Coaches");
        string[] coachStrings = ReadDataFromDataTable(data);
        List<Coach> coachList = new List<Coach>();
        for (int i = 0; i < coachStrings.Length; i++)
        {
            Coach coach = CreateCoachFromDataString(coachStrings[i]);
            if (coach != null) coachList.Add(coach);
            Console.WriteLine(coach);
        }

        coaches = coachList.ToArray();
    }

    static DataTable GetDataTableFromFile(string fileLocation, string sheetName)
    {
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=Excel 12.0;";

        var adapter = new OleDbDataAdapter($"SELECT * FROM [{sheetName}$]", connectionString);
        var ds = new DataSet();

        adapter.Fill(ds);
        return ds.Tables[0];
    }

    static string[] ReadDataFromDataTable(DataTable data)
    {
        string[] dataStrings = new string[data.Rows.Count];
        for (int i = 0; i < data.Rows.Count; i++)
        {
            dataStrings[i] = Regex.Replace(data.Rows[i][0].ToString(), " ", "");
        }

        return dataStrings;
    }

    static Student CreateStudentFromDataString(string data)
    {
        string[] dataStrings = data.Split(",");
        if (dataStrings.Length > 4)
        {
            Student student = new Student(dataStrings[0], dataStrings[1], dataStrings[2], dataStrings[3], dataStrings[4]);
            return student;
        }
        return null;
    }

    static Coach CreateCoachFromDataString(string data)
    {
        string[] dataStrings = data.Split(",");
        if (dataStrings.Length > 1)
        {
            Coach coach = new Coach(dataStrings[0], dataStrings[1]);
            return coach;
        }
        return null;
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

public class Coach
{
    public string Naam;
    public string DocentCode;

    public Coach(string naam, string docentCode)
    {
        this.Naam = naam;
        this.DocentCode = docentCode;
    }

    public override string ToString()
    {
        return $"{Naam}, {DocentCode}";
    }
}