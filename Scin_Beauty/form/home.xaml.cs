using Scin_Beauty.Class;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Scin_Beauty.form
{
    /// <summary>
    /// Логика взаимодействия для home.xaml
    /// </summary>
    public partial class home : Window
    {
        public bool admin = false;
        public home()
        {
            InitializeComponent();
            AdminButton();
        }

        public void AdminButton()
        {
            if (admin)
            {
                loginImage.Visibility = Visibility.Collapsed;
                exitImage.Visibility = Visibility.Visible;
            }
            else
            {
                exitImage.Visibility = Visibility.Collapsed;
                loginImage.Visibility = Visibility.Visible;
            }
        }

        private void listServiceLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list list = new list();
            list.databaseTable = true;
            list.admin = admin;
            list.OutputList();
            list.Show();
            this.Close();
        }

        private void listProductLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            list list = new list();
            list.databaseTable = false;
            list.admin = admin;
            list.OutputList();
            list.Show();
            this.Close();
        }

        private void addProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (admin)
            {
                addService addService = new addService();
                addService.pozition = false;
                addService.titleLabel.Content = "Продукт";
                addService.Inizializete();
                addService.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Для начала авторизуйтесь");
            }

        }

        private void addServiceLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (admin)
            {
                addService addService = new addService();
                addService.pozition = true;
                addService.titleLabel.Content = "Услуга";
                addService.Inizializete();
                addService.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Для начала авторизуйтесь");
            }
        }

        private void loginImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool windowObject = false;
            foreach (Window window in App.Current.Windows)
            {
                if (window.Title == "Авторизация")
                {
                    windowObject = true;
                }
            }
            if (!windowObject)
            {
                login login = new login();
                login.home = this;
                login.Show();
            }
        }

        private void exitImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            admin = false;
            AdminButton();
        }

        private void OtthetLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (admin)
            {
                DataBase dataBase = new DataBase();
                Export export = new Export();
                DataTable dataTable = dataBase.Datas("select [Name],[Price],[DurationService] from Service");
                export.ExportToWord(dataTable);
                export.ExportToWord(dataBase.Datas("select [Name],[Price],[Count] from Product"));
                export.ExportToWord(dataBase.Datas("select [FirstName],[MiddleName],[LastName], sum(Amount) as [Количество товара], count(ServiceId) as [Количество услуг] from Client c" +
                    " inner join  ClientProduct cp on cp.ClientId = c.ClientId" +
                    " inner join  ClientSevice cs on cs.ClientId = c.ClientId" +
                    " group by[FirstName],[MiddleName],[LastName]"));
            }

        }
    }
}