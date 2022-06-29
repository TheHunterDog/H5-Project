#region

using System.Threading;
using NUnit.Framework;
using WPF.Screens;
using WPF.Views;
using WPF.ViewModels;
using WPF.Models;
using Database.Model;
using System.Linq;
using System;
#endregion

namespace UnitTest_WPF_Inplannen
{
    public class NodesDiagramTests
    {
        private NodesDiagramView _view;
        private NodesDiagramViewModel _nodesDiagramViewModel;

        [SetUp]
        public void Setup()
        {
            // initialize view
            _view = new NodesDiagramView();

            // initialize model
            _nodesDiagramViewModel = new NodesDiagramViewModel(_view);

            // example values for testing
            StudentSupervisor[] studentSupervisors = new StudentSupervisor[] {
            new StudentSupervisor("Piet Post", "BHK01", 10),
            new StudentSupervisor("Klaas Hak", "BHK02", 11),
            new StudentSupervisor("Daan Steen", "BHK04", 12)};
            Student[] students = new Student[] {
            new Student("s1234567", "Henk", "", "Jansen", "HBOICTOOSDDh", studentSupervisors[0]),
            new Student("s1234568", "Loek", "", "Bak", "HBOICTOOSDDh", studentSupervisors[0]),
            new Student("s1234569", "Jan", "", "Struik", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234542", "Koos", "", "Boos", "HBOICTOOSDDh", studentSupervisors[1]),
            new Student("s1234543", "Henk", "", "Jansen", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234544", "Loek", "", "Bak", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234545", "Jan", "", "Struik", "HBOICTOOSDDh", studentSupervisors[2]),
            new Student("s1234546", "Koos", "", "Boos", "HBOICTOOSDDh", studentSupervisors[1]),
            new Student("s1234561", "Peter", "van der", "Voort", "HBOICTOOSDDh", studentSupervisors[1])};

            StudentSupervisorMeeting[] studentSupervisorMeetings = new StudentSupervisorMeeting[] {
            new StudentSupervisorMeeting(students[0], studentSupervisors.Where(s => s.Id == students[0].StudentSupervisor).First(), new DateTime(2022, 06, 08), true),
            new StudentSupervisorMeeting(students[0], studentSupervisors.Where(s => s.Id == students[0].StudentSupervisor).First(), new DateTime(2022, 08, 08), false),
            new StudentSupervisorMeeting(students[1], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 09, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[1].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 10, 08), false),
            new StudentSupervisorMeeting(students[2], studentSupervisors.Where(s => s.Id == students[2].StudentSupervisor).First(), new DateTime(2022, 11, 08), false)};

            _nodesDiagramViewModel.SetTestData(students, studentSupervisors, studentSupervisorMeetings);
        }

        [Test]
        public void InitializationNotNull()
        {
            Assert.IsNotNull(_view);
            Assert.IsNotNull(_nodesDiagramViewModel);
            Assert.IsNotNull(_nodesDiagramViewModel.NodesDiagramModel());
        }

        [Test]
        public void InitializationContents()
        {
            Assert.IsTrue(_nodesDiagramViewModel.NodesDiagramModel().GetStudents().Length == 9);
            Assert.IsTrue(_nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisors().Length == 3);
            Assert.IsTrue(_nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorMeetings().Length == 9);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void GetStudentSupervisorStudents(int id)
        {
            StudentSupervisor[] studentSupervisors = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisors();
            Assert.IsTrue(_nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorStudents(studentSupervisors[id]).Length == 2 + id);
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(8)]
        public void CreateStudentNode(int id)
        {
            Student[] students = _nodesDiagramViewModel.NodesDiagramModel().GetStudents();
            StudentNode studentNode = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentNode(students[id], id);
            Assert.IsTrue(studentNode.Student == students[id]);
            Assert.IsTrue(studentNode.Position.Equals(new System.Windows.Vector(300, 50 + 100 * id)));
        }

        [TestCase(0)]
        [TestCase(1)]
        public void CreateStudentSupervisorNode(int id)
        {
            StudentSupervisor[] studentSupervisors = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisors();
            StudentSupervisorNode studentSupervisorNode = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentSupervisorNode(studentSupervisors[id], id, id);
            Assert.IsTrue(studentSupervisorNode.StudentSupervisor == studentSupervisors[id]);
            Assert.IsTrue(studentSupervisorNode.Position.Equals(new System.Windows.Vector(100, 50 + 100 * (id) + 50 * Math.Clamp(id - 1, 0, double.MaxValue))));
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        public void CreateStudentSupervisorMeetingNode(int id)
        {
            StudentSupervisorMeeting[] studentSupervisorMeetings = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorMeetings();
            StudentNode studentNode = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentNode(studentSupervisorMeetings[id].Student, id);
            StudentSupervisorMeetingNode studentSupervisorMeetingNode = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentSupervisorMeetingNode(studentNode, studentSupervisorMeetings[id], id);
            Assert.IsTrue(studentSupervisorMeetingNode.StudentSupervisorMeeting == studentSupervisorMeetings[id]);
            Assert.IsTrue(studentSupervisorMeetingNode.Position.Equals(new System.Windows.Vector(440 + (id * 120), studentNode.Position.Y + 10)));
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(6)]
        public void CreateStudentAndMeetingsGroup(int id)
        {
            Student[] students = _nodesDiagramViewModel.NodesDiagramModel().GetStudents();
            StudentNode studentNode = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentNode(students[id], id);
            StudentAndMeetingsGroup studentGroup = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentAndMeetingsGroup(studentNode);

            Assert.IsTrue(studentGroup.StudentNode == studentNode);
            Assert.IsTrue(studentGroup.StudentSupervisorMeetingNodes.Length == _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorMeetingNodes(studentNode).Length);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void StudentSupervisorStudentsAndMeetingsGroup(int id)
        {
            int output;

            StudentSupervisor[] studentSupervisors = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisors();
            StudentSupervisorStudentsAndMeetingsGroup group = _nodesDiagramViewModel.NodesDiagramModel().CreateStudentSupervisorStudentsAndMeetingsGroup(studentSupervisors[id], id, out output);

            Assert.IsTrue(group.StudentSupervisorNode.StudentSupervisor == studentSupervisors[id]);
            Assert.IsTrue(output == _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorStudents(studentSupervisors[id]).Length);
        }

        [TestCase(0)]
        [TestCase(2)]
        public void GetStudentAndMeetingsGroups(int id)
        {
            StudentSupervisor[] studentSupervisors = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisors();
            StudentAndMeetingsGroup[] groups = _nodesDiagramViewModel.NodesDiagramModel().GetStudentAndMeetingsGroups(studentSupervisors[id], id);

            Assert.IsTrue(groups.Length == _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorStudents(studentSupervisors[id]).Length);
        }

        [Test]
        public void GetStudentSupervisorStudentsAndMeetingsGroups()
        {
            StudentSupervisorStudentsAndMeetingsGroup[] groups = _nodesDiagramViewModel.NodesDiagramModel().GetStudentSupervisorStudentsAndMeetingsGroups();

            Assert.IsTrue(groups.Length == 3);
            Assert.IsTrue(groups[0].StudentAndMeetingGroups.Length == 2);
            Assert.IsTrue(groups[1].StudentAndMeetingGroups.Length == 3);
            Assert.IsTrue(groups[2].StudentAndMeetingGroups.Length == 4);
        }
    }
}