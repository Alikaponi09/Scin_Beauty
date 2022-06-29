using System;
using System.Collections.Generic;
using System.Data;
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

namespace Scin_Beauty.form
{
    /// <summary>
    /// Логика взаимодействия для recordClient.xaml
    /// </summary>
    public partial class recordClient : Window
    {
        public bool pozition;
        int statusComboId = -1;
        int serviceComboId = -1;
        int clientComboId = -1;
        DataTable dataTable;
        Class.DataBase DataBase = new Class.DataBase();

        public recordClient()
        {
            InitializeComponent();
            date.DataContext = DateTime.Now;
            Label label1 = new Label();
            label1.Name = "q" + 0;
            label1.Content = "Новый";
            statusClient.Items.Add(label1);

            Label label2 = new Label();
            label2.Name = "q" + 1;
            label2.Content = "Уже существующий";
            statusClient.Items.Add(label2);

            dataTable = DataBase.Datas("SELECT [ServiceId],[Name],[Price],[DurationService],[DiscriptionService],[ImageService] FROM [dbo].[Service]");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Label l = new Label();
                l.Name = "q" + dataTable.Rows[i][0].ToString();
                l.Content = dataTable.Rows[i][1].ToString();
                nameServiceRecord.Items.Add(l);
            }
            dataTable = DataBase.Datas("SELECT [ClientId],[FirstName],[MiddleName],[LastName] FROM[dbo].[Client]");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Label l = new Label();
                l.Name = "q" + dataTable.Rows[i][0].ToString();
                l.Content = dataTable.Rows[i][1] + " " + dataTable.Rows[i][2] + " " + dataTable.Rows[i][3];
                nameClient.Items.Add(l);
            }
        }

        public void Client()
        {
            if (statusComboId == 1)
            {
                FIO.Visibility = Visibility.Visible;
                newClient.Visibility = Visibility.Collapsed;
            }
            else
            {
                FIO.Visibility = Visibility.Collapsed;
                newClient.Visibility = Visibility.Visible;
            }
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list list = new list();
            list.admin = true;
            list.databaseTable = pozition;
            list.OutputList();
            list.Show();
            this.Close();
        }

        private void statusClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statusComboId = Convert.ToInt32(((Label)statusClient.SelectedItem).Name.Remove(0, 1));
            Client();
        }

        private void saveRecord_Click(object sender, RoutedEventArgs e)
        {
            if (statusComboId == 1)
            {
                if (clientComboId != -1 && serviceComboId != -1 && date.Value >= DateTime.Now)
                {
                    if (DataBase.Edit("INSERT INTO [dbo].[ClientSevice] ([ClientID],[ServiceID],[DataTimeService]) " +
                        "VALUES (" + clientComboId + "," + serviceComboId + ",'" + Convert.ToDateTime(date.Value) + "')"))
                    {
                        MessageBox.Show("Клиент записан");
                    }
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля заполнены");
                }
            }
            else
            {
                if (serviceComboId != -1
                    && date.Value >= DateTime.Now
                    && FirstNameClient.Text.Length > 0 
                    && MiddleNameClient.Text.Length > 0
                    && LastNameClient.Text.Length > 0
                    && (PhoneNumberClient.Text.Length == 11
                    || EmailClient.Text.Length > 0))
                {
                    if (DataBase.Edit("INSERT INTO [dbo].[Client] ([FirstName],[MiddleName],[LastName],[PhoneNumber],[Email])" +
                                      " VALUES (" +
                                      "'" + FirstNameClient.Text + "'," +
                                      "'" + MiddleNameClient.Text + "'," +
                                      "'" + LastNameClient.Text + "'," +
                                      "'" + PhoneNumberClient.Text + "'," +
                                      "'" + EmailClient.Text + "')"))
                    {
                        if (DataBase.Edit("INSERT INTO [dbo].[ClientSevice] ([ClientID],[ServiceID],[DataTimeService]) " +
                        "VALUES (" + DataBase.Datas("select Max(ClientId) from Client").Rows[0][0] + "," + serviceComboId + ",'" + Convert.ToDateTime(date.Value) + "')"))
                        {
                            MessageBox.Show("Клиент записан");
                        }
                    }
                    else
                        MessageBox.Show("Произошла ошибка");
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля заполнены");
                }
            }
        }

        private void nameServiceRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            serviceComboId = Convert.ToInt32(((Label)nameServiceRecord.SelectedItem).Name.Remove(0, 1));
        }

        private void nameClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientComboId = Convert.ToInt32(((Label)nameClient.SelectedItem).Name.Remove(0, 1));
        }
    }
}
