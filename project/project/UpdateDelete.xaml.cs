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
    /// Interaction logic for UpdateDelete.xaml
    /// </summary>
    public partial class UpdateDelete : Window
    {
        private Item item = null;
        private ListBox lbItem = null;
        public UpdateDelete(Item item, ListBox lbItem)
        {

            InitializeComponent();

            this.item = item;
            this.lbItem = lbItem;
            DisplayItemInfo();
        }
        private void DisplayItemInfo()
        {
            itemName.Text = this.item.ItemName;
            minQuantity.Text = this.item.MinQuantity.ToString();
            availableQuantity.Text = this.item.AvailableQuantity.ToString();
            locationName.Text = this.item.Location;

            foreach ( ComboBoxItem cb in supplierName.Items)
            {
                if (cb.Content.ToString() == this.item.Supplier)
                    supplierName.SelectedValue = cb;
            }
            foreach (ComboBoxItem cb in categoryName.Items)
            {
                if (cb.Content.ToString() == this.item.Category)
                    categoryName.SelectedItem = cb;
            }
        }
        private void ButtonRem_Click(object sender, RoutedEventArgs e)
        {
            lbItem.Items.Remove(item);
            Inventory.Remove(item);
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {

            Inventory.UpdateItem(item, itemName.Text, int.Parse(availableQuantity.Text), 
                int.Parse(minQuantity.Text), locationName.Text, supplierName.Text, categoryName.Text);
            lbItem.Items.Refresh();
            
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUserInput())
            {

                Inventory.Add(GetItemObject());
                lbItem.Items.Add(GetItemObject());
            }
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


    }
}
