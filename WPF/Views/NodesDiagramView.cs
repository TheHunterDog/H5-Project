﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.Views
{
    public class NodesDiagramView
    {
        public StudentSupervisorStudentsAndMeetingsNode[] StudentSupervisorStudentsAndMeetingsNodes { get; set; }

        public int VerticalCanvasSize { get
            {
                int size = 100;
                for (int i = 0; i < StudentSupervisorStudentsAndMeetingsNodes.Length; i++)
                {
                    size += 100 * StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes.Length;
                }
                return size;
            } 
        }

        public int HorizontalCanvasSize
        {
            get
            {
                int size = 500;
                for (int i = 0; i < StudentSupervisorStudentsAndMeetingsNodes.Length; i++)
                {
                    for (int j = 0; j < StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes.Length; j++)
                    {
                        int meetings = StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentSupervisorMeetingNodes.Length;
                        if (meetings * 120 + 520 > size) 
                            size = meetings * 120 + 520;
                    }
                }
                return size;
            }
        }
    }
}