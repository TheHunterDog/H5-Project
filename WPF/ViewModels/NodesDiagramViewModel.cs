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
            _nodesDiagramView.StudentSupervisorStudentsAndMeetingsNodes = _nodesDiagram.GetStudentSupervisorStudentsAndMeetingsNodes();
        }

        public void GetData()
        {
            //_nodesDiagram.LoadFromDatabase();
            ConnectBetweenModelAndView();
        }
    }
}
