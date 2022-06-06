﻿// <auto-generated />
using System;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(StudentBeleidContext))]
    [Migration("20220530084744_MissingSubjectFields")]
    partial class MissingSubjectFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Klasscode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentbegeleiderId")
                        .HasColumnType("int");

                    b.Property<string>("Studentnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tussenvoegsel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentbegeleiderId");

                    b.HasIndex("Studentnummer")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Database.Model.StudentBegeleider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Docentcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Docentcode")
                        .IsUnique();

                    b.ToTable("StudentBegeleiders");
                });

            modelBuilder.Entity("Database.Model.StudentBegeleiderGesprekken", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentBegeleiderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("GesprekDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Opmerkingen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Voltooid")
                        .HasColumnType("bit");

                    b.HasKey("StudentId", "StudentBegeleiderId", "GesprekDatum");

                    b.HasIndex("StudentBegeleiderId");

                    b.ToTable("StudentBegeleiderGesprekken");
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

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WPF.Model.StudentProblem", b =>
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

                    b.ToTable("StudentProblems");
                });

            modelBuilder.Entity("WPF.Model.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.HasOne("Database.Model.StudentBegeleider", "Studentbegeleider")
                        .WithMany("Students")
                        .HasForeignKey("StudentbegeleiderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Studentbegeleider");
                });

            modelBuilder.Entity("Database.Model.StudentBegeleiderGesprekken", b =>
                {
                    b.HasOne("Database.Model.StudentBegeleider", "StudentBegeleider")
                        .WithMany("StudentBegeleiderGesprekken")
                        .HasForeignKey("StudentBegeleiderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Model.Student", "Student")
                        .WithMany("StudentBegeleiderGesprekken")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudentBegeleider");
                });

            modelBuilder.Entity("WPF.Model.StudentProblem", b =>
                {
                    b.HasOne("Database.Model.Student", "Student")
                        .WithMany("StudentProblems")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPF.Model.Teacher", "Teacher")
                        .WithMany("StudentProblems")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Database.Model.Student", b =>
                {
                    b.Navigation("StudentBegeleiderGesprekken");

                    b.Navigation("StudentProblems");
                });

            modelBuilder.Entity("Database.Model.StudentBegeleider", b =>
                {
                    b.Navigation("StudentBegeleiderGesprekken");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("WPF.Model.Teacher", b =>
                {
                    b.Navigation("StudentProblems");
                });
#pragma warning restore 612, 618
        }
    }
}
