using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class User
    {

        public int UserId;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Password;
        public string Role;


        public User(string FirstName, string LastName, string Email, string Password, string Role)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.Role = Role;

        }

        // Get full name
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        //  Check if user is admin
        public bool IsAdmin()
        {
            return Role == "admin";
        }

        //  Get email domain
        public string GetEmailDomain()
        {
            if (Email.Contains("@"))
            {
                return Email.Split('@')[1];
            }
            return "";
        }


    }
}
