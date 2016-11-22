using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public int Score { get; set; }

        public DateTime DateCreated { get; set; }
    }
}