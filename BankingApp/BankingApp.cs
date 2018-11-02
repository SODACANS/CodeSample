using System;
using System.Collections.Generic;
using BankingApp.Models;
using McMaster.Extensions.CommandLineUtils;

namespace BankingApp
{
    class BankingApp
    {
        private ICollection<User> Users = new List<User>();
        private User LoggedInUser;
        private bool IsAuthenticated {
            get
            {
                return LoggedInUser != null;
            }
        }

        private static void ConfigureApp(CommandLineApplication app)
        {
            app.Name = "BankingApp";
            app.Description = "A simple banking app for recording transactions.";
            app.HelpOption(true);
            app.Command("login", loginCmd =>
            {
                loginCmd.Description = "The command to log-in to the app.";
                loginCmd.OnExecute(() =>
                {
                    Console.WriteLine(loginCmd.Name);
                });
            });
            app.Command("exit", loginCmd =>
            {
                loginCmd.Description = "The command to exit the app.";
                loginCmd.OnExecute(() =>
                {
                    Console.WriteLine(loginCmd.Name);
                });
            });
        }

        static void Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication();
            ConfigureApp(app);
            app.Execute(args);
            args = Prompt.GetString("bankingapp> ").Split(' ');
            app.Execute(args);
            args = Prompt.GetString("bankingapp> ").Split(' ');
            app.Execute(args);
            args = Prompt.GetString("bankingapp> ").Split(' ');
            app.Execute(args);
            Console.ReadKey();
        }
    }
}
