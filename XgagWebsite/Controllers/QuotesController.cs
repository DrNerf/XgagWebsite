using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XgagWebsite.AjaxResponses;
using XgagWebsite.Enums;
using XgagWebsite.Models;

namespace XgagWebsite.Controllers
{
    [Authorize]
    public class QuotesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Code()
        {
            return View();
        }

        public ActionResult GetAll(QuoteType type = QuoteType.Normal)
        {
            var quotes = DbContext.Quotes
                .Where(q => q.QuoteType == type)
                .OrderByDescending(q => q.DateTimeCreated)
                .ToList();
            return JsonResult(quotes.Select(q => QuoteItemResponse.Transform(q)));
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(string text, string authorId, QuoteType type = QuoteType.Normal)
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
            newQuote.QuoteType = type;
            var dbQuote = DbContext.Quotes.Add(newQuote);
            await DbContext.SaveChangesAsync();

            return JsonResult(new GenericOperationResponse<QuoteItemResponse>(
                QuoteItemResponse.Transform(dbQuote)));
        }
    }
}