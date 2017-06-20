using System.Collections.Generic;
using XgagWebsite.Models;

namespace XgagWebsite.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<ApplicationUser> TopContributors { get; set; }

        public IEnumerable<Post> TopPosts { get; set; }

        public PostOfTheDay PostOfTheDay { get; set; }
    }
}