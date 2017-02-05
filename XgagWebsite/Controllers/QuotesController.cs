using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    public class QuotesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var quotes = DbContext.Quotes.OrderByDescending(q => q.DateTimeCreated).ToList();
            return JsonResult(quotes.Select(q => QuoteItemResponse.Transform(q)));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(string text, string authorId)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrEmpty(authorId))
            {
                throw new ArgumentNullException(nameof(authorId));
            }
            int parsedAuthorId;
            var newQuote = DbContext.Quotes.Create();

            if (int.TryParse(authorId, out parsedAuthorId))
            {
                newQuote.Author = DbContext.People.FirstOrDefault(p => p.PersonId == parsedAuthorId);
            }
            else
            {
                newQuote.AnonymousAuthor = authorId;
            }

            newQuote.Text = text;
            newQuote.Owner = GetCurrentUser();
            newQuote.DateTimeCreated = DateTime.Now;
            var dbQuote = DbContext.Quotes.Add(newQuote);
            await DbContext.SaveChangesAsync();

            return JsonResult(new GenericOperationResponse<QuoteItemResponse>(
                QuoteItemResponse.Transform(dbQuote)));
        }
    }
}