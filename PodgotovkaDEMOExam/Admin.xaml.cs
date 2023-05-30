using Microsoft.Win32;
using PodgotovkaDEMOExam.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace PodgotovkaDEMOExam
{

    public partial class Admin : Window
    {

        DataSet1 dataSet1 = new DataSet1();
        ProductTableAdapter PTA = new ProductTableAdapter();
        OrdersTableAdapter OTA = new OrdersTableAdapter();
        String global_photo = "";
        public Admin()
        {
            InitializeComponent();

            data.ItemsSource = dataSet1.Product.DefaultView;
            PTA.Fill(dataSet1.Product);
            OTA.Fill(dataSet1.Orders);
        }

        private void Button_add_product(object sender, RoutedEventArgs e)
        {
            try { 
            PTA.InsertQuery(name_product.Text, price_product.Text, quantity_product.Text, "Image/" + global_photo);
            PTA.Fill(dataSet1.Product);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось добавить данные!");
            }
        }

        private void Button_change_product(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedItem != null)
                {
                    DataRowView selItem = (DataRowView)data.SelectedItem;
                    int id = (int)selItem["ID_product"];
                    PTA.UpdateQuery(name_product.Text, price_product.Text, quantity_product.Text,"Image/" + global_photo, id);
                    PTA.Fill(dataSet1.Product);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось изменить данные!");
            }
        }

        private void Button_delete_product(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedItem != null) 
                { 
                DataRowView selItem = (DataRowView)data.SelectedItem;
                int id = (int)selItem["ID_product"];
                PTA.DeleteQuery(id);
                PTA.Fill(dataSet1.Product);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить данные!");
            }
        }


        private void Button_back(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow go = new MainWindow();
                go.Show();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось удалить данные!");
            }
        }

        private void Button_add_order(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selItem = (DataRowView)data.SelectedItem;
                String name_order = (String)selItem["Name"];
                String price_order = (String)selItem["Price"];
                OTA.InsertQuery(name_order, price_order, "1", price_order);
                OTA.Fill(dataSet1.Orders);
                Button_order.Visibility= Visibility.Visible;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось добавить данные!");
            }
        }

        private void Button_order_show(object sender, RoutedEventArgs e)
        {
            Order go = new Order();
            go.ShowDialog();
        }

        
        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            string fileName = "";
            string name = "";
            if (fileDialog.ShowDialog() == true)
            {
                fileName = fileDialog.FileName;
                name = fileDialog.SafeFileName;
                global_photo = name;
            }

            string projectPath = Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\net6.0-windows", "");
            string toPath = $"{projectPath}\\Image\\{name}";

            try
            {
                File.Copy(fileName, toPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
