using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Database.Model;

namespace WPF.Util;

public static class SmartSearch
{
    public static List<int> SmartSearchStudent(string query)
    {
        PropertyInfo[] propertyInfo = typeof(Student).GetProperties();
        query = query.ToLower();
        List<int> id = new List<int>();
        
        using (StudentBeleidContext context = new StudentBeleidContext())
        {
            //todo: Add regex partial matching
            //TODO: Find better way to loop though every property
            id = context.Students.Where(s => s.Voornaam == query).Select(student => student.Id).ToList().Concat(id).ToList();
            id = context.Students.Where(s => s.Achternaam == query).Select(student => student.Id).ToList().Concat(id).ToList();
            id = context.Students.Where(s => s.Klasscode == query).Select(student => student.Id).ToList().Concat(id).ToList();
            id = context.Students.Where(s => s.Studentnummer == query).Select(student => student.Id).ToList().Concat(id).ToList();
            id = context.Students.Where(s => s.Tussenvoegsel == query).Select(student => student.Id).ToList().Concat(id).ToList();
        }
        return id;
    }
}