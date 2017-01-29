using System;
using System.Linq;
using System.Security.Principal;
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
    }
}