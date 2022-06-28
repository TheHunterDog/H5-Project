#region

using System.Collections.Generic;
using System.Linq;
using Database.Model;

#endregion

namespace WPF.Util;

public static class SmartSearch
{
    /// <summary>
    /// Search student based on any field in the Student <see cref="Student"/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="context"></param>
    /// <returns>int</returns>
    public static List<int> SmartSearchStudentID(string query, DatabaseContext context)
    {
        return context.Student.Where(s =>
            s.FirstName.Contains(query) ||
            s.LastName.Contains(query) ||
            s.ClassCode.Contains(query) ||
            s.StudentNumber.Contains(query) ||
            s.MiddleName.Contains(query)).Select(student => student.Id).ToList();
    }
    
    /// <summary>
    /// Search student based on any field in the Student <see cref="Student"/>
    /// </summary>
    /// <param name="query"></param>
    /// <param name="context"></param>
    /// <returns><see cref="Student"/></returns>
    public static List<Student> SmartSearchStudent(string query, DatabaseContext context)
    {
        return context.Student.Where(s =>
            s.FirstName.Contains(query) ||
            s.LastName.Contains(query) ||
            s.ClassCode.Contains(query) ||
            s.StudentNumber.Contains(query) ||
            s.MiddleName.Contains(query)).ToList();
    }
}