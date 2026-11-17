using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Category
    {
        public int categoryId;
        public string categoryName;


        public Category(string categoryName)
        {
            this.categoryName = categoryName;
        }

        //  Check if the category name is valid
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(categoryName);
        }

        //  Rename the category
        public void RenameCategory(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                categoryName = newName;
            }
        }
    }
}