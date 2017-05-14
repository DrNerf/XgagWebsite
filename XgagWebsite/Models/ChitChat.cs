using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class ChitChat
    {
        [Key]
        public int ChitChatId { get; set; }

        public string Content { get; set; }

        public virtual ICollection<ChitChatVote> Votes { get; set; }

        public DangerType DangerType { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public ChitChat()
        {
            Votes = new List<ChitChatVote>();
        }
    }
}