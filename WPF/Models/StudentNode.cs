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
                _position = new Vector(440 + (value.X * 120), value.Y);
            }
        }
    }

    public class StudentAndMeetingsNode
    {
        public StudentNode StudentNode { get; set; }
        public StudentSupervisorMeetingNode[] StudentSupervisorMeetingNodes { get; set; }
    }

    public class StudentSupervisorStudentsAndMeetingsNode
    {
        public StudentSupervisorNode StudentSupervisorNode { get; set; }
        public StudentAndMeetingsNode[] StudentAndMeetingNodes { get; set; }
    }
}
