using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Database.Model;

namespace WPF.Util;

public static class SmartSearch
{
    /// <summary>
    /// Search student based on any field in the Student <see cref="Student"/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="context"></param>
    /// <returns>int</returns>
    public static List<int> SmartSearchStudentID(string query, StudentBeleidContext context)
    {
        return context.Students.Where(s =>
            s.Voornaam.Contains(query) ||
            s.Achternaam.Contains(query) ||
            s.Klasscode.Contains(query) ||
            s.Studentnummer.Contains(query) ||
            s.Tussenvoegsel.Contains(query)).Select(student => student.Id).ToList();
    }
    
    /// <summary>
    /// Search student based on any field in the Student <see cref="Student"/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="context"></param>
    /// <returns><see cref="Student"/></returns>
    public static List<Student> SmartSearchStudent(string query, StudentBeleidContext context)
    {
        return context.Students.Where(s =>
            s.Voornaam.Contains(query) ||
            s.Achternaam.Contains(query) ||
            s.Klasscode.Contains(query) ||
            s.Studentnummer.Contains(query) ||
            s.Tussenvoegsel.Contains(query)).ToList();
    }
}