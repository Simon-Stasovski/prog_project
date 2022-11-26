using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace project.Models
{
    public static class Inventory
    {
        private static List<Item> items = new List<Item>();

        public static void Add(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("Item can not be null", "item");
            items.Add(item);
        }
        public static void Remove(Item item)
        {
            if (items.Count == 0)
                return;
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i])
                {
                    items.RemoveAt(i);
                }
            }

        }
        //Update the fields in an item or if the user does not provide a change, it keeps it at the same value
        public static void UpdateItem(Item item, string newName = null, int newAvailableQuantity = -1, int newMinQuantity = -1, string newLocation = null, string newSupplier = null, string newCategory = null)
        {
            if (newName != null)
                item.ItemName = newName;
            if (newAvailableQuantity != -1)
                item.AvailableQuantity = newAvailableQuantity;
            if (newMinQuantity != -1)
                item.MinQuantity = newMinQuantity;
            if (newLocation != null)
                item.Location = newLocation;
            if (newSupplier != null)
                item.Supplier = newSupplier;
            if (newCategory != null)
                item.Category = newCategory;
        }
        //Provides a formatted string to display in the general report window
        public static string GeneralReport()
        {
            StringBuilder report = new StringBuilder();
            foreach (Item item in items)
                report.AppendLine($"{item.ItemName}: Available Quantity: {item.AvailableQuantity} Min Quantity: {item.MinQuantity}");

            return report.ToString();
        }
        //Provides a formatted string to display in the shopping list window
        public static string ShoppingList()
        {
            StringBuilder shoppingList = new StringBuilder();

            foreach (Item item in items)
            {
                if (item.AvailableQuantity < item.MinQuantity)
                    shoppingList.AppendLine($"{item.ItemName} has available quantity of {item.AvailableQuantity} and minimum quatity of {item.MinQuantity}.");
            }
            return shoppingList.ToString();
        }
        //Allows for the user to load items from a .csv file. It checks for an exception and 
        //returns the items in a list of items
        public static List<Item> LoadItems(string saveLocation, bool saved)
        {
            List<Item> displayedItems = new List<Item>();

            if (File.Exists(saveLocation))
            {
                try
                {
                    string[] allLines = File.ReadAllLines(saveLocation);

                    for (int i = 0; i < allLines.Length; i++)
                    {
                        string[] splitLine = allLines[i].Split(',');
                        displayedItems.Add(new Item(splitLine[0], int.Parse(splitLine[1]), int.Parse(splitLine[2]), splitLine[3], splitLine[4], splitLine[5]));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return displayedItems;
        }
        public static List<Item> Items
        {
            get { return items; }
        }
        public static bool ChecktoOpen(string saveLocation, bool saved)
        {
            if (saved)
                return true;

            if (string.IsNullOrEmpty(saveLocation) && items.Count == 0)
                return true;


            //Data is not saved
            MessageBoxResult result =
            MessageBox.Show("Do you want to save changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);


            if (result == MessageBoxResult.No)
                return true;

            if (result == MessageBoxResult.Cancel)
                return false;

            if (result == MessageBoxResult.Yes)
                Inventory.SaveItems();

            return saved; //if the user saved it mean it ok to open a file and if the user cancels then saved will be false

        }
        public static void SaveItems()
        {
            bool saved = false;
            string saveLocation = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(saveLocation))
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "CVS Files|*.csv";
                    if (save.ShowDialog() == true)
                    {
                        saveLocation = save.FileName;
                    }
                    else
                        return;
                }
                if (!saved)
                {
                    try
                    {
                        StringBuilder itemCVSFile = new StringBuilder();
                        foreach (Item item in Inventory.items)
                            itemCVSFile.AppendLine($"{item.ItemName},{item.MinQuantity},{item.AvailableQuantity},{item.Location},{item.Supplier},{item.Category}");

                        File.WriteAllText(saveLocation, itemCVSFile.ToString());
                        saved = true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
