using System.ComponentModel.DataAnnotations;
using XgagWebsite.Enums;

namespace XgagWebsite.Models
{
    public class ChitChatVote
    {
        [Key]
        public int ChitChatVoteId { get; set; }

        public virtual ChitChat ChitChat { get; set; }

        public VoteType VoteType { get; set; }

        public virtual ApplicationUser Voter { get; set; }
    }
}