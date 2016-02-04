using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;
        Bank bank = null;

        [TestInitialize]
        public void TestInitialize()
        {
            bank = Bank.getInstance();
        }


        [TestMethod]
        public void CustomerSummary() 
        {
           // Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Customer mike = new Customer("Mike");
            mike.OpenAccount(new Account(Account.SAVINGS));
            bank.AddCustomer(mike);

            Assert.AreEqual("Customer Summary\n - John (1 account)\n - Mike (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void TotalInterestPaid() {
            //Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);
           
            Account savingsAccount = new Account(Account.SAVINGS);
            savingsAccount.Deposit(1500.0);
            bill.OpenAccount(savingsAccount);

            Assert.AreEqual(2.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }


        [TestMethod]
        public void InterestAccured()
        {
          //  Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Harry").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Withdraw(1000.0);

            bank.DailyInterestAccure();

            Assert.AreEqual(4.1, bank.totalInterestPaid());
        }
    }
}
