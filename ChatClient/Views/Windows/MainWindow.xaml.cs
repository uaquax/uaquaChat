using MaterialDesignThemes.Wpf;
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

namespace ChatClient
{
    public partial class MainWindow : Window
    {
        private Point oldPoint;
        private bool isDouble = false;

        public MainWindow()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
        }

        private void TitleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                isDouble = true;
                btnMaximize_Click(sender, e);
            }
            else
            {
                isDouble = false;
                DragMove();
            }
            oldPoint = e.GetPosition(this);
        }

        private void TitleMouseLeave(object sender, MouseEventArgs e)
        {
            var newPoint = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (WindowState == WindowState.Maximized && !isDouble)
                {
                    if (newPoint.Y > oldPoint.Y)
                    {
                        btnMaximize_Click(sender, e);
                        Left = newPoint.X / 2;
                        Top = 0;
                        DragMove();
                    }
                }
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                iconMaximize.Kind = PackIconKind.WindowRestore;
            }
            else
            {
                WindowState = WindowState.Normal;
                iconMaximize.Kind = PackIconKind.WindowMaximize;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
