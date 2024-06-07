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

namespace ServiceTechno
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && tbPass.Text != "")
            {
                Class1 dataBaseClass = new Class1();



                if (dataBaseClass.loginAdmin(tbLogin.Text, tbPass.Text))
                {
                    Window1 window1 = new Window1();
                    //vhod.ShowDialog();
                    window1.Show();
                    this.Close();
                }
                else
                {
                }

                
            }
            else
            {
            }

            if (tbLogin.Text != "" && tbPass.Text != "")
            {
                Class1 dataBaseClass = new Class1();



                if (dataBaseClass.loginuser(tbLogin.Text, tbPass.Text))
                {
                    Window2 window2 = new Window2();
                    //vhod.ShowDialog();
                    window2.Show();
                    this.Close();
                }
                else
                {
                }

                
            }
            else
            {
            }
        }


    }
}
