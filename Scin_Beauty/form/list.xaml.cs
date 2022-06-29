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
    /// Логика взаимодействия для list.xaml
    /// </summary>
    public partial class list : Window
    {
        Class.DataBase dataBase = new Class.DataBase();
        public bool databaseTable;
        public bool admin;
        int priceComboId = -1;
        string like = " where Name like '%%' ";

        public list()
        {
            InitializeComponent();
            Label label1 = new Label();
            label1.Name = "q" + 0;
            label1.Content = "по возростанию";
            priceCombo.Items.Add(label1);

            label1 = new Label();
            label1.Name = "q" + 1;
            label1.Content = "по убыванию";
            priceCombo.Items.Add(label1);
            OutputList();
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            home home = new home();
            home.admin = admin;
            home.AdminButton();
            home.Show();
            this.Close();
        }

        private void add_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (databaseTable)
            {
                recordClient recordClient = new recordClient();
                recordClient.pozition = databaseTable;
                recordClient.Show();
                this.Close();
            }
            else
            {
                buyProduct buyProduct = new buyProduct();
                buyProduct.pozition = databaseTable;
                buyProduct.Show();
                this.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            like = " where Name like '%" + likeTextBox.Text + "%' ";
            OutputList();
        }

        public void OutputList()
        {
            string qyure;
            if (databaseTable)
            {
                qyure = "select ServiceId,Name,Price,DurationService,DiscriptionService,ImageService from Service " + like;
            }
            else
            {
                qyure = "select ProductsId,Name,Price,DiscriptionProducts,ImageProducts from Product " + like;
            }

            if (priceComboId == 0)
            {
                qyure += " order by Price";
            }
            else if (priceComboId == 1)
            {
                qyure += " order by Price desc";
            }

            listStack.Children.Clear();
            DataTable res = dataBase.Datas(qyure);

            for (int i = 0; i < res.Rows.Count; i++)
            {
                if (!databaseTable)
                {
                    product product = new product();
                    product.id = Convert.ToInt32(res.Rows[i][0]);
                    product.nameProduct.Content = res.Rows[i][1];
                    product.priceProduct.Content = "Прайс: " + res.Rows[i][2];
                    product.descriptionProduct.Text = res.Rows[i][3].ToString();
                    product.list = this;
                    product.imageProduct.Source = new BitmapImage(new Uri("\\" + res.Rows[i][4].ToString(), UriKind.Relative));
                    if (!admin)
                    {
                        product.editProduct.Visibility = Visibility.Hidden;
                        product.deleteProduct.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        product.editProduct.Visibility = Visibility.Visible;
                        product.deleteProduct.Visibility = Visibility.Visible;
                    }
                    listStack.Children.Add(product);
                }
                else
                {
                    service service = new service();
                    service.id = Convert.ToInt32(res.Rows[i][0]);
                    service.nameService.Content = res.Rows[i][1];
                    service.priceService.Content = "Прайс: " + res.Rows[i][2];
                    service.timeService.Content = "Длительность: " + res.Rows[i][3] + " минут";
                    service.descriptionService.Text = res.Rows[i][4].ToString();
                    service.list = this;
                    service.imageServise.Source = new BitmapImage(new Uri("\\" + res.Rows[i][5].ToString(), UriKind.Relative));
                    listStack.Children.Add(service);
                    if (!admin)
                    {
                        service.editService.Visibility = Visibility.Collapsed;
                        service.deleteService.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        service.editService.Visibility = Visibility.Visible;
                        service.deleteService.Visibility = Visibility.Visible;
                    }
                }
               
                if (!admin)
                {
                    add.Visibility = Visibility.Collapsed;
                    add.Visibility = Visibility.Collapsed;
                }
                else
                {
                    add.Visibility = Visibility.Visible;
                    add.Visibility = Visibility.Visible;
                }
            }
        }

        private void priceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            priceComboId = Convert.ToInt32(((Label)priceCombo.SelectedItem).Name.Remove(0, 1));
            OutputList();
        }
    }
}
