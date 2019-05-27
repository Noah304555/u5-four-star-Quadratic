using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _304555Quadratic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Global Variables
        double descriminant = 0;
        double previousX = 0;
        double previousY = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            //Values of a, b and c
            int IndexX = txtStatement.Text.IndexOf("x");
            string Input = txtStatement.Text;

            //A 
            double A = 0;
            string Value1 = Input.Substring(0, IndexX);
            int previousPosX = IndexX;
            IndexX = txtStatement.Text.IndexOf("x", IndexX + 1);

            //B 
            double B = 0;
            string Value2 = Input.Substring(previousPosX + 5, IndexX - previousPosX - 5);
            int indexOfEqual = txtStatement.Text.IndexOf("=");

            //C
            double C = 0;
            string Value3 = Input.Substring(IndexX + 4, indexOfEqual - IndexX - 5);

            //Using double for values to store decimals
            double.TryParse(Value1, out A);
            double.TryParse(Value2, out B);
            double.TryParse(Value3, out C);

            //Quadratic Formula
            if (Input.Substring(0, 1) == "-")
            {
                A = -A;
            }
            if (Input.Substring(previousPosX + 3, IndexX - previousPosX - 5) == "-")
            {
                B = -B;
            }

            if (Input.Substring(IndexX + 2, indexOfEqual - IndexX - 6) == "-")
            {
                C = -1 * C;
            }

            descriminant = Math.Sqrt((B * B) - (4 * A * C));

            double Root1 = (-B + descriminant) / (2 * A);
            lblOutput.Content = "Root #1 " + Root1.ToString() + "        ";

            double Root2 = (-B - descriminant) / (2 * A);
            lblOutput.Content += "Root #2 " + Root2.ToString();

            //Making the different input components disapear 
            txtStatement.Visibility = Visibility.Hidden;
            Calculate.Visibility = Visibility.Hidden;
            lbltitle.Visibility = Visibility.Hidden;
            lblLegend.Visibility = Visibility.Visible;

            //Lines on the graph 
            for (int i = 0; i <= 40; i++)
            {

                myCanvas.Children.Add(CreateLine(50, 50 + i * 10, 450, 50 + i * 10, 1));

                myCanvas.Children.Add(CreateLine(50 + i * 10, 50, 50 + i * 10, 450, 1));
            }

            //Root 1 point
            Ellipse Root1dot = new Ellipse();
            Canvas.SetTop(Root1dot, 246);
            Root1dot.Width = 8;
            Root1dot.Height = 8;
            Canvas.SetLeft(Root1dot, 246 + 10 * Root1 / 2);
            Root1dot.Fill = Brushes.Black;
            myCanvas.Children.Add(Root1dot);

            //Root 2 point
            Ellipse Root2dot = new Ellipse();
            Canvas.SetTop(Root2dot, 246);
            Root2dot.Width = 8;
            Root2dot.Height = 8;
            Canvas.SetLeft(Root2dot, 246 + 10 * Root2 / 2);
            Root2dot.Fill = Brushes.Black;
            myCanvas.Children.Add(Root2dot);

            //Placing points
            double numberOfSteps = 0;
            if (Root1 > 0 && Root2 > 0)
            {
                numberOfSteps = (Root2 + Root1) / 2;
            }
            else if (Root1 < 0 || Root2 < 0)
            {
                numberOfSteps = (Root1 - Root2) / 2;
                if (numberOfSteps < 0)
                {
                    numberOfSteps = -numberOfSteps;
                }
            }
            else
            {
                numberOfSteps = (Root2 + Root2) / 2;
                numberOfSteps = -numberOfSteps;
            }

            double FirstStep = A * ((numberOfSteps * numberOfSteps) - (numberOfSteps - 1) * (numberOfSteps - 1));

            for (double i = -50; i <= 50; i++)//Creating the parabola!!!
            {
                myCanvas.Children.Add(CreateLine(i * 5 + 250, -((A * (i * i) + B * i + C) * 5 - 250), (i + 1) * 5 + 250, -((A * ((i + 1) * (i + 1)) + B * (i + 1) + C) * 5 - 250), 2));
                //Standard Form
            }
        }

        private Line CreateLine(double x1, double y1, double x2, double y2, double thickness)
        {
            //Creating lines for graph for parabola 
            Line myLine = new Line();

            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;
            myLine.StrokeThickness = thickness;
            if (y1 == 250)
            {
                myLine.StrokeThickness = 2;
            }
            if (x1 == 250)
            {
                myLine.StrokeThickness = 2;
            }
            previousX = x2;
            previousY = y2;
            return myLine;
        }
    }
}
