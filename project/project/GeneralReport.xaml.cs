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
    /// Interaction logic for GeneralReport.xaml
    /// </summary>
    public partial class GeneralReport : Window
    {
        public GeneralReport(List<Item> items)
        {
            InitializeComponent();
            foreach (Item item in items)
            {
                itemName.Items.Add(item.ItemName);
                itemAv.Items.Add(item.AvailableQuantity);
                itemMin.Items.Add(item.MinQuantity);
            }
        }

    }
}
