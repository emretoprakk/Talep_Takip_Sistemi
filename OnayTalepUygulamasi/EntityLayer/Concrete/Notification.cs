using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
    }
}
