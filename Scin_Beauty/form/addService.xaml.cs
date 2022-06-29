using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для addService.xaml
    /// </summary>
    public partial class addService : Window
    {
        public int id = -1;
        public bool pozition;
        Class.DataBase DataBase = new Class.DataBase();

        public addService()
        {
            InitializeComponent();
            Inizializete();
        }

        public void Inizializete()
        {
            DataTable dataTable;
            if (pozition)
            {
                timeDockPanel.Visibility = Visibility.Visible;
                CountDockPanel.Visibility = Visibility.Collapsed;
                dataTable = DataBase.Datas("select ServiceId,Name,Price,DurationService,DiscriptionService,ImageService from Service where ServiceId=" + id);
                if (dataTable.Rows.Count > 0)
                {
                    nameProduct.Text = dataTable.Rows[0][1].ToString();
                    priceProdict.Text = Convert.ToDouble(dataTable.Rows[0][2].ToString()).ToString();
                    timeService.Text = dataTable.Rows[0][3].ToString();
                    description.Text = dataTable.Rows[0][4].ToString();
                    image.Content = dataTable.Rows[0][5].ToString();
                }
            }
            else
            {
                timeDockPanel.Visibility = Visibility.Collapsed;
                CountDockPanel.Visibility = Visibility.Visible;
                dataTable = DataBase.Datas("select ProductsId,Name,Price,DiscriptionProducts,ImageProducts,Count from Product where ProductsId=" + id);
                if (dataTable.Rows.Count > 0)
                {
                    nameProduct.Text = dataTable.Rows[0][1].ToString();
                    priceProdict.Text = Convert.ToDouble(dataTable.Rows[0][2].ToString()).ToString();
                    description.Text = dataTable.Rows[0][3].ToString();
                    image.Content = dataTable.Rows[0][4].ToString();
                    CountTextBox.Text = dataTable.Rows[0][5].ToString();
                }
            }
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (id == -1)
            {
                home home = new home();
                home.admin = true;
                home.AdminButton();
                home.Show();
                this.Close();
            }
            else
            {
                list listt = new list();
                listt.databaseTable = pozition;
                listt.admin = true;
                listt.OutputList();
                listt.Show();
                this.Close();
            }
        }

        public bool Check(int value)
        {
            if (Convert.ToInt32(value) >= 0)
            {
                return true;
            }
            return false;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            int x;
            if (int.TryParse(priceProdict.Text, out x) && nameProduct.Text.Length != 0)
            {
                if (Check(Convert.ToInt32(priceProdict.Text)))
                {
                    if (id == -1)
                    {
                        if (pozition)
                        {
                            if (int.TryParse(timeService.Text, out x))
                            {
                                if (Check(Convert.ToInt32(timeService.Text)))
                                {
                                    if (DataBase.Edit("INSERT INTO [dbo].[Service]([Name],[Price],[DurationService],[DiscriptionService],[ImageService]) " +
                                                       "VALUES('" + nameProduct.Text + "', " +
                                                       Convert.ToInt32(priceProdict.Text) + ", " +
                                                       Convert.ToInt32(timeService.Text) + ", " +
                                                       "'" + description.Text + "', " +
                                                       "'" + image.Content + "')"))
                                    {
                                        list listt = new list();
                                        listt.databaseTable = pozition;
                                        listt.admin = true;
                                        listt.OutputList();
                                        listt.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Произошла ошибка при вставке сервиса");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Время указано не правильно\nДолжно быть больше 0");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не указано время");
                            }
                        }
                        else
                        {
                            if (int.TryParse(CountTextBox.Text, out x))
                            {
                                if (Check(Convert.ToInt32(CountTextBox.Text)))
                                {
                                    if (DataBase.Edit("INSERT INTO [dbo].[Product]([Name],[Price],[DiscriptionProducts],[ImageProducts],[Count]) " +
                                                 "VALUES('" + nameProduct.Text + "', " +
                                                 Convert.ToInt32(priceProdict.Text) + ", " +
                                                 "'" + description.Text + "', " +
                                                 "'" + image.Content + "', " + CountTextBox.Text + ")"))
                                    {
                                        list listt = new list();
                                        listt.databaseTable = pozition;
                                        listt.admin = true;
                                        listt.OutputList();
                                        listt.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Произошла ошибка при вставке продукта");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неправильный формат количества");
                            }
                        }
                    }
                    else
                    {
                        if (pozition)
                        {
                            if (int.TryParse(timeService.Text, out x))
                            {
                                if (Check(Convert.ToInt32(timeService.Text)))
                                {
                                    if (DataBase.Edit("update [dbo].[Service] set " +
                                                           "[Name] = '" + nameProduct.Text + "', " +
                                                           "[Price] = " + Convert.ToInt32(priceProdict.Text) + ", " +
                                                           "[DiscriptionService] = '" + description.Text + "', " +
                                                           "[ImageService] = '" + image.Content + "' " +
                                                           "where ServiceId=" + id))
                                    {
                                        list listt = new list();
                                        listt.databaseTable = pozition;
                                        listt.admin = true;
                                        listt.OutputList();
                                        listt.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Произошла ошибка при обновление сервиса");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Время указано не правильно\nДолжно быть больше 0");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не указано время");
                            }
                        }
                        else
                        {
                            if (int.TryParse(CountTextBox.Text, out x))
                            {
                                if (Check(Convert.ToInt32(CountTextBox.Text)))
                                {
                                    if (DataBase.Edit("update [dbo].[Product] set " +
                                                       "[Name] = '" + nameProduct.Text + "', " +
                                                       "[Price] = " + Convert.ToInt32(priceProdict.Text) + ", " +
                                                       "[DiscriptionProducts] = '" + description.Text + "', " +
                                                       "[ImageProducts] = '" + image.Content + "', " +
                                                       "[Count] = '" + CountTextBox.Text + "' " +
                                                       "where ProductsId=" + id))
                                    {
                                        list listt = new list();
                                        listt.databaseTable = pozition;
                                        listt.admin = true;
                                        listt.OutputList();
                                        listt.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Произошла ошибка при обновление продукта");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неправильный формат количества");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Числовые значения должны быть больше 0!");
                }
            }
            else
            {
                MessageBox.Show("Неправильный формат значиний в полях \nИли обязательные поля пустые!");
            }
        }

        public string FilePath { get; set; }
        public string FullFilePath { get; set; }
        public string DestinationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public bool OpenFileDialog() 
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.SafeFileName;
                FullFilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        private void image_Click(object sender, RoutedEventArgs e)
        {
            string qq;
            if (pozition)
            {
                qq = @"imageService\";
            }
            else
            {
                qq = @"imageProduct\";
            }

            if (OpenFileDialog())
            {
                FileInfo fileInfo = new FileInfo(FullFilePath);
                image.Content = qq + FilePath;
                fileInfo.CopyTo(System.IO.Path.Combine(DestinationPath, FilePath), true);
            }
        }

        private void timeService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"[0-9]");
            bool b = !rg.IsMatch(e.Text);
            e.Handled = b;
        }

        private void CountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"[0-9]");
            bool b = !rg.IsMatch(e.Text);
            e.Handled = b;
        }

        private void priceProdict_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"[0-9]");
            bool b = !rg.IsMatch(e.Text);
            e.Handled = b;
        }
    }
}