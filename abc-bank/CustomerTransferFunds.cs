using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CustomerTransferFunds : ITransferFunds
    {
        public Customer Account_To_Account_Transfer(Customer customer, IAccount fromAccnt, IAccount toAccnt, double amount)
        {
            //check priviledges based on customer object
            // customer.Accounts.
            if (amount <= 0)
                throw new ArgumentException("Amount must be more than zero");
            if (fromAccnt.sumTransactions() < amount)
                throw new Exception("Not enough funds in source account");

            try
            {
                fromAccnt.Withdraw(amount);
                toAccnt.Deposit(amount);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Unable to Transfer", ex);
            }

            return customer;
        }
    }
}
