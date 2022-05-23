using System;
using System.Collections.Generic;
using System.Linq;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WPF.Model;

namespace UnitTest_WPF_Inplannen;

[TestFixture]
public class Model_Should_Property_Changed
{
    #region student

    private Student _student;
    private string _firstname = "Jan";
    private string _lastname = "Willem";
    private string _lastnameprefix = "jaap";
    private string _klassencode = "OOSDDH";
    private string _studentnummer = "S123455";
    private int _id = 1234567890;

    #endregion

    #region studentbegeleider

    private StudentBegeleider _studentBegeleider = new StudentBegeleider();
    private string _name = "randy bonenwaag";
    private string _docentcode = "DOC345563";

    #endregion

    #region studentProblem

    private StudentProblem _studentProblem = new StudentProblem();

    private string _description =
        "Windesheim is een goeie school met veel gemotiveerde studenten. Hier kan je veel leren over diverse vakken.";

    private int _priority = 15;
    #endregion

    #region Completefilled models

    public StudentBegeleiderGesprekken StudentBegeleiderGesprekken = new StudentBegeleiderGesprekken()
    {
        GesprekDatum = DateTime.Now, Opmerkingen = "Voortgang bespreken", Student = null, StudentId = 0,
        StudentBegeleider = null, StudentBegeleiderId = 0, Voltooid = false
    };

    public StudentBegeleider studentBegeleider = new StudentBegeleider()
    {
        Docentcode = "DOC231253", Id = 1234, Naam = "karenBrakband", StudentBegeleiderGesprekken = null, Students = null
    };

    public StudentProblem StudentProblem = new StudentProblem()
    {
        Id = 12, Description = "Gepest", Priority = 10, Student = null, StudentId = 0, Teacher = null, TeacherId = 0
    };

    #endregion

    #region Teacher

    private Teacher _teacher = new Teacher();
    
    #endregion

    [SetUp]
    public void SetUp()
    {
        _student = new Student();
    }

    [Test]
    public void StudentBegeleider_Change_Property_Changed_Should_Throw_Exception_When_NULL()
    {
        Assert.Throws<ArgumentNullException>(() => _studentBegeleider.Docentcode = null);
        Assert.Throws<ArgumentNullException>(() => _studentBegeleider.Naam = null);
    }
    
    [Test]
    public void StudentBegeleider_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentBegeleider.Naam,null);
        Assert.AreEqual(_studentBegeleider.Docentcode,null);
        Assert.AreEqual(_studentBegeleider.Id,0);

        _studentBegeleider.Naam = _name;
        _studentBegeleider.Docentcode = _docentcode;
        _studentBegeleider.Id = _id;
        
        Assert.AreEqual(_studentBegeleider.Naam,_name);
        Assert.AreEqual(_studentBegeleider.Docentcode,_docentcode);
        Assert.AreEqual(_studentBegeleider.Id,_id);
    }

    
    [Test]
    public void Student_Change_Property_Changed_Should_Throw_Exception_When_NULL()
    {
        Assert.Throws<ArgumentNullException>(() => _student.Voornaam = null);
        Assert.Throws<ArgumentNullException>(() => _student.Achternaam = null);
        Assert.Throws<ArgumentNullException>(() => _student.Klasscode = null);
        Assert.Throws<ArgumentNullException>(() => _student.Tussenvoegsel = null);
        Assert.Throws<ArgumentNullException>(() => _student.Studentnummer = null); 
    }

    [Test]
    public void Student_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_student.Achternaam,null);
        Assert.AreEqual(_student.Voornaam,null);
        Assert.AreEqual(_student.Klasscode,null);
        Assert.AreEqual(_student.Tussenvoegsel,null);
        Assert.AreEqual(_student.Id,0);
        Assert.AreEqual(_student.Studentnummer,null);
        
        

        _student.Achternaam = _lastname;
        _student.Voornaam = _firstname;
        _student.Klasscode = _klassencode;
        _student.Tussenvoegsel = _lastnameprefix;
        _student.Id = _id;
        _student.Studentnummer = _studentnummer;
        
        Assert.AreEqual(_student.Achternaam,_lastname);
        Assert.AreEqual(_student.Voornaam,_firstname);
        Assert.AreEqual(_student.Klasscode,_klassencode);
        Assert.AreEqual(_student.Tussenvoegsel,_lastnameprefix);
        Assert.AreEqual(_student.Id,_id);
        Assert.AreEqual(_student.Studentnummer,_studentnummer);
    }

    [Test]
    public void StudentProblem_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentProblem.Description,null);
        Assert.AreEqual(_studentProblem.Id,0);
        Assert.AreEqual(_studentProblem.Priority,0);
        Assert.AreEqual(_studentProblem.Student,null);
        Assert.AreEqual(_studentProblem.Teacher,null);
        Assert.AreEqual(_studentProblem.StudentId,0);
        Assert.AreEqual(_studentProblem.TeacherId,0);

        _studentProblem.Description = _description;
        _studentProblem.Id = _id;
        _studentProblem.Priority = _priority;
        _studentProblem.Student = _student;
        _studentProblem.Teacher = _teacher;
        _studentProblem.StudentId = _student.Id;
        _studentProblem.TeacherId = _teacher.Id;

        Assert.AreEqual(_studentProblem.Description,_description);
        Assert.AreEqual(_studentProblem.Id,_id);
        Assert.AreEqual(_studentProblem.Priority,_priority);
        Assert.AreEqual(_studentProblem.Student,_student);
        Assert.AreEqual(_studentProblem.Teacher,_teacher);
        Assert.AreEqual(_studentProblem.StudentId,_student.Id);
        Assert.AreEqual(_studentProblem.TeacherId,_teacher.Id);

    }
    
    [Test]
    public void Teacher_Change_Property_Changed_Should_Throw_Exception_When_NULL()
    {
        Assert.Throws<ArgumentNullException>(() => _teacher.StudentProblems = null);

    }

    [Test]
    public void Teacher_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_teacher.Id,0);
        Assert.AreEqual(_teacher.StudentProblems,null);
        
        _teacher.Id = _id;

        Assert.AreEqual(_teacher.Id,_id);
    }
}