﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CustomerManager
    {
        public List<Customer> GetCustomers()
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                List<Customer> customers = new List<Customer>();
                customers = db.Customers.ToList();
                return customers;
            }
            
        }

        public Customer GetCustomerById(int id)
        {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                Customer? customer = db.Customers.Find(id);
                return customer;
            }            

        }

        public void UpdateCustomer(int id, Customer customer)
                {
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                Customer? customerToEdit = db.Customers.Find(id);
                if ( customerToEdit!=null)
                {
                    customerToEdit.CustFirstName = customer.CustFirstName;
                    customerToEdit.CustLastName = customer.CustLastName;
                    customerToEdit.CustAddress = customer.CustAddress;
                    customerToEdit.CustCity = customer.CustCity;
                    customerToEdit.CustProv = customer.CustProv;
                    customerToEdit.CustPostal = customer.CustPostal;
                    customerToEdit.CustCountry = customer.CustCountry;
                    customerToEdit.CustHomePhone = customer.CustHomePhone;
                    customerToEdit.CustBusPhone= customer.CustBusPhone;
                    customerToEdit.CustEmail = customer.CustEmail;

                }
                    db.SaveChanges();
                

            }
                }

        public static string GetAgentNameById(int id)
        {
            using(TravelExpertsContext db = new TravelExpertsContext())
            {
                string AgentFirstName = db.Agents.Where(a => a.AgentId == id).Select(a => a.AgtFirstName).FirstOrDefault();
                string AgentLastName = db.Agents.Where(a => a.AgentId == id).Select(a => a.AgtLastName).FirstOrDefault();
                string AgentFullName = AgentFirstName + " " + AgentLastName;
                return AgentFullName;
            }
        }

		public void UpdateBalance(int custID, decimal updatedBalance)
		{
			using (TravelExpertsContext db = new TravelExpertsContext())
			{
				Customer customer = db.Customers.Find(custID);
				if (customer != null)
				{
					customer.Balance = updatedBalance;
					db.SaveChanges();
				}
			}
		}
	}
}
