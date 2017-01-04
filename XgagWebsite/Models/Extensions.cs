using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Services = XgagWebsite.Services;

namespace XgagWebsite.Models
{
    public static class Extensions
    {
        public static Task NotifyForNewComment(this ApplicationUser user, Post post, Comment comment)
        {
            return Task.Factory.StartNew(() => 
            {
                using (var emailService = new Services.EmailService(new string[] { user.Email }))
                {
                    emailService.SendToAllReceivers(string.Format(
                        "Your post '{0}' just got commented by {1}!",
                        post.Title,
                        comment.Owner.UserName));
                }
            });
        }
    }
}