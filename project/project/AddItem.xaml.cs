using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using project.Models;

namespace project
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        private ListBox lbItem = null;       
        public AddItem(ListBox lbItem)
        {
            InitializeComponent();
            this.lbItem = lbItem;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserInput())
            {

                Inventory.Add(GetItemObject());
                lbItem.Items.Add(GetItemObject());
            }
        }
        private bool CheckUserInput()
        {
            StringBuilder msg = new StringBuilder();

            if (string.IsNullOrEmpty(itemName.Text))
                msg.AppendLine("Item Name is a required field");

            if (minQuantity.Text == null)
                msg.AppendLine("Min Quantity is a required field");

            if (availableQuantity == null)
                msg.AppendLine("Available Quantity is a required field");

            if (string.IsNullOrEmpty(locationName.Text))
                msg.AppendLine("Location is a required field");

            if (supplierName.SelectedIndex == -1)
                msg.AppendLine("Supplier Name is a required field");

            if (categoryName.SelectedIndex == -1)
                msg.AppendLine("Category is a required field");


            if (string.IsNullOrEmpty(msg.ToString()))
                return true;

            MessageBox.Show(msg.ToString(), "Required User Input", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        private Item GetItemObject()
        {
            return new Item()
            {
                ItemName = itemName.Text,
                MinQuantity = int.Parse(minQuantity.Text),
                AvailableQuantity = int.Parse(availableQuantity.Text),
                Location = locationName.Text,
                Supplier = supplierName.Text,
                Category = categoryName.Text
            };
        }
    }
}
