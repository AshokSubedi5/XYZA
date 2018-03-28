using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZA.Models
{
    public class CustomerModels
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modify_Date { get; set; }
        public string Remarks { get; set; }
    }
}
