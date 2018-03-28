using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using XYZA.Models;


namespace XYZA.BLDA
{
    public class Entity : DbContext
    {
        public Entity() : base("name=ConnectionStr") { }
        public DbSet<CustomerModels> Customers { get; set; }
    }
}
