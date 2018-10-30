using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class BankAccount
    {
        private static uint NEXT_ACCOUNT_NUMBER = 1;

        public uint AccountNumber { get; }
        public long Balance { get; } = 0;
        public User AccountOwner { get; }
        private readonly ICollection<Transaction> Transactions = new List<Transaction>();

        public BankAccount(User owner)
        {
            AccountNumber = NEXT_ACCOUNT_NUMBER;
            NEXT_ACCOUNT_NUMBER++;
            AccountOwner = owner;
        }

        public long RecordTransaction(Transaction transaction)
        {
            throw new Exception("Not yet implemented");
        }
    }
}
