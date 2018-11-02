using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class User
    {
        public readonly BankAccount Account = new BankAccount();
        private readonly string FirstName;
        private readonly string LastName;
        public readonly string UserName;
        public readonly string PasswordHash;
        public readonly string Salt;

        public User(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            (PasswordHash, Salt) = PasswordHandler.HashPassword(password);
            UserName = userName;
        }
    }
}
