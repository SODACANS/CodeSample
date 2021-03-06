﻿using BankingApp.Models;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApp
{
    class Controller
    {
        private ICollection<User> Users = new List<User>();
        private User LoggedInUser;
        private bool IsAuthenticated
        {
            get
            {
                return LoggedInUser != null;
            }
        }

        public void CreateUser(string firstName, string lastName, string userName, string password)
        {
            User newUser = new User(firstName, lastName, userName, password);
            Users.Add(newUser);
        }

        public bool IsUserNameTaken(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName) != null;
        }

        public bool Login(string userName, string password)
        {
            User user = Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return false;
            }
            if (user.PasswordHash == PasswordHandler.HashPassword(password, user.Salt))
            {
                LoggedInUser = user;
                return true;
            }
            return false;
        }

        internal void Logout()
        {
            LoggedInUser = null;
        }

        public string GetLoggedInUserName()
        {
            return LoggedInUser?.UserName;
        }

        public double GetBalance()
        {
            if (!IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User must be logged in to view an account balance.");
            }
            return LoggedInUser.Account.Balance / 100.0;
        }

        public void Withdraw(uint amount)
        {
            if (!IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User must be logged in to record a withdrawl.");
            }
            LoggedInUser.Account.Withdraw(amount);
        }

        public void Deposit(uint amount)
        {
            if (!IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User must be logged in to record a deposit.");
            }
            LoggedInUser.Account.Deposit(amount);
        }

        public string History()
        {
            if (!IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User must be logged in to view transaction history.");
            }
            return LoggedInUser.Account.GetTransactionRecord();
        }
    }
}
