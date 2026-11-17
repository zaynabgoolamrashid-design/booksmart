using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Order
    {
        public int orderId;
        public int userId;
        public DateTime orderDate;
        public decimal totalAmount;


        public Order(int userId, decimal totalAmount)
        {
            this.userId = userId;
            this.totalAmount = totalAmount;
            this.orderDate = DateTime.Now;
        }

        //  Check if total amount is valid
        public bool IsTotalValid()
        {
            return totalAmount > 0;
        }

        //  Update the total amount 
        public void UpdateTotal(decimal newAmount)
        {
            if (newAmount > 0)
            {
                totalAmount = newAmount;
            }
        }
    }
}