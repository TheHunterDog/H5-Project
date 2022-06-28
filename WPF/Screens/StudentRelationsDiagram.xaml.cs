using Database.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPF.Models;
using WPF.ViewModels;
using WPF.Views;

namespace WPF.Screens
{
    /// <summary>
    /// Interaction logic for StudentRelationsDiagram.xaml
    /// </summary>
    public partial class StudentRelationsDiagram : Window
    {
        private double _canvasScale = 1;
        private double _canvasHorizontalScroll = 0, _canvasVerticalScroll = 0;
        private Vector _canvasSize = new Vector(500, 0);

        private ScaleTransform scaleTransform = new ScaleTransform(1, 1);
        private TranslateTransform translateTransform = new TranslateTransform(0, 0);

        public StudentRelationsDiagram()
        {
            InitializeComponent();

            // initialize view
            NodesDiagramView view = new NodesDiagramView();

            // draw empty canvas
            DrawCanvas(view);

            // draw data on canvas async for performance
            Canvas.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    // initialize model
                    NodesDiagramViewModel nodesDiagramViewModel = new NodesDiagramViewModel(view);

                    // populate view
                    nodesDiagramViewModel.GetData();

                    // draw data on canvas
                    DrawCanvas(view);
                }
                )
            );
        }

        private void DrawCanvas(NodesDiagramView view)
        {
            // draw background
            Brush brush = new SolidColorBrush(Color.FromRgb(127, 127, 127));
            Canvas.Background = brush;

            // create transform groups
            TransformGroup myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(scaleTransform);
            myTransformGroup.Children.Add(translateTransform);

            Canvas.RenderTransform = myTransformGroup;

            // check for content
            if (view.StudentSupervisorStudentsAndMeetingsNodes == null) return;

            // determine canvas size
            _canvasSize = new Vector(view.HorizontalCanvasSize, view.VerticalCanvasSize);

            // for each studentsupervisor block
            for (int i = 0; i < view.StudentSupervisorStudentsAndMeetingsNodes.Length; i++)
            {
                // draw studentsupervisor
                DrawCoachToCanvas(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentSupervisorNode);
                // draw vertical line
                DrawLineToCanvas(
                    new Vector(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentSupervisorNode.Position.X + 149, 
                    view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[0].StudentNode.Position.Y + 25), 
                    (view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes.Length - 1) * 100, false);
                // for each student
                for (int j = 0; j < view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes.Length; j++)
                {
                    // draw student
                    DrawStudentToCanvas(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentNode);
                    // for each meeting
                    for (int k = 0; k < view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentSupervisorMeetingNodes.Length; k++)
                    {
                        // draw meeting
                        DrawMeetingToCanvas(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentSupervisorMeetingNodes[k]);
                    }
                    // draw horizontal meeting line
                    DrawLineToCanvas(
                        new Vector(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentNode.Position.X + 100, 
                        view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentNode.Position.Y + 4), 
                        120 * Math.Clamp(view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentSupervisorMeetingNodes.Length - 1, 0, int.MaxValue) + 
                        (view.StudentSupervisorStudentsAndMeetingsNodes[i].StudentAndMeetingNodes[j].StudentSupervisorMeetingNodes.Length > 0 ? 81 : 0), true);
                }
            }

        }

        private void DrawStudentToCanvas(StudentNode studentNode)
        {
            // draw backdrop
            DrawImageToCanvas(studentNode.Position, new Uri("C:/Users/evert/Downloads/H5-Project-main_english/H5-Project-main_english/WPF/Images/Nodes_Student.png", UriKind.Absolute), new Vector(100, 50));
            // draw number
            DrawTextToCanvas(new Vector(studentNode.Position.X + 5, studentNode.Position.Y), studentNode.Student.StudentNumber, Color.FromRgb(255, 255, 255));
            // draw name
            DrawTextToCanvas(new Vector(studentNode.Position.X + 5, studentNode.Position.Y + 16), $"{studentNode.Student.FirstName} {studentNode.Student.MiddleName} {studentNode.Student.LastName}", Color.FromRgb(0, 0, 0));
            // draw class
            DrawTextToCanvas(new Vector(studentNode.Position.X + 5, studentNode.Position.Y + 28), studentNode.Student.ClassCode, Color.FromRgb(0, 0, 0));
            // draw line to studentsupervisor
            DrawLineToCanvas(new Vector(studentNode.Position.X - 50, studentNode.Position.Y + 24), 50, true);
        }

        private void DrawCoachToCanvas(StudentSupervisorNode studentSupervisorNode)
        {
            // draw backdrop
            DrawImageToCanvas(studentSupervisorNode.Position, new Uri("C:/Users/evert/Downloads/H5-Project-main_english/H5-Project-main_english/WPF/Images/Nodes_Coach.png", UriKind.Absolute), new Vector(100, 50));
            // draw code
            DrawTextToCanvas(new Vector(studentSupervisorNode.Position.X + 5, studentSupervisorNode.Position.Y), studentSupervisorNode.StudentSupervisor.TeacherCode, Color.FromRgb(255, 255, 255));
            // draw name
            DrawTextToCanvas(new Vector(studentSupervisorNode.Position.X + 5, studentSupervisorNode.Position.Y + 16), studentSupervisorNode.StudentSupervisor.Name, Color.FromRgb(0, 0, 0));
            // draw line from studentsupervisor
            DrawLineToCanvas(new Vector(studentSupervisorNode.Position.X + 100, studentSupervisorNode.Position.Y + 24), 50, true);
        }

        private void DrawMeetingToCanvas(StudentSupervisorMeetingNode studentSupervisorMeetingNode)
        {
            // draw backdrop
            DrawImageToCanvas(studentSupervisorMeetingNode.Position, new Uri("C:/Users/evert/Downloads/H5-Project-main_english/H5-Project-main_english/WPF/Images/Nodes_Meeting.png", UriKind.Absolute), new Vector(80, 40));
            // draw completed-mark
            if(studentSupervisorMeetingNode.StudentSupervisorMeeting.Done) DrawImageToCanvas(studentSupervisorMeetingNode.Position, new Uri("C:/Users/evert/Downloads/H5-Project-main_english/H5-Project-main_english/WPF/Images/Nodes_MeetingDone.png", UriKind.Absolute), new Vector(80, 40));
            // draw header
            DrawTextToCanvas(new Vector(studentSupervisorMeetingNode.Position.X + 5, studentSupervisorMeetingNode.Position.Y), "Gesprek:", Color.FromRgb(255, 255, 255), 8);
            // draw date and time
            DrawTextToCanvas(new Vector(studentSupervisorMeetingNode.Position.X + 5, studentSupervisorMeetingNode.Position.Y + 10), studentSupervisorMeetingNode.StudentSupervisorMeeting.MeetingDate.ToShortDateString(), Color.FromRgb(0, 0, 0), 8);
            DrawTextToCanvas(new Vector(studentSupervisorMeetingNode.Position.X + 5, studentSupervisorMeetingNode.Position.Y + 18), studentSupervisorMeetingNode.StudentSupervisorMeeting.MeetingDate.TimeOfDay.ToString(), Color.FromRgb(0, 0, 0), 8);
            // draw line up
            DrawLineToCanvas(new Vector(studentSupervisorMeetingNode.Position.X + 39, studentSupervisorMeetingNode.Position.Y - 5), 5, false);
        }

        private void DrawImageToCanvas(Vector vector, Uri uri, Vector size)
        {
            // create image
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.EndInit();

            // paint image on rect
            Brush brush = new ImageBrush(image);
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = brush;

            // set size
            rectangle.Width = size.X;
            rectangle.Height = size.Y;

            // add image to canvas
            Canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, vector.X);
            Canvas.SetTop(rectangle, vector.Y);
        }

        private void DrawTextToCanvas(Vector vector, string value, Color color, double fontSize = 10)
        {
            // create textblock
            TextBlock textBlock = new TextBlock();
            textBlock.Text = value;
            textBlock.FontSize = fontSize;
            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            //add text to canvas
            Canvas.Children.Add(textBlock);
            Canvas.SetLeft(textBlock, vector.X + 5);
            Canvas.SetTop(textBlock, vector.Y);
        }

        private void DrawLineToCanvas(Vector from, double length, bool horizontal)
        {
            // paint color on rect
            Brush brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = brush;

            // set size
            rectangle.Width = horizontal ? length : 2;
            rectangle.Height = !horizontal ? length : 2;

            // add image to canvas
            Canvas.Children.Add(rectangle);
            Canvas.SetLeft(rectangle, from.X);
            Canvas.SetTop(rectangle, from.Y);
        }

        private void StudentRelationsDiagram_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // zoom on scroll + LeftCTRL
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyUp(Key.LeftCtrl))
            {
                // update value
                _canvasScale += ((e.Delta > 0) ? .1 : -.1);
                // clamp value
                _canvasScale = Math.Clamp(_canvasScale, 1, 2);
                // update viewbox
                UpdateViewBoxZoom(_canvasScale);
                // clamp values, has to be redone since size has changed by zooming
                _canvasHorizontalScroll = Math.Clamp(_canvasHorizontalScroll, 0, Math.Clamp((_canvasSize.X * scaleTransform.ScaleX) - Width, 0, double.MaxValue));
                _canvasVerticalScroll = Math.Clamp(_canvasVerticalScroll, 0, Math.Clamp((_canvasSize.Y * scaleTransform.ScaleY) - Height, 0, double.MaxValue));
                // update viewbox
                UpdateViewBoxScroll(new Vector(-_canvasHorizontalScroll, -_canvasVerticalScroll));
            }
            // scroll horizontal on scroll + LeftShift
            else if (Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyUp(Key.LeftShift))
            {
                // update value
                _canvasHorizontalScroll -= ((e.Delta > 0) ? 50 : -50);
                // clamp value
                _canvasHorizontalScroll = Math.Clamp(_canvasHorizontalScroll, 0, Math.Clamp((_canvasSize.X * scaleTransform.ScaleX) - Width, 0, double.MaxValue));
                // update viewbox
                UpdateViewBoxScroll(new Vector(-_canvasHorizontalScroll, -_canvasVerticalScroll));
            }
            // scroll vertical on scroll + LeftShift
            else
            {
                // update value
                _canvasVerticalScroll -= ((e.Delta > 0) ? 50 : -50);
                // clamp value
                _canvasVerticalScroll = Math.Clamp(_canvasVerticalScroll, 0, Math.Clamp((_canvasSize.Y * scaleTransform.ScaleY) - Height, 0, double.MaxValue));
                // update viewbox
                UpdateViewBoxScroll(new Vector(_canvasHorizontalScroll, -_canvasVerticalScroll));
            }
        }

        private void UpdateViewBoxZoom(double scale)
        {
            // create new scaleTransform
            scaleTransform = new ScaleTransform(scale, scale);
            // remove current transforms
            ((TransformGroup)Canvas.RenderTransform).Children.Clear();
            // reassign transforms
            ((TransformGroup)Canvas.RenderTransform).Children.Add(scaleTransform);
            ((TransformGroup)Canvas.RenderTransform).Children.Add(translateTransform);
            // update size
            ZoomViewbox.Width = Canvas.ActualWidth;
            ZoomViewbox.Height = Canvas.ActualHeight;
        }

        private void UpdateViewBoxScroll(Vector scroll)
        {            
            // create new translateTransform
            translateTransform = new TranslateTransform(scroll.X, scroll.Y);
            // remove current transforms
            ((TransformGroup)Canvas.RenderTransform).Children.Clear();
            // reassign transforms
            ((TransformGroup)Canvas.RenderTransform).Children.Add(scaleTransform);
            ((TransformGroup)Canvas.RenderTransform).Children.Add(translateTransform);
            // update size
            ZoomViewbox.Width = Canvas.ActualWidth;
            ZoomViewbox.Height = Canvas.ActualHeight;
        }

        // called when the window size gets changed
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // update scroll limits
            _canvasHorizontalScroll = Math.Clamp(_canvasHorizontalScroll, 0, Math.Clamp((_canvasSize.X * scaleTransform.ScaleX) - Width, 0, double.MaxValue));
            _canvasVerticalScroll = Math.Clamp(_canvasVerticalScroll, 0, Math.Clamp((_canvasSize.Y * scaleTransform.ScaleY) - Height, 0, double.MaxValue));
            // update viewbox
            UpdateViewBoxScroll(new Vector(-_canvasHorizontalScroll, -_canvasVerticalScroll));
        }
    }
}
