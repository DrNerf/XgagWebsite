using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Enums;
using XgagWebsite.Helpers;
using XgagWebsite.Models;

namespace XgagWebsite
{
    public static class IIdentityExtensions
    {
        public static int GetRemainingVotes(this IIdentity identity, VoteType voteType)
        {
            var result = 0;
            var maxVotes = ConfigurationHelper.Instance.MaxDailyVotes;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users
                    .First(x => x.UserName == identity.Name);

                result = maxVotes - user.DailyVotes
                    .Count(x => x.VoteDate == DateTime.Now.Date && x.VoteType == voteType);
            }

            return result;
        }

        public static string GetProfilePictureUrl(this IIdentity identity)
        {
            var result = string.Empty;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users
                    .First(x => x.UserName == identity.Name);

                result = user.ProfilePictureUrl;
                if (result[0] == '~')
                {
                    result = VirtualPathUtility.ToAbsolute(result);
                }
            }

            return result;
        }

        public static string GetProfilePictureUrl(this ApplicationUser user, string baseUrl)
        {
            var result = user.ProfilePictureUrl;
            if (result[0] == '~')
            {
                result = $"{baseUrl}{VirtualPathUtility.ToAbsolute(result)}";
            }

            return result;
        }

        public static string TrimRelativePrefix(this ApplicationUser user)
        {
            return user.ProfilePictureUrl.TrimStart('~');
        }
    }
}