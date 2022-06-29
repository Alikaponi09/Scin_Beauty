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
using System.Windows.Shapes;

namespace Scin_Beauty.form
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Window
    {
        public home home;
        public login()
        {
            InitializeComponent();
        }

        private void loginLabel_Click(object sender, RoutedEventArgs e)
        {
            if (password.Text == "4321")
            {
                home.admin = true;
                home.AdminButton();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Неверный пароль");
            }
        }
    }
}
