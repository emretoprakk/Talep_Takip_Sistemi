using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Request> Requests { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
    