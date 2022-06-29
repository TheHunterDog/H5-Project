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
        // example values for testing
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

        // loads all students, studentsupervisors and meetings from the database
        public void LoadFromDatabase()
        {
            students = null; studentSupervisors = null; studentSupervisorMeetings = null;

            // loading from database logic
            using (var context = new DatabaseContext())
            {
                students = context.Student.ToArray();
                studentSupervisors = context.StudentSupervisor.ToArray();
                studentSupervisorMeetings = context.StudentSupervisorMeeting.ToArray();
            }
        }

        public void LoadFromTest(Student[] studentsT, StudentSupervisor[] studentSupervisorsT, StudentSupervisorMeeting[] meetingsT)
        {
            students = null; studentSupervisors = null; studentSupervisorMeetings = null;

            studentSupervisorMeetings = meetingsT;
            studentSupervisors = studentSupervisorsT;
            students = studentsT;
        }

        // returns all students that were loaded from the database
        public Student[] GetStudents()
        {
            return students;
        }
        // returns all studentsupervisors that were loaded from the database
        public StudentSupervisor[] GetStudentSupervisors()
        {
            return studentSupervisors;
        }
        // returns all studentsupervisormeetings that were loaded from the database
        public StudentSupervisorMeeting[] GetStudentSupervisorMeetings()
        {
            return studentSupervisorMeetings;
        }

        // returns all students from a studentsupervisor that were loaded from the database
        public Student[] GetStudentSupervisorStudents(StudentSupervisor studentSupervisor)
        {
            return GetStudents().Where(s => s.StudentSupervisor == studentSupervisor.Id).ToArray();
        }
        // returns a node object that contains the position and studentdata
        public StudentNode CreateStudentNode(Student student, int previousStudents)
        {
            // create student node
            StudentNode node = new StudentNode();
            node.Student = student;
            // setting x position is irrelevant(REF StudentNode Getter), y position is based on its index
            node.Position = new System.Windows.Vector(0, 50 + 100 * previousStudents);

            return node;
        }
        // returns a node object that contains the position and studentsupervisordata
        public StudentSupervisorNode CreateStudentSupervisorNode(StudentSupervisor studentSupervisor, int previousStudents, int currentStudents)
        {
            // create studentsupervisor node
            StudentSupervisorNode node = new StudentSupervisorNode();
            node.StudentSupervisor = studentSupervisor;
            // setting x position is irrelevant(REF StudentSupervisorNode Getter), y position is based on how many students already exist and how many students the supervisor has
            node.Position = new System.Windows.Vector(0, 50 + 100 * (previousStudents) + 50 * Math.Clamp(currentStudents - 1, 0, double.MaxValue));

            return node;
        }
        // returns a node object that contains the position and studentsupervisordata
        public StudentSupervisorMeetingNode CreateStudentSupervisorMeetingNode(StudentNode student, StudentSupervisorMeeting studentSupervisorMeeting, int index)
        {
            // create studentsupervisormeeting node
            StudentSupervisorMeetingNode node = new StudentSupervisorMeetingNode();
            node.StudentSupervisorMeeting = studentSupervisorMeeting;
            // setting x position is index(REF StudentSupervisorMeetingNode Getter), y position is based on studentNode y position
            node.Position = new System.Windows.Vector(index, student.Position.Y + 10);

            return node;
        }
        // returns a group that contains a studentnode and an array of meetingnodes of that student
        public StudentAndMeetingsGroup CreateStudentAndMeetingsGroup(StudentNode student)
        {
            // create studentsupervisormeeting node
            StudentAndMeetingsGroup group = new StudentAndMeetingsGroup();
            group.StudentNode = student;
            group.StudentSupervisorMeetingNodes = GetStudentSupervisorMeetingNodes(student);

            return group;
        }
        // returns a group that contains a studentsupervisornode and an array of studentandmeetingsGroups of that studentsupervisor
        public StudentSupervisorStudentsAndMeetingsGroup CreateStudentSupervisorStudentsAndMeetingsGroup(StudentSupervisor studentSupervisor, int previousStudents, out int currentStudents)
        {
            Student[] studentsFromSupervisor = GetStudentSupervisorStudents(studentSupervisor);
            currentStudents = studentsFromSupervisor.Length;
            if (currentStudents > 0)
            {
                // create studentsupervisormeeting node
                StudentSupervisorStudentsAndMeetingsGroup group = new StudentSupervisorStudentsAndMeetingsGroup();

                group.StudentSupervisorNode = CreateStudentSupervisorNode(studentSupervisor, previousStudents, studentsFromSupervisor.Length);
                group.StudentAndMeetingGroups = GetStudentAndMeetingsGroups(studentSupervisor, previousStudents);

                return group;
            }
            else return null;
        }

        // returns a node object that contains the position and studentsupervisormeetingdata
        public StudentSupervisorMeetingNode[] GetStudentSupervisorMeetingNodes(StudentNode student)
        {
            // get all meetings involving a certain student
            StudentSupervisorMeeting[] studentSupervisorMeetingsArray = GetStudentSupervisorMeetings().Where(s => s.Student == student.Student).ToArray();
            // create studentsupervisormeeting nodes
            StudentSupervisorMeetingNode[] studentSupervisorMeetingNodes = new StudentSupervisorMeetingNode[studentSupervisorMeetingsArray.Length];
            for (int i = 0; i < studentSupervisorMeetingNodes.Length; i++)
            {
                // create studentsupervisormeeting node
                studentSupervisorMeetingNodes[i] = CreateStudentSupervisorMeetingNode(student, studentSupervisorMeetingsArray[i], i);
            }

            return studentSupervisorMeetingNodes;
        }
        // returns a node object that contains a studentnode and an array of studentsupervisormeetingnodes
        public StudentAndMeetingsGroup[] GetStudentAndMeetingsGroups(StudentSupervisor studentSupervisor, int previousStudents)
        {
            // get students from studentSupervisor
            Student[] studentsArray = GetStudentSupervisorStudents(studentSupervisor);
            // create studentNodes array
            StudentNode[] studentNodes = new StudentNode[studentsArray.Length];
            for (int i = 0; i < studentsArray.Length; i++)
            {
                // create studentNodes
                studentNodes[i] = CreateStudentNode(studentsArray[i], previousStudents + i);
            }
            // create studentAndMeetingsGroup item for each student
            StudentAndMeetingsGroup[] studentAndMeetingsGroup = new StudentAndMeetingsGroup[studentNodes.Length];
            for (int i = 0; i < studentNodes.Length; i++)
            {
                studentAndMeetingsGroup[i] = CreateStudentAndMeetingsGroup(studentNodes[i]);

            }

            return studentAndMeetingsGroup;
        }
        // returns a node object that contains the studentsupervisor and an array of studentandmeetingnodes
        public StudentSupervisorStudentsAndMeetingsGroup[] GetStudentSupervisorStudentsAndMeetingsGroups()
        {
            StudentSupervisor[] studentSupervisors = GetStudentSupervisors();

            List<StudentSupervisorStudentsAndMeetingsGroup> studentSupervisorStudentsAndMeetingsNodes = new List<StudentSupervisorStudentsAndMeetingsGroup>();

            int previousStudents = 0;

            for (int i = 0; i < studentSupervisors.Length; i++)
            {
                int studentsFromSupervisorCount = 0;
                StudentSupervisorStudentsAndMeetingsGroup? group = CreateStudentSupervisorStudentsAndMeetingsGroup(studentSupervisors[i], previousStudents, out studentsFromSupervisorCount);
                if (group != null)
                {
                    studentSupervisorStudentsAndMeetingsNodes.Add(group);
                    previousStudents += studentsFromSupervisorCount;
                }
            }

            return studentSupervisorStudentsAndMeetingsNodes.ToArray();
        }
    }
}