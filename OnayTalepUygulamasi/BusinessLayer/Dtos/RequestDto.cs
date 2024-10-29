using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class RequestDto
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public int ApprovedById { get; set; }
        public string DepartmentName { get; set; }
        public string Username { get; set; }


    }
}
