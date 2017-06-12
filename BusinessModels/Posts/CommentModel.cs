using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public long DateTimePosted { get; set; }

        public virtual ApplicationUserModel Owner { get; set; }

        public virtual PostModel PostOwner { get; set; }

        public virtual IEnumerable<CommentModel> Comments { get; set; }

        public virtual CommentModel Parent { get; set; }
    }
}
