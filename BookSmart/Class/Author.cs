using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSmart.Class
{
    public class Author
    {
        public int authorId;
        public string authorName;


        public Author(string authorName)
        {
            this.authorName = authorName;
        }

        //  Check if the author name is valid
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(authorName);
        }

        //  Rename the author
        public void RenameAuthor(string newName)
        {
            if (!string.IsNullOrWhiteSpace(newName))
            {
                authorName = newName;
            }
        }
    }
}