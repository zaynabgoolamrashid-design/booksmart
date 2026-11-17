using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BookSmart
{
    public class CartHelper
    {
        public static DataTable GetCartFromSession()
        {
            DataTable cart = HttpContext.Current.Session["Cart"] as DataTable;
            if (cart == null)
            {
                cart = new DataTable();
                cart.Columns.Add("Book_id", typeof(int));
                cart.Columns.Add("Title", typeof(string));
                cart.Columns.Add("Price", typeof(decimal));
                cart.Columns.Add("Quantity", typeof(int));
                HttpContext.Current.Session["Cart"] = cart;
            }
            return cart;
        }

        public static void AddToCart(int bookId, string title, decimal price, int quantity = 1)
        {
            DataTable cart = GetCartFromSession();

            DataRow existingRow = null;
            foreach (DataRow row in cart.Rows)
            {
                if ((int)row["Book_id"] == bookId)
                {
                    existingRow = row;
                    break;
                }
            }

            if (existingRow != null)
            {
                existingRow["Quantity"] = (int)existingRow["Quantity"] + quantity;
            }
            else
            {
                DataRow newRow = cart.NewRow();
                newRow["Book_id"] = bookId;
                newRow["Title"] = title;
                newRow["Price"] = price;
                newRow["Quantity"] = quantity;
                cart.Rows.Add(newRow);
            }

            HttpContext.Current.Session["Cart"] = cart;
        }
    }
}
