using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        Account checkingAccount;
        Account savingsAccount;
        Account maxiSavingsAccount;

        Customer customer;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer("John");
            checkingAccount = new Account(AccountType.CHECKING, customer);
            savingsAccount = new Account(AccountType.SAVINGS, customer);
            maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, customer);
        }

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();

            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);
            checkingAccount.Deposit(100.0);
            

            Assert.AreEqual(0.100324126259039, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsAccount()
        {
            Bank bank = new Bank();
            bank.AddCustomer(customer.OpenAccount(savingsAccount));
            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0049704022746937, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }

        //[TestMethod]
        //public void MaxiSavingsAccount()
        //{
        //    Bank bank = new Bank();
        //    bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
        //    maxiSavingsAccount.Deposit(3000.0);
        //    maxiSavingsAccount.Deposit(100.0);
        //    maxiSavingsAccount.Withdraw(100.0);

        //    Assert.AreEqual(153.80248940234196, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        //}

        //[TestMethod]
        //public void MaxiSavingsAccount9days()
        //{
        //    Bank bank = new Bank();
        //    bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
        //    maxiSavingsAccount.Deposit(3000.0);
        //    maxiSavingsAccount.Deposit(100.0);
        //    maxiSavingsAccount.Withdraw(100.0);

        //    Assert.AreEqual(3.0014963864118727, bank.TotalInterestPaid(DateTime.Now.AddDays(9)), DOUBLE_DELTA);
        //}
    }
}
