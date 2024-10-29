using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class UpdateStatusRequest
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public RequestStatus Status { get; set; }
    }
}
