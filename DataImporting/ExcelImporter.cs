using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore.Internal;
using WPF;
using WPF.Model;

public class ExcelImporter
{
    // default file locations, for testing purposes only
    public static string _defaultStudentFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Students1.xlsx";
    public static string _defaultCoachFileLocation =
        @"C:/Users/evert/source/repos/H5-Project/DataImporting/Files/Coaches1.xlsx";

    // ran from console, for testing purposes only
    static void Main(string[] args)
    {
        // print coaches and students
        PrintStudents();
        PrintCoaches();
        // remove all students and coaches
        RemoveStudentsAndCoaches();
        Console.WriteLine("Removed All:");
        PrintStudents();
        PrintCoaches();
        ImportCoachesFromFile();
        ImportStudentsFromFile();
        Console.WriteLine("New:");
        PrintStudents();
        PrintCoaches();

    }

    public static void RemoveStudentsAndCoaches()
    {
        using (var context = new StudentBeleidContext())
        {
            // remove studentbegeleiders first to avoid relation conflicts
            context.StudentBegeleiders.RemoveRange(context.StudentBegeleiders); 
            // remove students
            context.Students.RemoveRange(context.Students); 
            // save changes to database
            context.SaveChanges(); 
        }
    }

    // TEST
    public static void PrintStudents()
    {
        Console.WriteLine("\n All Students in database:");
        using (var context = new StudentBeleidContext())
        {
            // obtain students from database
            List<Student> students = context.Students.ToList(); 
            for (int i = 0; i < students.Count; i++)
            {
                // get student from list
                Student student = students.ElementAt(i); 
                // print student data
                Console.WriteLine(student); 
            }
        }
    }

    public static void PrintCoaches()
    {
        Console.WriteLine("\n All Coaches in database:");
        using (var context = new StudentBeleidContext())
        {
            // obtain coaches from database
            List<StudentBegeleider> coaches = context.StudentBegeleiders.ToList(); 
            for (int i = 0; i < coaches.Count; i++)
            {
                // get coach from list
                StudentBegeleider coach = coaches.ElementAt(i); 
                // print coach data
                Console.WriteLine(coach); 
            }
        }
    }
    // TEST

    public static void ImportStudentsFromFile(string fileLocation = "")
    {
        // set fileLocation
        if (fileLocation.Equals("")) fileLocation = _defaultStudentFileLocation;
        // get data from file
        DataTable data = GetDataTableFromFile(fileLocation, "Students");
        // if data is null, file could not be found
        if(data == null) return;
        // get strings with student data
        string[] studentStrings = ReadDataFromDataTable(data);
        // save data to database
        using (var context = new StudentBeleidContext())
        {
            for (int i = 0; i < studentStrings.Length; i++)
            {
                // convert string to object of type Student
                Student student = CreateStudentFromDataString(studentStrings[i]);
                // add student to database
                if (student != null)
                {
                    // obtain coach(docentcode) from datastring
                    string docentCode = studentStrings[i].Split(",")[3].Trim();
                    // get count of coaches which have a matching docentcode(max 1);
                    int countBeg = context.StudentBegeleiders.Where(x => x.Docentcode.Equals(docentCode)).Count();
                    // if no matching coach is found, create a new one with an undefined name(can be overwritten when added to excel file with the same docentcode)
                    if (countBeg < 1) 
                    {
                        // create name for new coach
                        string defaultStudentCoachName = "Undefined name";
                        // create datastring
                        string newStudentBegeleiderDataString = $"{defaultStudentCoachName}, {docentCode}";
                        // create new coach from datastring
                        StudentBegeleider newStudentBegeleider = CreateCoachFromDataString(newStudentBegeleiderDataString);
                        // add coach to database
                        context.StudentBegeleiders.Add(newStudentBegeleider);
                        // savve changes to database
                        context.SaveChanges();
                    }
                    StudentBegeleider begeleider = context.StudentBegeleiders.Where(x => x.Docentcode.Equals(docentCode)).First();
                    student.StudentbegeleiderId = begeleider.Id;
                    // get whether there is already a student with the same code in the database
                    int count = context.Students.Where(x => x.Studentnummer.Equals(student.Studentnummer)).Count();
                    // when yes, update student with new name etc.
                    if (count > 0)
                    {
                        // get student from database
                        var result = context.Students.Where(x => x.Studentnummer.Equals(student.Studentnummer)).First();
                        // edit student parameters
                        result.Voornaam = student.Voornaam;
                        result.Achternaam = student.Achternaam;
                        result.Klasscode = student.Klasscode;
                        result.StudentbegeleiderId = student.StudentbegeleiderId;
                        result.Studentnummer = student.Studentnummer; // redundant
                    }
                    // when no, add the student to the database
                    else
                    {
                        // add student to database
                        context.Students.Add(student);
                    }
                }
            }
            // save changes to database
            context.SaveChanges();
        }
    }

