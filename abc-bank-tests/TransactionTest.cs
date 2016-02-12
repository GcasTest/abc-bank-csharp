using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Model;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction_Positive_Amount_Is_Deposit()
        {
            Transaction t = new Transaction(5);
            Assert.IsTrue(t.transactionType == TransactionTypes.Deposit);
        }

        [TestMethod]
        public void Transaction_Negative_Amount_Is_Withdrawal()
        {
            Transaction t = new Transaction(-5);
            Assert.IsTrue(t.transactionType == TransactionTypes.Withdrawal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transaction_Zero_Amount_Throws_Exception()
        {            
            Transaction t = new Transaction(0);
        }    

    }
}
