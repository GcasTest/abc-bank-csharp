using abc_bank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Model
{
    public class Bank
    {
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String CustomerSummary() {
            StringBuilder summary = new StringBuilder();

            summary.Append("Customer Summary");

            foreach (Customer c in customers)
                summary.Append(string.Format("{0} - {1} ({2})", Environment.NewLine, c.GetName(), Utilities.PluralFormatter(c.GetNumberOfAccounts(), "account")));
            
            return summary.ToString();
        }

        public decimal totalInterestPaid()
        {
            decimal total = 0;
            foreach(Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String GetFirstCustomer()
        {
            try
            {
                customers = null;
                return customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
