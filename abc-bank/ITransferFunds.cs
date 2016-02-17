using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface ITransferFunds
    {
        Customer Account_To_Account_Transfer(Customer customer, IAccount fromAccnt, IAccount toAccnt2, double amount);
        //void Account_To_ExternalBank_Transfer();
    }
}
