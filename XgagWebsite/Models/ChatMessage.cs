using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XgagWebsite.Models
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [ForeignKey(nameof(Owner))]
        public virtual string OwnerId { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }

        public ChatMessage Clone()
        {
            return new ChatMessage()
            {
                ChatMessageId = ChatMessageId,
                DateTime = DateTime,
                Message = Message,
                Owner = Owner,
                OwnerId = OwnerId
            };
        }
    }
}