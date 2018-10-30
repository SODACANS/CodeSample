using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class User
    {
        private readonly ICollection<BankAccount> Accounts = new List<BankAccount>();
        private readonly string FirstName;
        private readonly string LastName;
        private readonly string UserName;
        private readonly string passwordHash;

        public User(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            passwordHash = password;
            UserName = userName;
        }
    }
}
