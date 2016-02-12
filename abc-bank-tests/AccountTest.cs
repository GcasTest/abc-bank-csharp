using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts.Impl;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountTest_Withdraw_More_Than_Account_Balance()
        {
            IAccount account = new CheckingAccount();
            account.Deposit(100);

            try
            {
                account.Withdraw(150);
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual(ae.Message, "cannot withdraw more than current account balance");
                return;
            }

            Assert.Fail("No exception was thrown");
        }
    }
}
