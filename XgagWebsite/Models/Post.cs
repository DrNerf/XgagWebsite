﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public virtual Image Image { get; set; }

        public int Score { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}