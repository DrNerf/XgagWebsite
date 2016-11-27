using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime DateTimePosted { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}