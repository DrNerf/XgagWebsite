using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XgagWebsite.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
            Votes = new List<Vote>();
        }

        public int PostId { get; set; }

        public string Title { get; set; }

        public virtual Image Image { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public string YouTubeLink { get; set; }

        public int CalculateScore()
        {
            return Votes?.Select(v => v.Type).Cast<int>().Sum() ?? 0;
        }

        public int CountComments()
        {
            var count = Comments.Count();
            count += Comments.Where(c => c.Comments.Any())
                .SelectMany(c => c.Comments).Count();
            return count;
        }

        public bool AnyNewComments()
        {
            var anyNewComments = Comments.Any(c => c.DateTimePosted.Date == DateTime.Now.Date);
            var anyNewRepliesComments = Comments.Where(c => c.Comments.Any())
                .SelectMany(c => c.Comments)
                .Any(c => c.DateTimePosted.Date == DateTime.Now.Date);

            return anyNewComments || anyNewRepliesComments;
        }
    }
}