using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using project.Models;


namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool saved = false;
        private string saveLocation = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new AddItem(lbItem);
            addItem.Show();
        }



        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Inventory.SaveItems();
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Inventory.ChecktoOpen(saveLocation, saved))
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "CVS Files|*.csv";
                if (open.ShowDialog() == true)
                {
                    //save location
                    saveLocation = open.FileName;
                    //read from file
                    List<Item> items = Inventory.LoadItems(saveLocation, saved);
                    //update UI
                    foreach (Item item in items)
                    {
                        lbItem.Items.Add(item);
                        Inventory.Add(item);
                    }
                    saved = true;
                    lbItem.Items.Refresh();
                }
            }
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            GeneralReport generalReport = new GeneralReport(Inventory.Items);
            generalReport.Show();
        }


        private void ButtonList_Click(object sender, RoutedEventArgs e)
        {
            ShoppingList shoppingList = new ShoppingList(Inventory.Items);
            shoppingList.Show();
        }

        public void lbItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateDelete updateDelete = new UpdateDelete((Item)lbItem.SelectedItem, lbItem);
            updateDelete.Show();
        }

    }
   


}
