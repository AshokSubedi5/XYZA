using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XYZA.Models
{
   public class EmailTemplate
    {       
        public string Id { get; set; }  
        [Required]
        public string Subject { get; set; }       
        public string Message { get; set; }
        public DateTime Created_Date { get; set; }
        public string Remarks { get; set; }
    }
}
