using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;

namespace abc_bank_tests
{
    [TestClass]
    public class CheckingAccountTest
    {
        [TestMethod]
        public void CheckingAccountTest_SumTransactions()
        {
            IAccount checkingAccount = new CheckingAccount();

            checkingAccount.Deposit(450.75m);
            checkingAccount.Deposit(120.32m);
            checkingAccount.Withdraw(4.67m);

            Assert.AreEqual(566.40m, checkingAccount.SumTransactions());
        }

        [TestMethod]
        public void CheckingAccountTest_InterestEarned_Less_Than_A_Thousand()
        {
            IAccount checkingAccount = new CheckingAccount();

            checkingAccount.Deposit(450.75m);
            checkingAccount.Deposit(120.32m);
            checkingAccount.Withdraw(4.67m);

            Assert.AreEqual(.5664m, checkingAccount.InterestEarned());
        }

        [TestMethod]
        public void CheckingAccountTest_InterestEarned_More_Than_A_Thousand()
        {
            IAccount checkingAccount = new CheckingAccount();

            checkingAccount.Deposit(950.75m);
            checkingAccount.Deposit(1520.32m);
            checkingAccount.Withdraw(95.00m);

            Assert.AreEqual(2.37607m, checkingAccount.InterestEarned());
        }

    }
}
