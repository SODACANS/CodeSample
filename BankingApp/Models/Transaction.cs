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
        private readonly DateTime DateRecorded = DateTime.Now;

        public Transaction(TransactionType transactionType, long startingBalnace, uint amount)
        {
            TransactionType = transactionType;
            Amount = amount;
            StartingBalance = startingBalnace;
            int sign = TransactionType == TransactionType.Deposit ? 1 : -1;
            EndingBalance = StartingBalance + sign * Amount;
        }

        public string Describe()
        {
            return $"{DateRecorded.ToShortDateString()}     {TransactionType.ToString()}     Starting Balance: {(StartingBalance/100.0).ToString("C2")}     Endiing Balance: {(EndingBalance / 100.0).ToString("C2")}     Amount: {(Amount / 100.0).ToString("C2")}";
        }
    }
}
