#region

using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using Database.Model;

#endregion

namespace DataImporting;

public class ExcelImporter
{
    public static void Main(string[] args)
    {

    }

    public static void RemoveStudentsAndCoaches()
    {
        using (var context = new DatabaseContext())
        {
            // remove studentbegeleiders first to avoid relation conflicts
            context.StudentSupervisor.RemoveRange(context.StudentSupervisor); 
            // remove students
            context.Student.RemoveRange(context.Student); 
            // save changes to database
            context.SaveChanges(); 
        }
    }
    public static void PrintStudents()
    {
        Trace.WriteLine("\n All Student in database:");
        using (var context = new DatabaseContext())
        {
            // obtain students from database
            List<Student> students = context.Student.ToList(); 
            for (int i = 0; i < students.Count; i++)
            {
                // get student from list
                Student student = students.ElementAt(i);
                // print student data
                Trace.WriteLine(student); 
            }
        }
    }

    public static void PrintCoaches()
    {
        Console.WriteLine("\n All Coaches in database:");
        using (var context = new DatabaseContext())
        {
            // obtain coaches from database
            List<StudentSupervisor> coaches = context.StudentSupervisor.ToList(); 
            for (int i = 0; i < coaches.Count; i++)
            {
                // get coach from list
                StudentSupervisor coach = coaches.ElementAt(i); 
                // print coach data
                Console.WriteLine(coach); 
            }
        }
    }

    /**
     * <summary>Imports students from excel file</summary>
     */
    public static void ImportStudentsFromFile(string fileLocation = "")
    {
        // get data from file
        DataTable? data = GetDataTableFromFile(fileLocation);//, "Student");
        // if data is null, file could not be found
        if(data == null) return;
        // get strings with student data
        string[] studentStrings = ReadDataFromDataTable(data);
        // save data to database
        using (var context = new DatabaseContext())
        {
            for (int i = 0; i < studentStrings.Length; i++)
            {
                // convert string to object of type Student
                Student? student = CreateStudentFromDataString(studentStrings[i]);
                // add student to database
                if (student != null)
                {
                    // obtain coach(docentcode) from datastring
                    string docentCode = (studentStrings.Length == 5) ? studentStrings[i].Split(",")[3].Trim() : studentStrings[i].Split(",")[4].Trim();
                    // get count of coaches which have a matching docentcode(max 1);
                    int countBeg = context.StudentSupervisor.Where(x => x.TeacherCode.Equals(docentCode)).Count();
                    // if no matching coach is found, create a new one with an undefined name(can be overwritten when added to excel file with the same docentcode)
                    if (countBeg < 1) 
                    {
                        // create name for new coach
                        string defaultStudentCoachName = "Undefined name";
                        // create datastring
                        string newStudentBegeleiderDataString = $"{defaultStudentCoachName}, {docentCode}";
                        // create new coach from datastring
                        StudentSupervisor? newStudentBegeleider = CreateCoachFromDataString(newStudentBegeleiderDataString);

                        if (newStudentBegeleider != null)
                        {
                            // add coach to database
                            context.StudentSupervisor.Add(newStudentBegeleider);
                            // savve changes to database
                            context.SaveChanges();
                        }
                        else return;
                    }
                    StudentSupervisor supervisor = context.StudentSupervisor.Where(x => x.TeacherCode.Equals(docentCode)).First();
                    student.StudentbegeleiderId = supervisor.Id;
                    // get whether there is already a student with the same code in the database
                    int count = context.Student.Where(x => x.Studentnummer.Equals(student.Studentnummer)).Count();
                    // when yes, update student with new name etc.
                    if (count > 0)
                    {
                        // get student from database
                        var result = context.Student.Where(x => x.Studentnummer.Equals(student.Studentnummer)).First();
                        // edit student parameters
                        if (student.Voornaam != null) result.Voornaam = student.Voornaam;
                        if (student.Achternaam != null) result.Achternaam = student.Achternaam;
                        if (student.Tussenvoegsel != null) result.Tussenvoegsel = student.Tussenvoegsel;
                        if (student.Klasscode != null) result.Klasscode = student.Klasscode;
                    }
                    // when no, add the student to the database
                    else
                    {
                        // add student to database
                        context.Student.Add(student);
                    }
                }
            }
            // save changes to database
            context.SaveChanges();
        }
    }

