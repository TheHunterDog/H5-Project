using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Models
{
    public class StudentNode
    {
        public Student Student { get; set; }

        private Vector _position;
        public Vector Position
        {
            get { return _position; }
            set
            {
                // x position is fixed since all students are placed in a single line
                _position = new Vector(300, value.Y);
            }
        }
    }

    public class StudentSupervisorNode
    {
        public StudentSupervisor StudentSupervisor { get; set; }
        private Vector _position;
        public Vector Position
        {
            get { return _position; }
            set
            {
                // x position is fixed since all studentsupervisors are placed in a single line
                _position = new Vector(100, value.Y);
            }
        }
    }

    public class StudentSupervisorMeetingNode
    {
        public StudentSupervisorMeeting StudentSupervisorMeeting { get; set; }

        private Vector _position;
        public Vector Position
        {
            get { return _position; }
            set
            {
                // x position is based on input x value, which is the index of the meeting for the student
                _position = new Vector(440 + (value.X * 120), value.Y);
            }
        }
    }

    public class StudentAndMeetingsGroup
    {
        public StudentNode StudentNode { get; set; }
        public StudentSupervisorMeetingNode[] StudentSupervisorMeetingNodes { get; set; }
    }

    public class StudentSupervisorStudentsAndMeetingsGroup
    {
        public StudentSupervisorNode StudentSupervisorNode { get; set; }
        public StudentAndMeetingsGroup[] StudentAndMeetingGroups { get; set; }
    }
}
