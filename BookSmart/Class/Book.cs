using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Book
    {
        public int bookId;
        public string title;
        public int authorId;
        public int publisherId;
        public int categoryId;
        public decimal price;
        public int stock;
        public int publishedYear;
        public string description;
        public string CoverImagePath;



        public Book(string title, int authorId, int publisherId, int categoryId, decimal price, int stock, int publishedYear, string description,string CoverImagePath)
        {
            this.title = title;
            this.authorId = authorId;
            this.publisherId = publisherId;
            this.categoryId = categoryId;
            this.price = price;
            this.stock = stock;
            this.publishedYear = publishedYear;
            this.description = description;
            this.CoverImagePath = CoverImagePath;
        }

        //  Check if the book is in stock
        public bool IsInStock()
        {
            return stock > 0;
        }

        //  Reduce stock when a book is sold
        public void ReduceStock(int quantitySold)
        {
            if (quantitySold > 0 && quantitySold <= stock)
            {
                stock -= quantitySold;
            }
        }

        //  Check if the price is valid
        public bool IsPriceValid()
        {
            return price >= 0;
        }
    }
}