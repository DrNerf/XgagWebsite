using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.Enums;
using XgagWebsite.Helpers;
using XgagWebsite.Models;
using Service = XgagWebsite.Services;

namespace XgagWebsite.Controllers
{
    public class PostsController : BaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Upload(string title, HttpPostedFileBase uploadImage)
        {
            if (string.IsNullOrEmpty(title) || uploadImage == null)
            {
                throw new HttpException(400, "Invalid input data!");
            }

            var image = await ImagesHelper.SaveImageAsync(uploadImage, DbContext);
            var owner = DbContext.Users.First(u => u.UserName == User.Identity.Name);
            if (image == null)
            {
                throw new HttpException(500, "Invalid input data!");
            }

            var post = DbContext.Posts.Create();
            post.DateCreated = DateTime.Now;
            post.Title = title;
            post.Image = image;
            post.Owner = owner;

            DbContext.Posts.Add(post);
            await DbContext.SaveChangesAsync();

            NotifySubscribedUsersForNewPost();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult View(int id)
        {
            var post = DbContext.Posts.FirstOrDefault(p => p.PostId == id);
            post.Comments = post.Comments
                .OrderByDescending(c => c.DateTimePosted)
                .ToList();

            if (post == null)
            {
                throw new HttpException(404, "Post not found :(");
            }

            return View(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Comment(string commentText, int postId)
        {
            if (string.IsNullOrEmpty(commentText))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid comment data!");
            }

            var post = DbContext.Posts.First(p => p.PostId == postId);
            var comment = DbContext.Comments.Create();
            comment.Owner = DbContext.Users.First(u => u.UserName == User.Identity.Name);
            comment.Text = TryConvertTextToHtml(commentText);
            comment.DateTimePosted = DateTime.Now;
            if (post.Comments == null)
            {
                post.Comments = new List<Comment>() { comment };
            }
            else
            {
                post.Comments.Add(comment);
            }
            await DbContext.SaveChangesAsync();

            if (post.Owner.IsSubscribedForComments)
            {
                try
                {
                    post.Owner.NotifyForNewComment(post, comment);
                }
                catch { }
            }

            return Json(new { IsSuccess = true });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Reply(string replyText, int commentId)
        {
            if (string.IsNullOrEmpty(replyText))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid comment data!");
            }

            var parent = DbContext.Comments.First(p => p.CommentId == commentId);
            var comment = DbContext.Comments.Create();
            comment.Owner = DbContext.Users.First(u => u.UserName == User.Identity.Name);
            comment.Text = TryConvertTextToHtml(replyText);
            comment.DateTimePosted = DateTime.Now;
            if (parent.Comments == null)
            {
                parent.Comments = new List<Comment>() { comment };
            }
            else
            {
                parent.Comments.Add(comment);
            }

            await DbContext.SaveChangesAsync();

            if (parent.PostOwner.Owner.IsSubscribedForComments)
            {
                try
                {
                    parent.PostOwner.Owner.NotifyForNewComment(parent.PostOwner, comment);
                }
                catch { }
            }

            return Json(new { IsSuccess = true });
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Vote(int postId, VoteType voteType)
        {
            var voter = DbContext.Users.First(u => u.UserName == User.Identity.Name);
            var post = DbContext.Posts.FirstOrDefault(p => p.PostId == postId);
            if (!post.Votes.Any(v => v.Voter.Id == voter.Id))
            {
                var vote = DbContext.Votes.Create();
                vote.Post = post;
                vote.Type = voteType;
                vote.Voter = voter;
                post.Votes.Add(vote);
                await DbContext.SaveChangesAsync(); 
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Unvote(int postId)
        {
            var vote = DbContext.Votes
                .Where(v => v.Post.PostId == postId)
                .FirstOrDefault(v => v.Voter.UserName == User.Identity.Name);

            if (vote != null)
            {
                DbContext.Votes.Remove(vote);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private string TryConvertTextToHtml(string text)
        {
            return ImagesHelper.IsImageUrl(text) ? ImagesHelper.GetImageTag(text) : text;
        }

        private Task NotifySubscribedUsersForNewPost()
        {
            var subscriberEmails = DbContext.Users.Where(u => u.IsSubscribedForNewPosts)
                .Select(u => u.Email)
                .ToList();

            return Task.Factory.StartNew(() => 
            {
                if (subscriberEmails.Any())
                {
                    using (var service = new Service.EmailService(subscriberEmails))
                    {
                        service.SendToAllReceivers(string.Format("A new post has been uploaded to Xgag. Go check it out!"));
                    }
                }
            });
        }
    }
}