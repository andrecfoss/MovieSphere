using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChat.Shared
{
    public class PostMessage
    {

        public long Id { get; set; }
        public string FromUserId { get; set; }

        public string UserName { get; set; }    
        public string Message { get; set; }

        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }


        public ICollection <CommentsMessage> Comments { get; set; }
    }
}
