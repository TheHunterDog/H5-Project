using Microsoft.EntityFrameworkCore;

namespace Database.Model;

public class StudentBeleidContext: DbContext
{
  /// <summary>
  /// Dbsets required for OnModelCreating
  /// </summary>
  public DbSet<Student> Students { get; set; }
   public DbSet<StudentBegeleider> StudentBegeleiders { get; set; }
   public DbSet<StudentBegeleiderGesprekken> StudentBegeleiderGesprekken { get; set; }

    #region Constructors
    /// <summary>
/// conext needs empty constructor
/// @see https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#using-a-constructor-with-no-parameters
/// </summary>
    public StudentBeleidContext()
    {

    }
#endregion

    /// <summary>
    /// Connect to the mssql database
    /// </summary>
    /// <param name="options"><see cref="DbContextOptionsBuilder"/></param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      // String will be moved to app.config
       // options.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["StudentBeleidDatabase"].ConnectionString);
       options.UseSqlServer("Server=ftp.huttennl.nl,1433;Database=StudentBegeleid;User Id=sa;Password=9CknApvBHa2aNuovTirqhmEd");
    }

    /// <summary>
    /// Set all fields to required constraints
    /// </summary>
    /// <param name="modelBuilder"></param>
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
       modelBuilder.Entity<Student>()
          .Property(s => s.Tussenvoegsel).IsRequired(false);
       modelBuilder.Entity<Student>()
          .Property(s => s.Achternaam).IsRequired();
       modelBuilder.Entity<Student>()
          .Property(s => s.Klasscode).IsRequired();


       modelBuilder.Entity<Student>()
          .HasOne(s => s.Studentbegeleider)
          .WithMany(sb => sb.Students)
          .HasForeignKey(s => s.StudentbegeleiderId)
          .OnDelete(DeleteBehavior.NoAction)
          .IsRequired(false);

       modelBuilder.Entity<Student>().HasData(
          new Student
          {
             Klasscode = "OOSDDH2022", Voornaam = "Mark", Achternaam = "Heijnekamp", Tussenvoegsel = "",
             Studentnummer = "s1156618",Id = 1156618
          },
          new Student
          {
             Klasscode = "OOSDDH2022", Voornaam = "Rob", Achternaam = "Hutten", Tussenvoegsel = "",
             Studentnummer = "s1152882",Id = 1152882
          }, new Student
          {
             Klasscode = "OOSDDH2022", Voornaam = "Antoine", Achternaam = "Pijlgroms", Tussenvoegsel = "",
             Studentnummer = "s1159362",Id = 1159362
          }, new Student
          {
             Klasscode = "OOSDDH2022", Voornaam = "Evert-Jan", Achternaam = "Nijsink", Tussenvoegsel = "",
             Studentnummer = "s1160918",Id = 1160918
          }, new Student
          {
             Klasscode = "OOSDDH2022", Voornaam = "Tristan", Achternaam = "Jongedijk", Tussenvoegsel = "",
             Studentnummer = "s1147577",Id = 1147577
          },
          new Student
          {
             Klasscode = "OOSDDH2023", Voornaam = "Jan", Achternaam = "Jaap", Tussenvoegsel = "Jappie",
             Studentnummer = "s1147576",Id = 1147576
          }
       );

       #endregion

       #region StudentBegeleider

       modelBuilder.Entity<StudentBegeleider>()
          .HasKey(s => new {s.Id});
       modelBuilder.Entity<StudentBegeleider>()
          .HasIndex(s => s.Docentcode).IsUnique();
       modelBuilder.Entity<StudentBegeleider>()
          .Property(s => s.Docentcode).IsRequired();

       modelBuilder.Entity<StudentBegeleider>().HasData(

          new StudentBegeleider
          {
             Docentcode = "SA1234",Naam = "Karen brakband",Id = 964
          }
       );
       
       #endregion

       #region studentBegeleiderGesprekken

       modelBuilder.Entity<StudentBegeleiderGesprekken>()
          .HasKey(sbg => new {sbg.StudentId, sbg.StudentBegeleiderId, sbg.GesprekDatum});

       modelBuilder.Entity<StudentBegeleiderGesprekken>()
          .HasOne(sbg => sbg.Student)
          .WithMany(s => s.StudentBegeleiderGesprekken)
          .HasForeignKey(sbg => sbg.StudentId)
          .OnDelete(DeleteBehavior.Cascade);

       modelBuilder.Entity<StudentBegeleiderGesprekken>()
          .HasOne(sbg => sbg.StudentBegeleider)
          .WithMany(sb => sb.StudentBegeleiderGesprekken)
          .HasForeignKey(sbg => sbg.StudentBegeleiderId)
          .OnDelete(DeleteBehavior.Cascade);

       #endregion
       
    }
}