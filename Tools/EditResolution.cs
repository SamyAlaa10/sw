using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Worker_influences.Tools
{
    public class EditResolution
    {
        static double MyScreenWidth = 1536;
        static double MtscreenHeghit = 864;
        public static double GetNewNumberForThisScreenWidth(double WidthScreen, double WidthElement)
        {
            return (WidthScreen / MyScreenWidth) * WidthElement;
        }
        public static double GetNewNumberForThisScreenHeghit(double HeghitScreen, double HeghitElement)
        {
            return (HeghitScreen / MtscreenHeghit) * HeghitElement;
        }
        public static double GetNewNumberForThisScreenFont(double HeghitScreen, double FontSize)
        {
            return (HeghitScreen / MtscreenHeghit) * FontSize;
        }
        public static Thickness GetNewNumberForThisScreenMargin(double WidthScreen, double HeghitScreen, Thickness Margin)
        {
            Thickness Margin_ = new Thickness(GetNewNumberForThisScreenWidth(WidthScreen, Margin.Left),
                GetNewNumberForThisScreenHeghit(HeghitScreen, Margin.Top)
                , GetNewNumberForThisScreenWidth(WidthScreen, Margin.Right)
                , GetNewNumberForThisScreenHeghit(HeghitScreen, Margin.Bottom));
            return Margin_;
        }


        public static void editControl(dynamic MainGred, Window window)
        {

            if (MainGred.GetType().Name == "ScrollViewer")
            {
                ScrollViewer GRD = MainGred as ScrollViewer;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);

            }
            if (MainGred.GetType().Name == "Button")
            {
                Button GRD = MainGred as Button;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
                GRD.FontSize = GetNewNumberForThisScreenFont(window.Height, GRD.FontSize);

            }
            if (MainGred.GetType().Name == "TextBox")
            {
                TextBox GRD = MainGred as TextBox;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
                GRD.FontSize = GetNewNumberForThisScreenFont(window.Height, GRD.FontSize);

            }
            if (MainGred.GetType().Name == "Label")
            {
                Label GRD = MainGred as Label;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
                GRD.FontSize = GetNewNumberForThisScreenFont(window.Height, GRD.FontSize);

            }
            if (MainGred.GetType().Name == "DataGrid")
            {
                DataGrid GRD = MainGred as DataGrid;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
                GRD.FontSize = GetNewNumberForThisScreenFont(window.Height, GRD.FontSize);

            }
            if (MainGred.GetType().Name == "Calendar")
            {
                Calendar GRD = MainGred as Calendar;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
                GRD.FontSize = GetNewNumberForThisScreenFont(window.Height, GRD.FontSize);

            }
        }

        public static void editDecorator(dynamic MainGred, Window window)
        {


            if (MainGred.GetType().Name == "Border")
            {
                Border GRD = MainGred as Border;
                GRD.Width = GetNewNumberForThisScreenWidth(window.Width, GRD.Width);
                GRD.Height = GetNewNumberForThisScreenHeghit(window.Height, GRD.Height);
                GRD.Margin = GetNewNumberForThisScreenMargin(window.Width, window.Height, GRD.Margin);
            }

        }

        public static void EditPanel(Panel MainGred, Window window)
        {

            Grid GRD = MainGred as Grid;

            for (int x = 0; x < GRD.Children.Count; x++)
            {
                if (GRD.Children[x].GetType().BaseType.Name == "TextBoxBase" ||
                    GRD.Children[x].GetType().BaseType.Name == "ButtonBase" ||
                    GRD.Children[x].GetType().BaseType.Name == "ContentControl" ||
                    GRD.Children[x].GetType().BaseType.Name == "Control" ||
                     GRD.Children[x].GetType().BaseType.Name == "MultiSelector")
                {
                    editControl(GRD.Children[x], window);
                }

                if (GRD.Children[x].GetType().BaseType.Name == "Decorator")
                {
                    editDecorator(GRD.Children[x], window);
                }
            }


        }

    }
}
