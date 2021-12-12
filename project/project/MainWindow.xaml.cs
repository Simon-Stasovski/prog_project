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
using System.IO;


namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    public class Item
    {
        private string itemName;
        private int availableQuantity;
        private int minQuantity = 1;
        private string location;
        private string supplier;
        
        public Item(string itemName)
        {
            this.itemName = itemName;
        }
        public Item(string itemName, int availableQuantity, int minQuantity, string location, string supplier) : this(itemName)
        {
            this.minQuantity = minQuantity;
            this.availableQuantity = availableQuantity;
            this.location = location;
            this.supplier = supplier;
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int AvailableQuantity
        {
            get { return availableQuantity; }
            set 
            {
                if (value < 0)
                    throw new ArgumentException("The quantity of an item can not be negative.", "AvailableQuantity");
                availableQuantity = value;
            }
        }
        public int MinQuantity
        {
            get { return minQuantity; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("The quantity of an item can not be less than 1.", "MinQuantity");
                minQuantity = value;
            }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }
        private enum Categories
        {
            Pantry, Diary, Drinks, FrozenFood, FruitVegetable, Bakery, CleaningSupplies, Other
        }
        public class Inventory
        {
            private List<Item> items = new List<Item>();

            public void Add(Item item)
            {
                if (item == null)
                    throw new ArgumentNullException("Item can not be null", "item");
                items.Add(item);
            }
            public void Remove(Item item)
            {
                if (items.Count == 0)
                    return;
                items.RemoveAt(0);
            }
            public void UpdateItem(string itemName, string newName = null, int newAvailableQuantity = -1, int newMinQuantity = -1, string newLocation = null, string newSupplier = null)
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if(items[i].ItemName == itemName)
                    {
                        if (newName != null)
                            items[i].ItemName = itemName;
                        if (newAvailableQuantity != -1)
                            items[i].AvailableQuantity = newAvailableQuantity;
                        if (newMinQuantity != -1)
                            items[i].MinQuantity = newMinQuantity;
                        if (newLocation != null)
                            items[i].Location = newLocation;
                        if (newSupplier != null)
                            items[i].Supplier = newSupplier;
                        return;
                    }
                }
            }
            public string GeneralReport()
            {
                StringBuilder report = new StringBuilder();

                foreach(Item item in items)
                {
                    report.AppendLine($"{item.ItemName}: Available Quantity: {item.AvailableQuantity} Min Quantity: {item.MinQuantity}");
                }
                return report.ToString();
            }
            public string ShoppingList()
            {
                StringBuilder shoppingList = new StringBuilder();

                foreach (Item item in items)
                {
                    if (item.AvailableQuantity < item.MinQuantity)
                        shoppingList.AppendLine($"{item.ItemName} has available quantity of {item.AvailableQuantity} and minimum quatity of {item.MinQuantity}.");
                }
                return shoppingList.ToString();
            }
            public void LoadItems(string filePath)
            {

                if (File.Exists(filePath))
                {
                    try
                    {
                        string[] allLines = File.ReadAllLines(filePath);

                        for (int i = 0; i < allLines.Length; i += 2)
                        {
                            string[] splitLine = allLines[i].Split(',');
                            items.Add(new Item(splitLine[0], int.Parse(splitLine[1]), int.Parse(splitLine[2]), splitLine[3], splitLine[4]));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    } 
}
