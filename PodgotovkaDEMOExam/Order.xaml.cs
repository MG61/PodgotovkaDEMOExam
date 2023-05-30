using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using PodgotovkaDEMOExam.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PodgotovkaDEMOExam
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        DataSet1 dataSet1 = new DataSet1();
        OrdersTableAdapter OTA = new OrdersTableAdapter();

        public Order()
        {
            InitializeComponent(); 
            data.ItemsSource = dataSet1.Orders.DefaultView;
            OTA.Fill(dataSet1.Orders);

            Summa();
        }
        
        private void Button_plus_order(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedItem != null)
                {
                    DataRowView selItem = (DataRowView)data.SelectedItem;
                    int id = (int)selItem["ID_order"];
                    String name_order = (String)selItem["Name"];
                    String price_order = (String)selItem["Price"];
                    String quantity_order = (String)selItem["Quantity"];
                    quantity_order = (Convert.ToInt32(quantity_order) + 1).ToString();
                    String total_price = (Convert.ToInt32(price_order) * Convert.ToInt32(quantity_order)).ToString();
                    OTA.UpdateQuery(name_order, price_order, quantity_order, total_price, id);
                    OTA.Fill(dataSet1.Orders);
                    Summa();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось изменить данные!");
            }
        }

        private void Button_minus_order(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedItem != null)
                {
                    DataRowView selItem = (DataRowView)data.SelectedItem;
                    int id = (int)selItem["ID_order"];
                    String name_order = (String)selItem["Name"];
                    String price_order = (String)selItem["Price"];
                    String quantity_order = (String)selItem["Quantity"];
                    if (Convert.ToInt32(quantity_order) > 1)
                    {
                        quantity_order = (Convert.ToInt32(quantity_order) - 1).ToString();
                        String total_price = (Convert.ToInt32(price_order) * Convert.ToInt32(quantity_order)).ToString();
                        OTA.UpdateQuery(name_order, price_order, quantity_order, total_price, id);
                        OTA.Fill(dataSet1.Orders);
                        Summa();
                    }
                    else
                    {
                        OTA.DeleteQuery(id);
                        OTA.Fill(dataSet1.Orders);
                        Summa();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось изменить данные!");
            }
        }

        private void Button_delete_order(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedItem != null)
                {
                    DataRowView selItem = (DataRowView)data.SelectedItem;
                    int id = (int)selItem["ID_order"];
                    OTA.DeleteQuery(id);
                    OTA.Fill(dataSet1.Orders);
                    Summa();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось изменить данные!");
            }
        }

        private void Button_buy_order(object sender, RoutedEventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //string fileText = "Your output text";
            //if (saveFileDialog.ShowDialog() == true)
            //    File.WriteAllText(saveFileDialog.FileName, fileText);

            //Stream stream = File.Open(saveFileDialog.FileName, FileMode.Open);
            //Stream outputStream = new MemoryStream();
            //DocumentConverter.ToDocument(stream, outputStream, FileType.DOC_PDF);
        } 
        
        public void Summa()
        {
            //Подсчёт суммы
            int sum = 0;
            foreach (DataRowView row in data.ItemsSource)
            {
                sum += Int32.Parse(row["Total"].ToString());
            }
            summ.Text = sum.ToString();
            discount.Text = ((Int32.Parse(summ.Text) / 100) * 10).ToString();
            total.Text = ((Int32.Parse(summ.Text) - Int32.Parse(discount.Text))).ToString();
        }
    }
}
