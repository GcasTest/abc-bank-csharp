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
        BankApp ict = new BankApp();

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public List<Account> getAllAccounts()
        {
            List<Account> bankAllAccounts = new List<Account>();
            foreach (Customer c in customers)
            {
                bankAllAccounts.InsertRange(bankAllAccounts.Count, c.getAccounts());
            }

            return bankAllAccounts;
        }

        public double totalInterestPaid()
        {
            double total = 0;

            foreach (Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public bool DailyInterestAccure()
        {
            try
            {
                List<Task> tasks = new List<Task>();
                foreach (Account acc in getAllAccounts())
                {
                    Task t = Task.Run(() => acc.dailyInterestAccure());
                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());
                return true;
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
        }

        //Unused Method -- can be removed
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
