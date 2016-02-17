using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IAccount
    {
          string accountType { get; }
            //ALSO IMPLEMENT IDS
            List<Transaction> transactions { get; set; }


            double CheckIfTransactionsExist(bool checkAll);
            // int GetAccountType();
            void Deposit(double amount);
            void Withdraw(double amount);
            double InterestEarned();

            double sumTransactions();

    }
}
