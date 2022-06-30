#region

using System.Net.NetworkInformation;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;

#endregion

namespace Database.Model;

public class DatabaseContext : DbContext
{

  /// <summary>
  /// Dbsets required for OnModelCreating
  /// </summary>

  public DbSet<Student> Student { get; set; }
  public DbSet<LearningGoal> LearningGoals { get; set; }
  public DbSet<StudentSupervisor> StudentSupervisor { get; set; }
  public DbSet<StudentSupervisorMeeting> StudentSupervisorMeeting { get; set; }
  public DbSet<StudentProblem> StudentProblem { get; set; }
  public DbSet<Teacher> Teacher { get; set; }
  public DbSet<Subject> Subject { get; set; }
  public DbSet<Notification> Notifications { get; set; }
  
  public DbSet<MapNode> MapNodes { get; set; }

  #region Constructors

    public DatabaseContext()
    {
    }

    #endregion

    public static bool PingServer()
    {
        var ping = new Ping();
        return ping.Send("ftp.huttennl.nl").Status.ToString().Equals("Success");
    }
    
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
            .HasIndex(s => s.StudentNumber).IsUnique();
        modelBuilder.Entity<Student>()
            .Property(s => s.FirstName).IsRequired();
        modelBuilder.Entity<Student>()
            .Property(s => s.MiddleName).IsRequired(false);
        modelBuilder.Entity<Student>()
            .Property(s => s.LastName).IsRequired();
        modelBuilder.Entity<Student>()
            .Property(s => s.ClassCode).IsRequired();
        
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Supervisor)
            .WithMany(sb => sb.Students)
            .HasForeignKey(s => s.StudentSupervisor)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.StudentProblems)
            .WithOne(s => s.Student)
            .HasForeignKey(s => s.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Subjects)
            .WithMany(s => s.Students);
        #endregion

        #region StudentSupervisor

        modelBuilder.Entity<StudentSupervisor>()
            .HasKey(s => new {s.Id});
        modelBuilder.Entity<StudentSupervisor>()
            .HasIndex(s => s.TeacherCode).IsUnique();
        modelBuilder.Entity<StudentSupervisor>()
            .Property(s => s.TeacherCode).IsRequired();

        #endregion

        #region studentSupervisorMeeting

        modelBuilder.Entity<StudentSupervisorMeeting>()
            .HasKey(sbg => new {sbg.StudentId, sbg.StudentSupervisorId, GesprekDatum = sbg.MeetingDate});

        modelBuilder.Entity<StudentSupervisorMeeting>()
            .HasOne(sbg => sbg.Student)
            .WithMany(s => s.SupervisorMeetings)
            .HasForeignKey(sbg => sbg.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentSupervisorMeeting>()
            .HasOne(sbg => sbg.StudentSupervisor)
            .WithMany(sb => sb.StudentSupervisorMeetings)
            .HasForeignKey(sbg => sbg.StudentSupervisorId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region LearningGoal

        modelBuilder.Entity<LearningGoal>()
           .HasKey(s => new {s.Id});
        modelBuilder.Entity<LearningGoal>()
           .Property(s => s.Id)
           .ValueGeneratedOnAdd();
        modelBuilder.Entity<LearningGoal>()
           .HasIndex(s => s.Description).IsUnique();

        modelBuilder.Entity<Student>()
           .HasOne(s => s.Supervisor)
           .WithMany(sb => sb.Students)
           .HasForeignKey(s => s.StudentSupervisor)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(false);

        modelBuilder.Entity<LearningGoal>()
           .HasOne(s => s.Student)
           .WithMany(sb => sb.LearningGoals)
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

        #region IAuthenticatable

        modelBuilder.Entity<StudentSupervisor>()
            .Property(s => s.Username).IsRequired();
        modelBuilder.Entity<StudentSupervisor>()
            .Property(s => s.Password).IsRequired();
        modelBuilder.Entity<StudentSupervisor>()
            .HasIndex(s => s.Username).IsUnique();
        
        modelBuilder.Entity<Teacher>()
            .Property(s => s.Username).IsRequired();
        modelBuilder.Entity<Teacher>()
            .HasIndex(s => s.Username).IsUnique();
        modelBuilder.Entity<Teacher>()
            .Property(s => s.Password).IsRequired();
        #endregion
        
        #region Subject

        modelBuilder.Entity<Subject>()
            .HasKey(s => new {s.Id});
        modelBuilder.Entity<Subject>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Subject>()
            .Property(s => s.Name).IsRequired();
        modelBuilder.Entity<Subject>()
            .Property(s => s.Description).IsRequired(false);
        #endregion

        #region Notification

        modelBuilder.Entity<Notification>()
            .HasKey(s => new {s.Id});
        modelBuilder.Entity<Notification>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Notification>()
            .Property(s => s.Description).IsRequired();

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Receiver)
            .WithMany(i => i.NotificationsRecived)
            .HasForeignKey(s => s.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<StudentSupervisor>()
            .HasMany(s => s.NotificationsRecived)
            .WithOne(s => s.Receiver)
            .HasForeignKey(s => s.ReceiverId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region MapNodes

        modelBuilder.Entity<MapNode>()
            .HasKey(s => new {s.Id});
        modelBuilder.Entity<MapNode>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<MapNode>()
            .HasIndex(s => s.Letter).IsUnique();
        modelBuilder.Entity<MapNode>()
            .Property(s => s.Letter).IsRequired();
        
        modelBuilder.Entity<MapNode>().Property(node => node.LocationCollection)
            .HasConversion(
                a => (string)JsonConvert.SerializeObject(a),
                a => JsonConvert.DeserializeObject<LocationCollection>(a));

        #endregion
    }

}