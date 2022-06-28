using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Model;

namespace WPF.Models
{
    public class NodesDiagramModel
    {
        static StudentSupervisor[] studentSupervisors = new StudentSupervisor[] {
            new StudentSupervisor("Piet Post", "BHK01", 10), 
            new StudentSupervisor("Klaas Hak", "BHK02", 11),
            new StudentSupervisor("Daan Steen", "BHK04", 12)
        };
        static Student[] students = new Student[] { 
            new Student("s1234567", "Henk", "", "Jansen", "HBOICTOOSDDh", studentSupervisors[0]), 
            new Student("s1234568", "Loek", "", "Bak", "HBOICTOOSDDh", studentSupervisors[0]), 
            new Student("s1234569", "Jan", "", "Struik", "HBOICTOOSDDh", studentSupervisors[2]), 
            new Student("s1234542", "Koos", "", "Boos", "HBOICTOOSDDh", studentSupervisors[1]),
            new Student("s1234543", "Henk", "", "Jansen", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234544", "Loek", "", "Bak", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234545", "Jan", "", "Struik", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234546", "Koos", "", "Boos", "HBOICTOOSDDh", studentSupervisors[1]),
            new Student("s1234561", "Peter", "van der", "Voort", "HBOICTOOSDDh", studentSupervisors[1])};

        static StudentSupervisorMeeting[] studentSupervisorMeetings = new StudentSupervisorMeeting[] {
            new StudentSupervisorMeeting(students[0], studentSupervisors.Where(s => s.Id == students[0].StudentSupervisor).First(), new DateTime(2022, 06, 08), true),
            new StudentSupervisorMeeting(students[0], studentSupervisors.Where(s => s.Id == students[0].StudentSupervisor).First(), new DateTime(2022, 08, 08), false),
            new StudentSupervisorMeeting(students[1], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 09, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 11, 08), false)};


        public void LoadFromDatabase()
        {
            // loading from database logic here
            using (var context = new DatabaseContext())
            {
                students = context.Student.ToArray();
                studentSupervisors = context.StudentSupervisor.ToArray();
                studentSupervisorMeetings = context.StudentSupervisorMeeting.ToArray();
            }
            // not done because database was changed by someone after I pulled this branch

            // would place data in arrays declared above
        }

        public Student[] GetStudents()
        {
            // get from database
            // Database was changed, creating own data instead

            return students;
        }

        public StudentSupervisor[] GetStudentSupervisors()
        {
            // get from database
            // Database was changed, creating own data instead

            return studentSupervisors;
        }

        public StudentSupervisorMeeting[] GetStudentSupervisorMeetings()
        {
            // get from database
            // Database was changed, creating own data instead

            return studentSupervisorMeetings;
        }

        public Student[] GetStudentSupervisorStudents(StudentSupervisor studentSupervisor)
        {
            return students.Where(s => s.StudentSupervisor == studentSupervisor.Id).ToArray();
        }

        public StudentNode CreateStudentNode(Student student, int previousStudents)
        {
            StudentNode node = new StudentNode();
            node.Student = student;
            node.Position = new System.Windows.Vector(0, 50 + 100 * previousStudents);

            return node;
        }

        public StudentSupervisorNode CreateStudentSupervisorNode(StudentSupervisor studentSupervisor, int previousStudents, int currentStudents)
        {
            StudentSupervisorNode node = new StudentSupervisorNode();
            node.StudentSupervisor = studentSupervisor;
            node.Position = new System.Windows.Vector(0, 50 + 100 * (previousStudents) );//   + 50 * (double)Math.Floor( (decimal)((currentStudents - (currentStudents < 3 ? 1 : 0)) / 2)) );

            return node;
        }
/*
        public StudentNode[] GetStudentNodes(Student[] students) 
        {
            students.GroupBy(s => s.Studentbegeleider.TeacherCode);

            StudentNode[] studentNodes = new StudentNode[students.Length];
            for (int i = 0; i < studentNodes.Length; i++)
            {
                studentNodes[i] = CreateStudentNode(students[i], i);
            }

            return studentNodes;
        }

        public StudentSupervisorNode[] GetStudentSupervisorNodes()
        {
            StudentSupervisor[] studentSupervisors = GetStudentSupervisors();
            studentSupervisors.GroupBy(s => s.TeacherCode);

            StudentSupervisorNode[] studentSupervisorNodes = new StudentSupervisorNode[studentSupervisors.Length];
            for (int i = 0; i < studentSupervisors.Length; i++)
            {
                studentSupervisorNodes[i] = 
            }

            return studentSupervisorNodes;
        }*/

