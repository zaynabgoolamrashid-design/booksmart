using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Order_Item
    {
        public int orderItemId;
        public int orderId;
        public int bookId;
        public int quantity;
        public decimal price;


        public Order_Item(int orderId, int bookId, int quantity, decimal price)
        {
            this.orderId = orderId;
            this.bookId = bookId;
            this.quantity = quantity;
            this.price = price;
        }

        //  Calculate total price for this item
        public decimal GetTotal()
        {
            return quantity * price;
        }

        //  Check if the quantity is valid
        public bool IsQuantityValid()
        {
            return quantity > 0;
        }

        //  Update quantity
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity > 0)
            {
                quantity = newQuantity;
            }
        }
    }
}