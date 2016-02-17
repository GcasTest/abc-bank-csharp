using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            IAccount checkingAccount = new CheckingAccount(); //Account(Account.CHECKING);
            IAccount savingsAccount = new SavingsAccount();//Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingsAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTransferFunds()
        {
            Customer oscar = new Customer("Oscar");
            IAccount oscarsChecking = new CheckingAccount(); //Account(Account.CHECKING);
            IAccount oscarsSavings = new SavingsAccount();
            oscar.OpenAccount(oscarsChecking);
            oscarsChecking.Deposit(100.0);
            oscar.OpenAccount(oscarsSavings);

            oscar.TransferFunds(oscarsChecking, oscarsSavings, 100);
            Assert.AreEqual(0, oscarsChecking.sumTransactions());

        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestCustomerNameWithSpace()
        {
            var bill = new Customer(" ");

        }

        [TestMethod]
        [Ignore]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingsAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
