using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using XYZA.Models;

namespace XYZA.BLDA
{
   public class CustomerBLL
    {
        Entity _entity;

        public CustomerBLL() {
            _entity = new Entity();
        }
        public IEnumerable<CustomerModels> GetAllCustomer() {
            return _entity.Customers.Take(12);
        }


        public CustomerModels GetCustomer(string customerId) {
            return _entity.Customers.FirstOrDefault(x => x.Id == customerId);
        }

        public bool AddCustomer(CustomerModels customer)
        {
            try
            {
                customer.Id = Guid.NewGuid().ToString();
                customer.Created_Date = DateTime.Now;
                customer.Modify_Date = DateTime.Now;
                _entity.Customers.Add(customer);
                _entity.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateCustomer(CustomerModels customer)
        {
            try
            {
                CustomerModels oldCustomer = _entity.Customers.Find(customer.Id);
                
                oldCustomer.FirstName = customer.FirstName;
                oldCustomer.MiddleName = customer.MiddleName;
                oldCustomer.LastName = customer.LastName;
                oldCustomer.Phone = customer.Phone;
                oldCustomer.Email = customer.Email;
                oldCustomer.Modify_Date = DateTime.UtcNow;
                oldCustomer.State = customer.State;
                oldCustomer.Country = customer.Country;
                oldCustomer.City = customer.City;
                oldCustomer.Address1 = customer.Address1;
                oldCustomer.Address2 = customer.Address2;
                oldCustomer.Facebook = customer.Facebook;
                oldCustomer.Instagram = customer.Instagram;
                oldCustomer.Remarks = customer.Remarks;

                _entity.Entry(oldCustomer).State = EntityState.Modified;              
                _entity.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(string id)
        {
            try
            {
                CustomerModels customer = _entity.Customers.Find(id);
                _entity.Customers.Remove(customer);
                _entity.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
