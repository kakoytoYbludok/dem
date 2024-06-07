using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ServiceTechno
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TableZayavkaFill()
        {
            try
            {

                Action action = () =>
                {
                    Class1 class1 = new Class1();

                    class1.sqlExecute("select [ID_Zayavka], [Number], [Date], [Problem], [Device_ID], [Type_ID], [Client_ID], [Status_ID] from [dbo].[Zayavka]", Class1.act.select);

                    class1.dependency.OnChange += DependancyOnChange_ManStr;

                    dgZayavka.ItemsSource = class1.resultTable.DefaultView;
                    dgZayavka.Columns[0].Visibility = Visibility.Hidden;
                    dgZayavka.Columns[1].Header = "Номер";
                    dgZayavka.Columns[2].Header = "Дата";
                    dgZayavka.Columns[3].Header = "Проблема";
                    dgZayavka.Columns[4].Header = "Девайс";
                    dgZayavka.Columns[5].Header = "Тип";
                    dgZayavka.Columns[6].Header = "Клиент";
                    dgZayavka.Columns[7].Header = "Статус";
                };
                Dispatcher.Invoke(action);
            }
            catch { };
        }
        private void DependancyOnChange_ManStr(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
            {
                TableZayavkaFill();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView dataRowView = dgZayavka.SelectedItems[0] as DataRowView;
                tbNum.Text = dataRowView[1].ToString();
                tbDate.Text = dataRowView[2].ToString();
                tbProb.Text = dataRowView[3].ToString();
                tbDev.Text = dataRowView[4].ToString();
                tbType.Text = dataRowView[5].ToString();
                tbClient.Text = dataRowView[6].ToString();
                tbStatus.Text = dataRowView[7].ToString();

            }
            catch { }
        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            TableZayavkaFill();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class1 class1 = new Class1();
            class1.sqlExecute(string.Format("insert into [dbo].[Zayavka] ([Number], [Date], [Problem], [Device_ID], [Type_ID], [Client_ID], [Status_ID])" +
                "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", tbNum.Text, tbDate.Text, tbProb.Text, tbDev.Text, tbType.Text, tbClient.Text, tbStatus.Text), Class1.act.manipulation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Class1 dataBaseClass = new Class1();
            DataRowView dataRowView = dgZayavka.SelectedItems[0] as DataRowView;
            dataBaseClass.sqlExecute(String.Format("update [dbo].[Uslugi] set " +
                "[Number] = '{0}'," +
                "[Date] = '{1}'," +
                "[Problem] = '{2}'," +
                "[Device_ID] = '{3}'" +
                "[Type_ID] = '{4}'" +
                "[Client_ID] = '{5}'" +
                "[Status_ID] = '{6}'",
                  tbNum.Text, tbDate.Text, tbProb.Text, tbDev.Text, tbType.Text, tbClient.Text, tbStatus.Text, dataRowView[0]), Class1.act.manipulation);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dgZayavka.Items.Count != 0 & dgZayavka.SelectedItems.Count != 0)
            {
                DataRowView dataRowView = (DataRowView)dgZayavka.SelectedItems[0];
                Class1 class1 = new Class1();
                class1.sqlExecute(string.Format("delete from [dbo].[Zayavka] where [ID_Zayavka] = {0}", dataRowView[0]), Class1.act.manipulation);
            }
        }
    }
}
