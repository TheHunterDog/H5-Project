using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    public class NodesDiagramViewModel
    {
        private NodesDiagramModel _nodesDiagram = new NodesDiagramModel();

        private NodesDiagramView _nodesDiagramView;

        public NodesDiagramViewModel(NodesDiagramView view)
        {
            _nodesDiagramView = view;
        }

        void ConnectBetweenModelAndView()
        {
            _nodesDiagramView.StudentSupervisorStudentsAndMeetingsNodes = _nodesDiagram.GetStudentSupervisorStudentsAndMeetingsGroups();
        }

        public void GetDataFromDataBase()
        {
            _nodesDiagram.LoadFromDatabase();
            ConnectBetweenModelAndView();
        }
        public void GetExampleData()
        {
            ConnectBetweenModelAndView();
        }
        public void SetTestData(Student[] studentsT, StudentSupervisor[] studentSupervisorsT, StudentSupervisorMeeting[] meetingsT)
        {
            _nodesDiagram.LoadFromTest(studentsT, studentSupervisorsT, meetingsT);
            ConnectBetweenModelAndView();
        }

        public NodesDiagramModel NodesDiagramModel()
        {
            return _nodesDiagram;
        }
    }
}
