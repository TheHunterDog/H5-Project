using Microsoft.EntityFrameworkCore;

namespace Database.Model;

public class StudentenLijst : StudentBeleidContext
{
     public DbSet<Student> Students { get; set; }

     public void Loadlist()
     {
          
     
     }
}