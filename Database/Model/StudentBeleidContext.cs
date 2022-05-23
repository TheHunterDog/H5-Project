using Microsoft.EntityFrameworkCore;

namespace Database.Model;

public class StudentBeleidContext : DbContext
{

  /// <summary>
  /// Dbsets required for OnModelCreating
  /// </summary>
  public DbSet<Student> Students { get; set; }
  public DbSet<Leerdoel> Leerdoelen { get; set; }
  public DbSet<StudentBegeleider> StudentBegeleiders { get; set; }
  public DbSet<StudentBegeleiderGesprekken> StudentBegeleiderGesprekken { get; set; }
  public DbSet<StudentProblem> StudentProblems { get; set; }
  public DbSet<Teacher> Teachers { get; set; }


    #region Constructors
    
    public StudentBeleidContext()
    {
    }

    #endregion

    /// <summary>
    /// Tell EntityFramework which database to use
    /// </summary>
    /// <param name="options"><see cref="DbContextOptionsBuilder"/></param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // String will be moved to app.config
        // options.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["StudentBeleidDatabase"].ConnectionString);
        options.UseSqlServer(
            "Server=ftp.huttennl.nl,1433;Database=StudentBegeleid;User Id=sa;Password=9CknApvBHa2aNuovTirqhmEd");
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

        modelBuilder.Entity<Student>()
            .HasMany(s => s.StudentProblems)
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

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

        #region Leerdoel

        modelBuilder.Entity<Leerdoel>()
           .HasKey(s => new {s.Id});
        modelBuilder.Entity<Leerdoel>()
           .Property(s => s.Id)
           .ValueGeneratedOnAdd();
        modelBuilder.Entity<Leerdoel>()
           .HasIndex(s => s.Beschrijving).IsUnique();

        modelBuilder.Entity<Student>()
           .HasOne(s => s.Studentbegeleider)
           .WithMany(sb => sb.Students)
           .HasForeignKey(s => s.StudentbegeleiderId)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(false);

        modelBuilder.Entity<Leerdoel>()
           .HasOne(s => s.Student)
           .WithMany(sb => sb.Leerdoelen)
           .HasForeignKey(s => s.StudentId)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(false);

        #endregion

        #region studentProblems

        modelBuilder.Entity<StudentProblem>()
            .HasKey(sp => new { sp.Id });

        modelBuilder.Entity<StudentProblem>()
            .HasOne(sp => sp.Student)
            .WithMany(s => s.StudentProblems)
            .HasForeignKey(sp => sp.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentProblem>()
            .HasOne(sp => sp.Teacher)
            .WithMany(s => s.StudentProblems)
            .HasForeignKey(sp => sp.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Teacher

        modelBuilder.Entity<Teacher>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

        #endregion
    }

}