    public static void ImportCoachesFromFile(string fileLocation = "")
    {
        // set fileLocation
        if (fileLocation.Equals("")) fileLocation = _defaultCoachFileLocation;
        // get data from file
        DataTable data = GetDataTableFromFile(fileLocation, "Coaches");
        // if data is null, file could not be found
        if (data == null) return;
        // get strings with coach data
        string[] coachStrings = ReadDataFromDataTable(data);
        // save data to database
        using (var context = new StudentBeleidContext())
        {
            for (int i = 0; i < coachStrings.Length; i++)
            {
                // convert string to object of type Coach
                StudentBegeleider coach = CreateCoachFromDataString(coachStrings[i]);
                // add coach to database
                if (coach != null)
                {
                    //Console.WriteLine(coach);
                    // get whether there is already a coach with the same code in the database
                    int count = context.StudentBegeleiders.Where(x => x.Docentcode.Equals(coach.Docentcode)).Count();
                    // when yes, update coach with new name etc.
                    if (count > 0)
                    {
                        //Console.WriteLine("Update");
                        var result = context.StudentBegeleiders.Where(x => x.Docentcode.Equals(coach.Docentcode)).First();
                        result.Naam = coach.Naam;
                        result.Docentcode = coach.Docentcode; // redundant
                    }
                    // when no, add the coach to the database
                    else
                    {
                        //Console.WriteLine("Add");
                        context.StudentBegeleiders.Add(coach);
                    }
                }
            }
            // save changes to database
            context.SaveChanges();
        }
    }

    public static DataTable GetDataTableFromFile(string fileLocation, string sheetName)
    {
        // create connection string
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=Excel 12.0;";
        // create command string
        var commandString = $"SELECT * FROM [{sheetName}$]";
        OleDbDataAdapter adapter;
        try
        {
            // create adapter
            adapter = new OleDbDataAdapter(commandString, connectionString);
        }
        catch (Exception e)
        {
            return null;
        }
        // create dataset
        var ds = new DataSet();
        // fill dataset
        adapter.Fill(ds);
        // return dataset
        return ds.Tables[0];

    }

    public static string[] ReadDataFromDataTable(DataTable data)
    {
        // create string array
        string[] dataStrings = new string[data.Rows.Count];
        // fill string array
        for (int i = 0; i < data.Rows.Count; i++)
        {
            // read row from datatable
            dataStrings[i] = data.Rows[i][0].ToString();
        }
        // return string array
        return dataStrings;
    }

    public static Student CreateStudentFromDataString(string data)
    {
        // create string array with split items from string
        string[] dataStrings = data.Split(",");
        // check whether all nessecary info is filled in
        // when yes, create student
        if (dataStrings.Length > 4)
        {
            // create student object
            Student student = new ();
            // fill in info
            student.Studentnummer = dataStrings[0].Trim();
            student.Voornaam = dataStrings[1].Trim();
            student.Achternaam = dataStrings[2].Trim();
            student.Klasscode = dataStrings[4].Trim();
            // return student
            return student;
        }
        // when no, return null
        return null;
    }

    public static StudentBegeleider CreateCoachFromDataString(string data)
    {
        // create string array with split items from string
        string[] dataStrings = data.Split(",");
        // check whether all nessecary info is filled in
        // when yes, create coach
        if (dataStrings.Length > 1)
        {
            // create coach object
            StudentBegeleider coach = new();
            // fill in info
            coach.Naam = dataStrings[0].Trim();
            coach.Docentcode = dataStrings[1].Trim();
            // return coach
            return coach;
        }
        // when no, return null
        return null;
    }
}