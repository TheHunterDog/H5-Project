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

    private StudentSupervisor _studentSupervisor = new StudentSupervisor();
    private string? _name = "randy bonenwaag";
    private string _docentcode = "DOC345563";

    #endregion

    #region studentProblem

    private StudentProblem _studentProblem = new StudentProblem();

    private string _description =
        "Windesheim is een goeie school met veel gemotiveerde studenten. Hier kan je veel leren over diverse vakken.";

    private int _priority = 15;
    #endregion

    #region Completefilled models
    
    public StudentSupervisorMeeting StudentSupervisorMeeting = new StudentSupervisorMeeting()
    {
        MeetingDate = DateTime.Now, Comments = "Voortgang bespreken", Student = new Student(), StudentId = 0,
        StudentSupervisor = new StudentSupervisor(), StudentSupervisorId = 0, Done = false
    };
    
    public StudentSupervisor StudentSupervisor = new StudentSupervisor()
    {
        TeacherCode = "DOC231253", Id = 1234, Name = "karenBrakband", StudentSupervisorMeetings = new StudentSupervisorMeeting[]{new StudentSupervisorMeeting()}, Students = new Student[]{new Student()}
    };
    
    public StudentProblem StudentProblem = new StudentProblem()
    {
        Id = 12, Description = "Gepest", Priority = 10, Student = new Student(), StudentId = 0, Teacher = new Teacher(), TeacherId = 0
    };
    
    #endregion

    #region Teacher

    private Teacher _teacher = new Teacher();

    #endregion

    #region StudentSupervisorMeeting

    private StudentSupervisorMeeting _studentSupervisorMeeting = new StudentSupervisorMeeting();
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
        Assert.Throws<ArgumentNullException>(() => _studentSupervisor.TeacherCode = null);
        Assert.Throws<ArgumentNullException>(() => _studentSupervisor.Name = null);
        Assert.Throws<ArgumentNullException>(() => _studentSupervisor.Students = null);
        Assert.Throws<ArgumentNullException>(() => _studentSupervisor.StudentSupervisorMeetings = null);
    }
    
    [Test]
    public void StudentBegeleider_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentSupervisor.Name,null);
        Assert.AreEqual(_studentSupervisor.TeacherCode,null);
        Assert.AreEqual(_studentSupervisor.Id,0);
        Assert.AreEqual(_studentSupervisor.Students,null);
        Assert.AreEqual(_studentSupervisor.StudentSupervisorMeetings, null);


        _studentSupervisor.Name = _name;
        _studentSupervisor.TeacherCode = _docentcode;
        _studentSupervisor.Id = _id;
        _studentSupervisor.Students = new[] {_student};
        _studentSupervisor.StudentSupervisorMeetings = new[] {StudentSupervisorMeeting};
        
        Assert.AreEqual(_studentSupervisor.Name,_name);
        Assert.AreEqual(_studentSupervisor.TeacherCode,_docentcode);
        Assert.AreEqual(_studentSupervisor.Id,_id);
        Assert.AreEqual(_studentSupervisor.Students,new [] {_student});
        Assert.AreEqual(_studentSupervisor.StudentSupervisorMeetings, new[] {StudentSupervisorMeeting});
        
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
        _student.Studentbegeleider = _studentSupervisor;        
        _student.StudentBegeleiderGesprekken = new []{StudentSupervisorMeeting};
        _student.StudentProblems = new []{_studentProblem};
        _student.StudentbegeleiderId = _id;
        
        Assert.AreEqual(_student.Achternaam,_lastname);
        Assert.AreEqual(_student.Voornaam,_firstname);
        Assert.AreEqual(_student.Klasscode,_klassencode);
        Assert.AreEqual(_student.Tussenvoegsel,_lastnameprefix);
        Assert.AreEqual(_student.Id,_id);
        Assert.AreEqual(_student.Studentnummer,_studentnummer);
        Assert.AreEqual(_student.Studentbegeleider,_studentSupervisor);
        Assert.AreEqual(_student.StudentbegeleiderId,_id);
        Assert.AreEqual(_student.StudentBegeleiderGesprekken,new []{StudentSupervisorMeeting});
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
        Assert.Throws<ArgumentNullException>(() => _studentSupervisorMeeting.Comments = null);
        Assert.Throws<ArgumentNullException>(() => _studentSupervisorMeeting.Student = null);
        Assert.Throws<ArgumentNullException>(() => _studentSupervisorMeeting.StudentSupervisor = null);
    }

    [Test]
    public void StudentBegeleiderGesprekken_Change_Property_changed_Should_Change()
    {
        Assert.AreEqual(_studentSupervisorMeeting.Comments,null);
        Assert.AreEqual(_studentSupervisorMeeting.Student,null);
        Assert.AreEqual(_studentSupervisorMeeting.StudentSupervisor,null);
        Assert.AreEqual(_studentSupervisorMeeting.Done,false);
        Assert.AreEqual(_studentSupervisorMeeting.MeetingDate,new DateTime());
        Assert.AreEqual(_studentSupervisorMeeting.StudentId,0);
        Assert.AreEqual(_studentSupervisorMeeting.StudentSupervisorId,0);

        _studentSupervisorMeeting.Comments = _description;
        _studentSupervisorMeeting.Student = _student;
        _studentSupervisorMeeting.Done = _completed;
        _studentSupervisorMeeting.MeetingDate = _date;
        _studentSupervisorMeeting.StudentSupervisor = _studentSupervisor;
        _studentSupervisorMeeting.StudentId = _id;
        _studentSupervisorMeeting.StudentSupervisorId = _id;
        
        Assert.AreEqual(_studentSupervisorMeeting.Comments,_description);
        Assert.AreEqual(_studentSupervisorMeeting.Student,_student);
        Assert.AreEqual(_studentSupervisorMeeting.Done,_completed);
        Assert.AreEqual(_studentSupervisorMeeting.MeetingDate,_date);
        Assert.AreEqual(_studentSupervisorMeeting.StudentSupervisor,_studentSupervisor);
        Assert.AreEqual(_studentSupervisorMeeting.StudentId,_id);
        Assert.AreEqual(_studentSupervisorMeeting.StudentSupervisorId,_id);

    }

}