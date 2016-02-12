using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;
using abc_bank.Model;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            CheckingAccount checkingAccount = new CheckingAccount();
            SavingsAccount savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0m);
            savingsAccount.Deposit(4000.0m);
            savingsAccount.Withdraw(200.0m);

            var actual = henry.GetStatement();
            var expected = "Statement for HENRY" + Environment.NewLine +
                    Environment.NewLine +
                    "Checking Account" + Environment.NewLine +
                    "  deposit $100.00" + Environment.NewLine +
                    "Total $100.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Savings Account" + Environment.NewLine +
                    "  deposit $4,000.00" + Environment.NewLine +
                    "  withdrawal $200.00" + Environment.NewLine +
                    "Total $3,800.00" + Environment.NewLine +
                    Environment.NewLine + 
                    "Total In All Accounts: $3,900.00";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingsAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccounts()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            oscar.OpenAccount(new MaxiSavingsAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_New_Account_With_Empty_Name()
        {
            Customer customer = new Customer("   ");
        }
    }
}
