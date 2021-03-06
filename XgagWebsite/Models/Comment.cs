﻿using BusinessModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class Comment
    {
        public Comment()
        {
            Comments = new List<Comment>();
        }

        [Key]
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime DateTimePosted { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual Post PostOwner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Comment Parent { get; set; }

        public static implicit operator CommentModel(Comment comment)
        {
            if (comment != null)
            {
                return new CommentModel()
                {
                    Comments = comment.Comments.Select(c => (CommentModel)c),
                    DateTimePosted = comment.DateTimePosted.ToUniversalTime().Ticks,
                    Id = comment.CommentId,
                    Owner = comment.Owner,
                    Parent = comment.Parent,
                    PostOwner = comment.PostOwner,
                    Text = comment.Text
                }; 
            }

            return null;
        }
    }
}