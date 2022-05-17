using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Database.Model;

namespace WPF.Util;

public static class SmartSearch
{
    public static List<int> SmartSearchStudentID(string query, StudentBeleidContext context)
    {
        
        //todo: Add regex partial matching
        //TODO: Find better way to loop though every property
        return context.Students.Where(s =>
            s.Voornaam.Contains(query) ||
            s.Achternaam.Contains(query) ||
            s.Klasscode.Contains(query) ||
            s.Studentnummer.Contains(query) ||
            s.Tussenvoegsel.Contains(query)).Select(student => student.Id).ToList();
    }
    public static List<Student> SmartSearchStudent(string query, StudentBeleidContext context)
    {
        
        //todo: Add regex partial matching
        //TODO: Find better way to loop though every property
        return context.Students.Where(s =>
            s.Voornaam.Contains(query) ||
            s.Achternaam.Contains(query) ||
            s.Klasscode.Contains(query) ||
            s.Studentnummer.Contains(query) ||
            s.Tussenvoegsel.Contains(query)).ToList();
    }
}