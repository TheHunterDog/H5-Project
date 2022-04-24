using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WPF.Model;

public class StudentBeleidContext: DbContext
{
   public DbSet<Student> Students { get; set; }
   public DbSet<StudentBegeleider> StudentBegeleiders { get; set; }
   public DbSet<StudentBegeleiderGesprekken> StudentBegeleiderGesprekkens { get; set; }

   public IConfiguration Configuration;

    #region Constructors

    protected StudentBeleidContext()
    {
       
    }

    #endregion

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
       options.UseSqlServer(ConfigurationManager.ConnectionStrings["StudentBeleidDatabase"].ConnectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      #region Student
      modelBuilder.Entity<Student>()
         .HasKey(s => new {s.Id});
      modelBuilder.Entity<Student>()
         .Property(s => s.Id)
         .ValueGeneratedOnAdd();
      modelBuilder.Entity<Student>()
         .HasIndex(s => s.Studentnummer).IsUnique();
      modelBuilder.Entity<Student>()
         .Property(s => s.Voornaam).IsRequired();
      // modelBuilder.Entity<Student>()
      //    .Property(s => s.Tussenvoegsel).IsOptional();
      modelBuilder.Entity<Student>()
         .Property(s => s.Achternaam).IsRequired();
      modelBuilder.Entity<Student>()
         .Property(s => s.Klasscode).IsRequired();
      modelBuilder.Entity<Student>()
         .HasOne<StudentBegeleider>(s => s.Studentbegeleider)
         .WithMany(sb => sb.Students)
         .HasForeignKey(s => s.StudentbegeleiderId);

      #region Seeder

      // modelBuilder.Entity<Student>().HasData(
      //
      //    new Student
      //    {
      //      Id = -1, Klasscode = "OOSDDH2022", Voornaam = "Mark", Achternaam = "Heijnekamp", Tussenvoegsel = "",
      //       Studentnummer = "s1156618", 
      //    },
      //    new Student
      //    {
      //       Id = -2,Klasscode = "OOSDDH2022", Voornaam = "Rob", Achternaam = "Hutten", Tussenvoegsel = "",
      //       Studentnummer = "s1152882", 
      //    },
      //    new Student
      //    {
      //       Id = -3,Klasscode = "OOSDDH2022", Voornaam = "Antoine", Achternaam = "Pijlgroms", Tussenvoegsel = "",
      //       Studentnummer = "s1159362", 
      //    },            new Student
      //    {
      //       Id = -4,Klasscode = "OOSDDH2022", Voornaam = "Evert-Jan", Achternaam = "Nijsink", Tussenvoegsel = "",
      //       Studentnummer = "s1160918",
      //    },            new Student
      //    {
      //       Id = -5,Klasscode = "OOSDDH2022", Voornaam = "Tristan", Achternaam = "Jongedijk", Tussenvoegsel = "",
      //       Studentnummer = "s1147577",
      //       
      //    }
      // );
      

      #endregion
      #endregion
   
      #region StudentBegeleider
   
      modelBuilder.Entity<StudentBegeleider>()
         .HasKey(s => new {s.Id});
      modelBuilder.Entity<StudentBegeleider>()
         .HasIndex(s => s.Docentcode).IsUnique();
      modelBuilder.Entity<StudentBegeleider>()
         .Property(s => s.Docentcode).IsRequired();

      #region Seeder

      // modelBuilder.Entity<StudentBegeleider>().HasData(
      //    new StudentBegeleider
      //    {Id = -1,
      //       Naam = "Karen Brakband",
      //       Docentcode = "ABCD-KB-2022"
      //    },
      // new StudentBegeleider
      // {Id = -2,
      //    Naam = "Willie conen",
      //    Docentcode = "ABCD-WC-2022"
      // }
      //    );

      #endregion
      #endregion
   
      #region studentBegeleiderGesprekken

      modelBuilder.Entity<StudentBegeleiderGesprekken>()
         .HasKey(sbg => new {sbg.StudentId, sbg.StudentBegeleiderId});

      #endregion
   }
}