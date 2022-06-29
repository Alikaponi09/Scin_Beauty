using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для buyProduct.xaml
    /// </summary>
    public partial class buyProduct : Window
    {
        public bool pozition;
        int x;
        int statusComboId = -1;
        int productComboId = -1;
        int clientComboId = -1;
        int countProductInt = int.MaxValue;
        Class.DataBase DataBase = new Class.DataBase();
        DataTable dataTable;

        public buyProduct()
        {
            InitializeComponent();
            Label label1 = new Label();
            label1.Name = "q" + 0;
            label1.Content = "Новый";
            statusClient.Items.Add(label1);

            Label label2 = new Label();
            label2.Name = "q" + 1;
            label2.Content = "Уже существующий";
            statusClient.Items.Add(label2);
            ComboBoxValue();
        }

        public void ComboBoxValue() 
        {
            dataTable = DataBase.Datas("SELECT[ProductsId],[Name],[Price],[DiscriptionProducts],[ImageProducts],[Count] FROM[dbo].[Product]");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Label l = new Label();
                l.Name = "q" + dataTable.Rows[i][0].ToString();
                l.Content = dataTable.Rows[i][1].ToString();
                nameBuyProduct.Items.Add(l);
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

        private void exitHime_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list list = new list();
            list.admin = true;
            list.databaseTable = pozition;
            list.OutputList();
            list.Show();
            this.Close();
        }

        private void statusClientProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            statusComboId = Convert.ToInt32(((Label)statusClient.SelectedItem).Name.Remove(0, 1));
            ComboBoxValue();
            Client();
        }

        private void nameBuyProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productComboId = Convert.ToInt32(((Label)nameBuyProduct.SelectedItem).Name.Remove(0, 1));
            countProductInt = Convert.ToInt32(DataBase.Datas("SELECT [Count] FROM [dbo].[Product] where ProductsId = " + productComboId).Rows[0][0]);
        }

        private void saveRecord_Click_1(object sender, RoutedEventArgs e)
        {
            if (statusComboId == 1)
            {
                if (clientComboId != -1 && productComboId != -1 && int.TryParse(countProduct.Text, out x))
                {
                    if (countProductInt > Convert.ToInt32(countProduct.Text))
                    {
                        if (DataBase.Edit("INSERT INTO [dbo].[ClientProduct] ([ClientID],[ProductId],[Amount]) " +
                        "VALUES (" + clientComboId + "," + productComboId + "," + countProduct.Text + ")"))
                        {
                            MessageBox.Show("Товар продан");
                            if (DataBase.Edit("UPDATE [dbo].[Product] SET [Count] = "+(countProductInt - Convert.ToInt32(countProduct.Text))+" WHERE ProductsId = "+productComboId))
                            {

                            }
                            else
                            {
                                MessageBox.Show("Произошла ошибка при обновление товара");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Количество товара превышено");
                    }
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля заполнены или неправильный формат полей");
                }
            }
            else
            {
                if (productComboId != -1
                    && FirstNameClient.Text.Length > 0
                    && MiddleNameClient.Text.Length > 0
                    && LastNameClient.Text.Length > 0
                    && (PhoneNumberClient.Text.Length > 0
                    || EmailClient.Text.Length > 0)
                    && int.TryParse(countProduct.Text, out x))
                {
                    if (DataBase.Edit("INSERT INTO [dbo].[Client] ([FirstName],[MiddleName],[LastName],[PhoneNumber],[Email])" +
                                      " VALUES (" +
                                      "'" + FirstNameClient.Text + "'," +
                                      "'" + MiddleNameClient.Text + "'," +
                                      "'" + LastNameClient.Text + "'," +
                                      "'" + PhoneNumberClient.Text + "'," +
                                      "'" + EmailClient.Text + "')"))
                    {
                        if (countProductInt > Convert.ToInt32(countProduct.Text))
                        {
                            if (DataBase.Edit("INSERT INTO [dbo].[ClientProduct] ([ClientID],[ProductId],[Amount]) " +
                                               "VALUES (" + DataBase.Datas("select Max(ClientId) from Client").Rows[0][0] + "," + productComboId + "," + countProduct.Text + ")"))
                            {
                                MessageBox.Show("Товар продан");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Количество товара превышено");
                        }
                    }
                    else
                        MessageBox.Show("Произошла ошибка");
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля заполнены или неправильный формат полей");
                }
            }
        }

        private void nameClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientComboId = Convert.ToInt32(((Label)nameClient.SelectedItem).Name.Remove(0, 1));
        }

        private void countProduct_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"[0-9]");
            bool b = !rg.IsMatch(e.Text);
            e.Handled = b;
        }

        private void PhoneNumberClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"[0-9]");
            bool b = !rg.IsMatch(e.Text);
            e.Handled = b;
        }
    }
}
