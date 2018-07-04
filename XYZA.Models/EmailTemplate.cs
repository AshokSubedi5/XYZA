using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZA.Models
{
   public class EmailTemplate
    {       
        public string Id { get; set; }       
        public string Subject { get; set; }       
        public string Message { get; set; }
        public DateTime Created_Date { get; set; }
        public string Remarks { get; set; }
    }
}
