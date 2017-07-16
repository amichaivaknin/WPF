using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleDraw
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush _color;
        private bool _mouseClicked;
        private double _thickness;
        private double _x;
        private double _y;

        public MainWindow()
        {
            InitializeComponent();
            _mouseClicked = false;
            _color = new SolidColorBrush(Colors.Black);
            _thickness = 1;
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var position = e.GetPosition(this);
            SetCoordinates(position);
            _mouseClicked = true;
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _mouseClicked = false;
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseClicked)
            {
                var position = e.GetPosition(this);
                AddLineToCanvas(position);
                SetCoordinates(position);
            }
        }

        private void RectangleElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var color = ((Rectangle) sender).Fill;
            _color = color;
            e.Handled = true;
        }

        private void ThicknessButtonElement_OnMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var thickness = ((Button)sender).Content.ToString();
            switch (thickness)
            {
                case "Thin":
                    _thickness = 1;
                    break;
                case "Medium":
                    _thickness = 3;
                    break;
                case "Thick":
                    _thickness = 5;
                    break;
            }
        }

        private void AddLineToCanvas(Point position)
        {
            var line = new Line
            {
                X1 = _x,
                Y1 = _y,
                X2 = position.X,
                Y2 = position.Y,
                Stroke = _color,
                StrokeThickness = _thickness
            };
            Canvas.Children.Add(line);
        }

        private void SetCoordinates(Point position)
        {
            _x = position.X;
            _y = position.Y;
        }
    }
}