    /**
     * <summary>Import coaches from excel file</summary>
     */
    public static void ImportCoachesFromFile(string fileLocation = "")
    {
        // get data from file
        DataTable? data = GetDataTableFromFile(fileLocation);//, "Coaches");
        // if data is null, file could not be found
        if (data == null) return;
        // get strings with coach data
        string[] coachStrings = ReadDataFromDataTable(data);
        // save data to database
        using (var context = new DatabaseContext())
        {
            for (int i = 0; i < coachStrings.Length; i++)
            {
                // convert string to object of type Coach
                StudentSupervisor? coach = CreateCoachFromDataString(coachStrings[i]);
                // add coach to database
                if (coach != null)
                {
                    // get whether there is already a coach with the same code in the database
                    int count = context.StudentSupervisor.Where(x => x.TeacherCode.Equals(coach.TeacherCode)).Count();
                    // when yes, update coach with new name etc.
                    if (count > 0)
                    {
                        // get coach from database
                        var result = context.StudentSupervisor.Where(x => x.TeacherCode.Equals(coach.TeacherCode)).First();
                        // update coach parameters
                        result.Name = coach.Name;
                        result.TeacherCode = coach.TeacherCode; // redundant
                    }
                    // when no, add the coach to the database
                    else
                    {
                        //Console.WriteLine("Add");
                        context.StudentSupervisor.Add(coach);
                    }
                }
            }
            // save changes to database
            context.SaveChanges();
        }
    }

    /**
     * <summary>Get datatable from the excel file</summary>
     */
    public static DataTable? GetDataTableFromFile(string fileLocation)
    {
        if (!fileLocation.Contains(".xlsx")) return null;

        // create connection string
        var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=Excel 12.0;";

        OleDbConnection objConn;
        DataTable? dt;
        try
        {
            // create connection
            objConn = new OleDbConnection(connectionString);
            objConn.Open();
            // get first sheet name
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count == 0) return null;
            if (dt.Rows[0].Table == null) return null;
        }
        catch (Exception e)
        {
            return null;
        }

        string? sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
        if (sheetName == null) return null;
        sheetName = sheetName.Replace("$", "");

        // dispose and close connection
        if (objConn != null)
        {
            objConn.Close();
            objConn.Dispose();
        }
        if (dt != null)
        {
            dt.Dispose();
        }
        Trace.WriteLine(sheetName);
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
        // dispose adapter
        adapter.Dispose();
        // return dataset
        return ds.Tables[0];

    }

    /**
     * <summary>read the data from the datatable</summary>
     */
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

    /**
     * <summary>Create a student from the data in the excel file</summary>
     */
    public static Student? CreateStudentFromDataString(string data)
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
            if (dataStrings.Length == 5)
            {
                student.Klasscode = dataStrings[4].Trim();
            }
            else if (dataStrings.Length == 6)
            {
                student.Tussenvoegsel = dataStrings[3].Trim();
                student.Klasscode = dataStrings[5].Trim();

            }
            // return student
            return student;
        }
        // when no, return null
        return null;
    }

    /**
     * <summary>Create coach from the data in the excel file</summary>
     */
    public static StudentSupervisor? CreateCoachFromDataString(string data)
    {
        // create string array with split items from string
        string[] dataStrings = data.Split(",");
        // check whether all nessecary info is filled in
        // when yes, create coach
        if (dataStrings.Length > 1)
        {
            // create coach object
            StudentSupervisor coach = new();
            // fill in info
            coach.Name = dataStrings[0].Trim();
            coach.TeacherCode = dataStrings[1].Trim();
            // return coach
            return coach;
        }
        // when no, return null
        return null;
    }
}