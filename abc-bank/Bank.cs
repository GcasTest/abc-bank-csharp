using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> customers;

        #region public
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
            {
                summary.Append("\n - " + c.Name + " (" + Format(c.GetNumberOfAccounts(), "account") + ")"); 
            }
            return summary.ToString();
        }


        public decimal TotalInterestPaid() {
            decimal total = 0.0M;
            foreach(Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        // This method needs ne called daily using a service
        public void DepositeDailyInterestForAllCustomers()
        {
            foreach (Customer c in this.customers)
            {
                c.DepositeDailyInterest(c.Accounts);
            }
        }
        #endregion

        #region private

        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
        #endregion
    }
}
