using System;
using System.Collections.Generic;
using System.Text;

namespace project.Models
{
    public class Item
    {
        private string itemName;
        private int availableQuantity;
        private int minQuantity = 1;
        private string location;
        private string supplier;
        private string category;

        public Item()
        {

        }
        public Item(string itemName)
        {
            this.itemName = itemName;
        }
        public Item(string itemName, int availableQuantity, int minQuantity, string location, string supplier, string category) : this(itemName)
        {
            this.MinQuantity = minQuantity;
            this.AvailableQuantity = availableQuantity;
            this.Location = location;
            this.Supplier = supplier;
            this.Category = category;
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
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public override string ToString()
        {

            return $"Item Name: {itemName}\nAvailable Quantity: {availableQuantity}\nMin Quantity: {minQuantity}\nLocation: {location}\nSupplier: {supplier}\nCategory: {category}";
        }
    }
}
