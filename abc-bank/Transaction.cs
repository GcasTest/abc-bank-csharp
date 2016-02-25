using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{

    public enum TranscationType
    {
        CREDIT = 0,
        DEBIT = 1,
        INTEREST = 2
    }
    public class Transaction
    {
        public readonly decimal amount;

        public DateTime transactionDate;

        #region public
        public Guid TransactionId
        {
            get;
            private set;
        }

        public TranscationType TransactionType { get; set; }

        public Transaction(decimal amount, TranscationType transactionType) 
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
            this.TransactionId = new Guid();
            this.TransactionType = transactionType;
        }
        #endregion
    }
}
