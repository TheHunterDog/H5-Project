using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPF.Model;

public class StudentenLijst : StudentBeleidContext
{
     public DbSet<Student> Students { get; set; }

     public void Loadlist()
     {
          
     
     }
}