﻿// <auto-generated />
using System;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Model.LearningGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.HasIndex("StudentId");

                    b.ToTable("LearningGoals");
                });

            modelBuilder.Entity("Database.Model.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentSupervisorId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("StudentSupervisorId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClassCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StudentSupervisor")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.HasIndex("StudentSupervisor");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Database.Model.StudentProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("StudentProblem");
                });

            modelBuilder.Entity("Database.Model.StudentSupervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherCode")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("StudentSupervisor");
                });

            modelBuilder.Entity("Database.Model.StudentSupervisorMeeting", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentSupervisorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MeetingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.HasKey("StudentId", "StudentSupervisorId", "MeetingDate");

                    b.HasIndex("StudentSupervisorId");

                    b.ToTable("StudentSupervisorMeeting");
                });

            modelBuilder.Entity("Database.Model.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lessons")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Database.Model.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("Database.Model.LearningGoal", b =>
                {
                    b.HasOne("Database.Model.Student", "Student")
                        .WithMany("LearningGoals")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Database.Model.Notification", b =>
                {
                    b.HasOne("Database.Model.StudentSupervisor", "Receiver")
                        .WithMany("NotificationsRecived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Database.Model.StudentSupervisor", null)
                        .WithMany("Notifications")
                        .HasForeignKey("StudentSupervisorId");

                    b.HasOne("Database.Model.Teacher", null)
                        .WithMany("Notifications")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.HasOne("Database.Model.StudentSupervisor", "Supervisor")
                        .WithMany("Students")
                        .HasForeignKey("StudentSupervisor")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("Database.Model.StudentProblem", b =>
                {
                    b.HasOne("Database.Model.Student", "Student")
                        .WithMany("StudentProblems")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Model.Teacher", "Teacher")
                        .WithMany("StudentProblems")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Database.Model.StudentSupervisorMeeting", b =>
                {
                    b.HasOne("Database.Model.Student", "Student")
                        .WithMany("SupervisorMeetings")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Model.StudentSupervisor", "StudentSupervisor")
                        .WithMany("StudentSupervisorMeetings")
                        .HasForeignKey("StudentSupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudentSupervisor");
                });

            modelBuilder.Entity("StudentSubject", b =>
                {
                    b.HasOne("Database.Model.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Model.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.Navigation("LearningGoals");

                    b.Navigation("StudentProblems");

                    b.Navigation("SupervisorMeetings");
                });

            modelBuilder.Entity("Database.Model.StudentSupervisor", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("NotificationsRecived");

                    b.Navigation("StudentSupervisorMeetings");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Database.Model.Teacher", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("StudentProblems");
                });
#pragma warning restore 612, 618
        }
    }
}
