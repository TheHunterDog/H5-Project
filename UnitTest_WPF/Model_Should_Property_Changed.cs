#region

using System;
using Database.Model;
using NUnit.Framework;

#endregion

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
        GesprekDatum = DateTime.Now, Opmerkingen = "Voortgang bespreken", Student = new Student(), StudentId = 0,
        StudentBegeleider = new StudentBegeleider(), StudentBegeleiderId = 0, Voltooid = false
    };
    
    public StudentBegeleider studentBegeleider = new StudentBegeleider()
    {
        Docentcode = "DOC231253", Id = 1234, Naam = "karenBrakband", StudentBegeleiderGesprekken = new StudentBegeleiderGesprekken[]{new StudentBegeleiderGesprekken()}, Students = new Student[]{new Student()}
    };
    
    public StudentProblem StudentProblem = new StudentProblem()
    {
        Id = 12, Description = "Gepest", Priority = 10, Student = new Student(), StudentId = 0, Teacher = new Teacher(), TeacherId = 0
    };
    
    #endregion

    #region Teacher

    private Teacher _teacher = new Teacher();

    #endregion

    #region StudentBegeleiderGesprekken

    private StudentBegeleiderGesprekken _studentBegeleiderGesprekken = new StudentBegeleiderGesprekken();
    private bool _completed = true;
    private DateTime _date = DateTime.Now;

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
        Assert.Throws<ArgumentNullException>(() => _studentBegeleider.Students = null);
        Assert.Throws<ArgumentNullException>(() => _studentBegeleider.StudentBegeleiderGesprekken = null);
    }
    
    [Test]
    public void StudentBegeleider_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentBegeleider.Naam,null);
        Assert.AreEqual(_studentBegeleider.Docentcode,null);
        Assert.AreEqual(_studentBegeleider.Id,0);
        Assert.AreEqual(_studentBegeleider.Students,null);
        Assert.AreEqual(_studentBegeleider.StudentBegeleiderGesprekken, null);


        _studentBegeleider.Naam = _name;
        _studentBegeleider.Docentcode = _docentcode;
        _studentBegeleider.Id = _id;
        _studentBegeleider.Students = new[] {_student};
        _studentBegeleider.StudentBegeleiderGesprekken = new[] {StudentBegeleiderGesprekken};
        
        Assert.AreEqual(_studentBegeleider.Naam,_name);
        Assert.AreEqual(_studentBegeleider.Docentcode,_docentcode);
        Assert.AreEqual(_studentBegeleider.Id,_id);
        Assert.AreEqual(_studentBegeleider.Students,new [] {_student});
        Assert.AreEqual(_studentBegeleider.StudentBegeleiderGesprekken, new[] {StudentBegeleiderGesprekken});
        
    }

    
    [Test]
    public void Student_Change_Property_Changed_Should_Throw_Exception_When_NULL()
    {
        Assert.Throws<ArgumentNullException>(() => _student.Voornaam = null);
        Assert.Throws<ArgumentNullException>(() => _student.Achternaam = null);
        Assert.Throws<ArgumentNullException>(() => _student.Klasscode = null);
        Assert.Throws<ArgumentNullException>(() => _student.Tussenvoegsel = null);
        Assert.Throws<ArgumentNullException>(() => _student.Studentnummer = null); 
        Assert.Throws<ArgumentNullException>(() => _student.StudentBegeleiderGesprekken = null); 
        Assert.Throws<ArgumentNullException>(() => _student.StudentProblems = null); 
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
        Assert.AreEqual(_student.Studentbegeleider,null);
        Assert.AreEqual(_student.StudentBegeleiderGesprekken,null);
        Assert.AreEqual(_student.StudentProblems,null);
        Assert.AreEqual(_student.StudentbegeleiderId,0);

        _student.Achternaam = _lastname;
        _student.Voornaam = _firstname;
        _student.Klasscode = _klassencode;
        _student.Tussenvoegsel = _lastnameprefix;
        _student.Id = _id;
        _student.Studentnummer = _studentnummer;
        _student.Studentbegeleider = _studentBegeleider;        
        _student.StudentBegeleiderGesprekken = new []{StudentBegeleiderGesprekken};
        _student.StudentProblems = new []{_studentProblem};
        _student.StudentbegeleiderId = _id;
        
        Assert.AreEqual(_student.Achternaam,_lastname);
        Assert.AreEqual(_student.Voornaam,_firstname);
        Assert.AreEqual(_student.Klasscode,_klassencode);
        Assert.AreEqual(_student.Tussenvoegsel,_lastnameprefix);
        Assert.AreEqual(_student.Id,_id);
        Assert.AreEqual(_student.Studentnummer,_studentnummer);
        Assert.AreEqual(_student.Studentbegeleider,_studentBegeleider);
        Assert.AreEqual(_student.StudentbegeleiderId,_id);
        Assert.AreEqual(_student.StudentBegeleiderGesprekken,new []{StudentBegeleiderGesprekken});
        Assert.AreEqual(_student.StudentProblems, new []{_studentProblem});
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
    [Test]
    public void StudentBegeleiderGesprekken_Change_Property_Changed_Should_Throw_Exception_When_NULL()
    {
        Assert.Throws<ArgumentNullException>(() => _studentBegeleiderGesprekken.Opmerkingen = null);
        Assert.Throws<ArgumentNullException>(() => _studentBegeleiderGesprekken.Student = null);
        Assert.Throws<ArgumentNullException>(() => _studentBegeleiderGesprekken.StudentBegeleider = null);
    }

    [Test]
    public void StudentBegeleiderGesprekken_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentBegeleiderGesprekken.Opmerkingen,null);
        Assert.AreEqual(_studentBegeleiderGesprekken.Student,null);
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentBegeleider,null);
        Assert.AreEqual(_studentBegeleiderGesprekken.Voltooid,false);
        Assert.AreEqual(_studentBegeleiderGesprekken.GesprekDatum,new DateTime());
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentId,0);
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentBegeleiderId,0);

        _studentBegeleiderGesprekken.Opmerkingen = _description;
        _studentBegeleiderGesprekken.Student = _student;
        _studentBegeleiderGesprekken.Voltooid = _completed;
        _studentBegeleiderGesprekken.GesprekDatum = _date;
        _studentBegeleiderGesprekken.StudentBegeleider = _studentBegeleider;
        _studentBegeleiderGesprekken.StudentId = _id;
        _studentBegeleiderGesprekken.StudentBegeleiderId = _id;
        
        Assert.AreEqual(_studentBegeleiderGesprekken.Opmerkingen,_description);
        Assert.AreEqual(_studentBegeleiderGesprekken.Student,_student);
        Assert.AreEqual(_studentBegeleiderGesprekken.Voltooid,_completed);
        Assert.AreEqual(_studentBegeleiderGesprekken.GesprekDatum,_date);
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentBegeleider,_studentBegeleider);
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentId,_id);
        Assert.AreEqual(_studentBegeleiderGesprekken.StudentBegeleiderId,_id);

    }

}