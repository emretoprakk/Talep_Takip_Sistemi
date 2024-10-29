using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
