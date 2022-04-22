using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WPF.Model;

public class StudentBeleidContext: DbContext
{
   public DbSet<Student> Students { get; set; }
   public DbSet<StudentBegeleider> StudentBegeleiders { get; set; }
   public DbSet<StudentBegeleiderGesprekken> StudentBegeleiderGesprekkens { get; set; }

   protected override void OnModelCreating(DbModelBuilder modelBuilder)
   {
      #region Student
      modelBuilder.Entity<Student>()
         .HasKey(s => new {s.Id});
      modelBuilder.Entity<Student>()
         .Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      modelBuilder.Entity<Student>()
         .HasIndex(s => s.Studentnummer).IsUnique();
      modelBuilder.Entity<Student>()
         .Property(s => s.Voornaam).IsRequired();
      modelBuilder.Entity<Student>()
         .Property(s => s.Tussenvoegsel).IsOptional();
      modelBuilder.Entity<Student>()
         .Property(s => s.Achternaam).IsRequired();
      modelBuilder.Entity<Student>()
         .Property(s => s.Klasscode).IsRequired();





      #endregion

      #region StudentBegeleider

      modelBuilder.Entity<StudentBegeleider>()
         .HasKey(s => new {s.Id});
      modelBuilder.Entity<StudentBegeleider>()
         .HasIndex(s => s.Docentcode).IsUnique();
      modelBuilder.Entity<StudentBegeleider>()
         .Property(s => s.Docentcode).IsRequired();

      #endregion

      #region studentBegeleiderGesprekken



      #endregion
   }
}