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

namespace Scin_Beauty.form
{
    /// <summary>
    /// Логика взаимодействия для product.xaml
    /// </summary>
    public partial class product : UserControl
    {
        public int id;
        public list list;
        Class.DataBase DataBase = new Class.DataBase();

        public product()
        {
            InitializeComponent();
        }

        private void editProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addService addService = new addService();
            addService.id = id;
            addService.pozition = false;
            addService.titleLabel.Content = "Продукт";
            addService.Inizializete();
            addService.Show();
            list.Close();
        }

        private void deleteProduct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (DataBase.Datas("select ProductId from ClientProduct where ProductId=" + id).Rows.Count == 0)
                {
                    if (DataBase.Edit("delete from Product where ProductsId=" + id))
                    {
                        MessageBox.Show("Удалено");
                        list.OutputList();
                    }
                }
                else
                {
                    MessageBox.Show("Удалить нельзя");
                }
            }
        }
    }
}
