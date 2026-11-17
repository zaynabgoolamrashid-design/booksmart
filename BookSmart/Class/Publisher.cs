using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Publisher
    {
        public int publisherId;
        public string publisherName;


        public Publisher(string publisherName)
        {
            this.publisherName = publisherName;
        }

        //  Check if the publisher name is valid
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(publisherName);
        }

        //  Rename the publisher
        public void RenamePublisher(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                publisherName = newName;
            }
        }
    }
}