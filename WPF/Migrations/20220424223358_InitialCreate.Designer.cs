﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPF.Model;

#nullable disable

namespace WPF.Migrations
{
    [DbContext(typeof(StudentBeleidContext))]
    [Migration("20220424223358_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WPF.Model.Student", b =>
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

            modelBuilder.Entity("WPF.Model.StudentBegeleider", b =>
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

            modelBuilder.Entity("WPF.Model.StudentBegeleiderGesprekken", b =>
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

                    b.HasKey("StudentId", "StudentBegeleiderId");

                    b.HasIndex("StudentBegeleiderId");

                    b.ToTable("StudentBegeleiderGesprekkens");
                });

            modelBuilder.Entity("WPF.Model.Student", b =>
                {
                    b.HasOne("WPF.Model.StudentBegeleider", "Studentbegeleider")
                        .WithMany("Students")
                        .HasForeignKey("StudentbegeleiderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Studentbegeleider");
                });

            modelBuilder.Entity("WPF.Model.StudentBegeleiderGesprekken", b =>
                {
                    b.HasOne("WPF.Model.StudentBegeleider", "StudentBegeleider")
                        .WithMany("StudentBegeleiderGesprekken")
                        .HasForeignKey("StudentBegeleiderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPF.Model.Student", "Student")
                        .WithMany("StudentBegeleiderGesprekkens")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("StudentBegeleider");
                });

            modelBuilder.Entity("WPF.Model.Student", b =>
                {
                    b.Navigation("StudentBegeleiderGesprekkens");
                });

            modelBuilder.Entity("WPF.Model.StudentBegeleider", b =>
                {
                    b.Navigation("StudentBegeleiderGesprekken");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}