        public StudentSupervisorMeetingNode[] GetStudentSupervisorMeetingNodes(StudentNode student)
        {
            StudentSupervisorMeeting[] studentSupervisorMeetings = GetStudentSupervisorMeetings();
            studentSupervisorMeetings = studentSupervisorMeetings.Where(s => s.Student == student.Student).ToArray();

            StudentSupervisorMeetingNode[] studentSupervisorMeetingNodes = new StudentSupervisorMeetingNode[studentSupervisorMeetings.Length];
            for (int i = 0; i < studentSupervisorMeetingNodes.Length; i++)
            {
                studentSupervisorMeetingNodes[i] = new StudentSupervisorMeetingNode();
                studentSupervisorMeetingNodes[i].StudentSupervisorMeeting = studentSupervisorMeetings[i];
                studentSupervisorMeetingNodes[i].Position = new System.Windows.Vector(i, student.Position.Y + 10);
            }

            return studentSupervisorMeetingNodes;
        }

        public StudentAndMeetingsNode[] GetStudentAndMeetingsNodes(StudentSupervisor studentSupervisor, int previousStudents)
        {
            Student[] studentsArray = GetStudentSupervisorStudents(studentSupervisor);
            StudentNode[] studentNodes = new StudentNode[studentsArray.Length];
            for (int i = 0; i < studentsArray.Length; i++)
            {
                studentNodes[i] = CreateStudentNode(studentsArray[i], previousStudents + i);
            }

            StudentAndMeetingsNode[] studentAndMeetingsNodes = new StudentAndMeetingsNode[studentNodes.Length];
            for (int i = 0; i < studentNodes.Length; i++)
            {
                studentAndMeetingsNodes[i] = new StudentAndMeetingsNode();
                studentAndMeetingsNodes[i].StudentNode = studentNodes[i];
                studentAndMeetingsNodes[i].StudentSupervisorMeetingNodes = GetStudentSupervisorMeetingNodes(studentNodes[i]);
            }

            return studentAndMeetingsNodes;
        }

        public StudentSupervisorStudentsAndMeetingsNode[] GetStudentSupervisorStudentsAndMeetingsNodes()
        {
            StudentSupervisor[] studentSupervisors = GetStudentSupervisors();

            List<StudentSupervisorStudentsAndMeetingsNode> studentSupervisorStudentsAndMeetingsNodes = new List<StudentSupervisorStudentsAndMeetingsNode>();

            int previousStudents = 0;


            for (int i = 0; i < studentSupervisors.Length; i++)
            {
                Student[] studentsFromSupervisor = GetStudentSupervisorStudents(studentSupervisors[i]);
                if (studentsFromSupervisor.Length > 0)
                {
                    StudentSupervisorStudentsAndMeetingsNode node = new StudentSupervisorStudentsAndMeetingsNode();
                    node.StudentSupervisorNode = CreateStudentSupervisorNode(studentSupervisors[i], previousStudents, studentsFromSupervisor.Length);
                    node.StudentAndMeetingNodes = GetStudentAndMeetingsNodes(studentSupervisors[i], previousStudents);

                    studentSupervisorStudentsAndMeetingsNodes.Add(node);

                    previousStudents += studentsFromSupervisor.Length;
                }
            }

            return studentSupervisorStudentsAndMeetingsNodes.ToArray();
        }
    }


}
