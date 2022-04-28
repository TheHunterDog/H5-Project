using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using WPF;
using WPF.Model;

public class ExcelImporter
{
    private static string _defaultStudentFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Students1.xlsx";
    private static string _defaultCoachFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Coaches1.xlsx";

    public static Student[] students;
    public static StudentBegeleider[] coaches;

    static void Main(string[] args)
    {
        ImportStudentsFromFile();
        //ImportCoachesFromFile();
    }

    public static void ImportStudentsFromFile(string fileLocation = "")
    {
        if (fileLocation.Equals("")) fileLocation = _defaultStudentFileLocation;
        DataTable data = GetDataTableFromFile(fileLocation, "Students");
        string[] studentStrings = ReadDataFromDataTable(data);
        List<Student> studentList = new List<Student>();
        using (var context = new StudentBeleidContext())
        {
            for (int i = 0; i < studentStrings.Length; i++)
            {
                Student student = CreateStudentFromDataString(studentStrings[i]);
                if (student != null)
                {
                    studentList.Add(student);
                    string docentCode = studentStrings[i].Split(",")[3].Trim();
                    StudentBegeleider begeleider = context.StudentBegeleiders.Where(x => x.Docentcode.Equals(docentCode)).First();
                    student.StudentbegeleiderId = begeleider.Id;
                    Console.WriteLine(student);
                    context.Students.Add(student);
                    
                }
            }
            context.SaveChanges();
        }



        students = studentList.ToArray();
    }

    public static void ImportCoachesFromFile(string fileLocation = "")
    {
        //StudentBeleidContext context = new StudentBeleidContext();

        if (fileLocation.Equals("")) fileLocation = _defaultCoachFileLocation;
        DataTable data = GetDataTableFromFile(fileLocation, "Coaches");
        string[] coachStrings = ReadDataFromDataTable(data);
        List<StudentBegeleider> coachList = new List<StudentBegeleider>();

        using (var context = new StudentBeleidContext())
        {
            for (int i = 0; i < coachStrings.Length; i++)
            {
                StudentBegeleider coach = CreateCoachFromDataString(coachStrings[i]);
                if (coach != null)
                {
                    coachList.Add(coach);
                    Console.WriteLine(coach);
                    context.StudentBegeleiders.Add(coach);
                }
            }
            context.SaveChanges();
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
            //dataStrings[i] = Regex.Replace(data.Rows[i][0].ToString(), " ", "");
            dataStrings[i] = data.Rows[i][0].ToString();
        }

        return dataStrings;
    }

    static Student CreateStudentFromDataString(string data)
    {
        string[] dataStrings = data.Split(",");
        if (dataStrings.Length > 4)
        {
            //Student student = new Student(dataStrings[0].Trim(), dataStrings[1].Trim(), dataStrings[2].Trim(), dataStrings[3].Trim(), dataStrings[4].Trim());
            Student student = new Student();
            student.Studentnummer = dataStrings[0].Trim();
            student.Voornaam = dataStrings[1].Trim();
            student.Achternaam = dataStrings[2].Trim();
            //student.Studentbegeleider;// = dataStrings[3].Trim();
            student.Klasscode = dataStrings[4].Trim();
            return student;
        }
        return null;
    }

    static StudentBegeleider CreateCoachFromDataString(string data)
    {
        string[] dataStrings = data.Split(",");
        if (dataStrings.Length > 1)
        {
            StudentBegeleider coach = new StudentBegeleider();
            coach.Naam = dataStrings[0].Trim();
            coach.Docentcode = dataStrings[1].Trim();
            return coach;
        }
        return null;
    }
}

/*public class Student
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
}*/