using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Concrete
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public RequestStatus Status { get; set; }

        public int? ApprovedById { get; set; }
        public User ApprovedBy { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string DepartmentName { get; set; } 
        public string Username { get; set; }

    }
}
