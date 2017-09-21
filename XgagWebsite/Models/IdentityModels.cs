using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.Collections.Generic;
using BusinessModels;
using System;

namespace XgagWebsite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            DailyVotes = new List<UserDailyVote>();
            Messages = new List<ChatMessage>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [DefaultValue(false)]
        public bool IsSubscribedForNewPosts { get; set; }

        [DefaultValue(false)]
        public bool IsSubscribedForComments { get; set; }

        public virtual ICollection<UserDailyVote> DailyVotes { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public string ProfilePictureUrl { get; set; }

        [DefaultValue(false)]
        public bool IsActivated { get; set; }

        public Guid? ApiSessionToken { get; set; }

        public string Trace { get; set; }

        public string Browser { get; set; }

        public string BrowserVersion { get; set; }

        public string Platform { get; set; }

        public DateTime? LastLogin { get; set; }

        public string IPAddress { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }

        public static implicit operator ApplicationUserModel(ApplicationUser user)
        {
            return new ApplicationUserModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Avatar = user.TrimRelativePrefix()
            };
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PersonVote> PeopleVotes { get; set; }

        public DbSet<UserDailyVote> UsersDailyVotes { get; set; }

        public DbSet<RankingHistoryItem> RankingHistory { get; set; }

        public DbSet<PersonRank> PeopleRanking { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<ChitChat> ChitChats { get; set; }

        public DbSet<ChitChatVote> ChitChatVotes { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<PostOfTheDay> PostsOfTheDay { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}