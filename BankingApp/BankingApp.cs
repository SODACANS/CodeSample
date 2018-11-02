using System;
using BankingApp.Models;
using McMaster.Extensions.CommandLineUtils;

namespace BankingApp
{
    class BankingApp
    {

        private static Controller Controller = new Controller();
        private static bool Exit = false;

        private static void ConfigureApp(CommandLineApplication app)
        {
            // Basic app info
            app.Name = "BankingApp";
            app.Description = "A simple banking app for recording transactions.";
            app.HelpOption(true);
            // Configure login command
            app.Command("login", loginCmd =>
            {
                loginCmd.Description = "The command to log-in to the app.";
                CommandArgument userName = loginCmd.Argument("user", "The user name for the user you wish to login as.");
                loginCmd.OnExecute(() =>
                {
                    BankingApp.Login(userName.Value);
                });
            });
            // Configure exit command
            app.Command("exit", exitCmd =>
            {
                exitCmd.Description = "The command to exit the app.";
                exitCmd.OnExecute(() =>
                {
                    BankingApp.Exit = true;
                });
            });
            // Configure create user command
            app.Command("create-user", userCmd =>
            {
                userCmd.Description = "the command to create a new banking app user.";
                userCmd.OnExecute(() => BankingApp.CreateUser());
            });
            
        }

        static void Main(string[] args)
        {
            CommandLineApplication app = new CommandLineApplication();
            ConfigureApp(app);
            while (!BankingApp.Exit)
            {
                args = Prompt.GetString("bankingapp>").Split(' ');
                try
                {
                    app.Execute(args);
                }
                catch (Exception ex)
                {
                    // CommandLineApplication will take care of printing a message in this case.
                }
            }
        }

        static void CreateUser()
        {
            // Read-in user details
            string firstName = Prompt.GetString("First name:");
            string lastName = Prompt.GetString("Last name:");
            string userName = Prompt.GetString("Select a user name:");
            // Ensure user name is unique.
            while (BankingApp.Controller.IsUserNameTaken(userName))
            {
                userName = Prompt.GetString("Select a user name:");
            }
            string password = Prompt.GetPassword("Create a password:");
            string confimPass = Prompt.GetPassword("Confirm your password:");
            // If passwords match create the new user
            while (password != confimPass)
            {
                Console.WriteLine("The passwords did not match.");
                password = Prompt.GetPassword("Create a password:");
                confimPass = Prompt.GetPassword("Confirm your password:");
            }
            BankingApp.Controller.CreateUser(firstName, lastName, userName, password);
            Console.WriteLine($"User for {firstName} {lastName} created.");
        }
        
        static void Login(string userName)
        {
            // Allow 3 password tries
            const int passTries = 3;
            for (int i = 0; i < passTries; i++)
            {
                String pass = Prompt.GetPassword("password:");
                if (BankingApp.Controller.Login(userName, pass))
                {
                    return;
                }
            }
            Console.WriteLine("Login attempts exceeded.");
        }
    }
}
