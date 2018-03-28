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
            return _entity.Customers;
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
                oldCustomer.LastName = customer.LastName;
                oldCustomer.Address = customer.Address;
                oldCustomer.Email = customer.Email;
                oldCustomer.Modify_Date = DateTime.Now;
                oldCustomer.Phone = customer.Phone;
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
