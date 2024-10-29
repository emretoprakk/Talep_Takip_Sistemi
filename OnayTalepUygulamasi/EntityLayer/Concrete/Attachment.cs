using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Attachment
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
