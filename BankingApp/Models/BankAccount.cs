using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class BankAccount
    {
        private static uint NEXT_ACCOUNT_NUMBER = 1;

        public readonly uint AccountNumber;
        public long Balance { get; private set; } = 0;
        private readonly ICollection<Transaction> Transactions = new List<Transaction>();

        public BankAccount()
        {
            AccountNumber = NEXT_ACCOUNT_NUMBER;
            NEXT_ACCOUNT_NUMBER++;
        }

        private long AddTransaction(TransactionType transactionType, uint amount)
        {
            Transaction transaction = new Transaction(transactionType, Balance, amount);
            Transactions.Add(transaction);
            Balance = transaction.EndingBalance;
            return Balance;
        }

        public long Deposit(uint amount)
        {
            return AddTransaction(TransactionType.Deposit, amount);
        }

        public long Withdraw(uint amount)
        {
            return AddTransaction(TransactionType.Withdrawl, amount);
        }
    }
}
