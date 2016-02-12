using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;
using abc_bank.Model;
using abc_bank.BusinessComponents.InterestStrategies.Impl;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new MaxiSavingsAccount());
            bank.AddCustomer(john);

            var expected = string.Format("Customer Summary{0} - John (1 account)", Environment.NewLine);
            var actual = bank.CustomerSummary();
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            CheckingAccount checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0m);

            Assert.AreEqual(0.1m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void SavingsAccount()
        {
            Bank bank = new Bank();
            SavingsAccount savingsAccount = new SavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0m);

            Assert.AreEqual(2.0m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void MaxiSavingsAccount()
        {
            Bank bank = new Bank();
            MaxiSavingsAccount maxiSavingsAccount = new MaxiSavingsAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0m);
            Assert.AreEqual(170, bank.totalInterestPaid());

            // change the interest strategy
            maxiSavingsAccount.SetInterestStrategy(new MaxiSavingsAccountFivePercentInterestStrategy());
            Assert.AreEqual(150, bank.totalInterestPaid());
        }
    }
}
