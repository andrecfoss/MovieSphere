using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorChat.Shared
{
    public class CommentsMessage
    {

        public long Id { get; set; }
        public long PostMessageId { get; set; }
        public string FromUserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }


        public virtual PostMessage PostMessage { get; set; }


    }
}
