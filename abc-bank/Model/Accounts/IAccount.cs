using abc_bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IAccount
    {
        List<Transaction> Transactions { get; set; }
        
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal InterestEarned();
        decimal SumTransactions();
        String StatementForAccount();
    }
}
