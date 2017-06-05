using BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static implicit operator PostModel(Post source)
        {
            return new PostModel()
            {
                Score = source.CalculateScore(),
                ImageUrl = source.Image != null ? $"/Images/View/{source.Image.ImageId}" : string.Empty,
                Title = source.Title,
                CommentsCount = source.Comments?.Count() ?? 0,
                AnyNewComments = source.AnyNewComments(),
                Id = source.PostId,
                DateCreated = source.DateCreated.ToUniversalTime(),
                YouTubeLink = source.YouTubeLink
            };
        }
    }
}