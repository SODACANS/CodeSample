using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdrawl = -1
    }

    public class Transaction
    {
        private readonly TransactionType TransactionType;
        private readonly long StartingBalance;
        public readonly long EndingBalance;
        private readonly uint Amount;

        public Transaction(TransactionType transactionType, long startingBalnace, uint amount)
        {
            TransactionType = transactionType;
            Amount = amount;
            StartingBalance = startingBalnace;
            int sign = TransactionType == TransactionType.Deposit ? 1 : -1;
            EndingBalance = StartingBalance + sign * Amount;
        }
    }
}
