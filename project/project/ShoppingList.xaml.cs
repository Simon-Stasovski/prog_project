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
    /// Interaction logic for ShoppingList.xaml
    /// </summary>
    public partial class ShoppingList : Window
    {
        public ShoppingList(List<Item> items)
        {
            InitializeComponent();
            foreach (Item item in items)
            {
                if (item.AvailableQuantity < item.MinQuantity)
                    itemsList.Items.Add($"{item.ItemName} has available quantity of {item.AvailableQuantity} and minimum quatity of {item.MinQuantity}.");
            }
        }
    }
}
