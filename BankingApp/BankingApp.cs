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
                userName.IsRequired(false, "Please provide a user name to login.");
                loginCmd.OnExecute(() =>
                {
                    BankingApp.Login(userName.Value);
                });
            });
            // Configure logout command
            app.Command("logout", logoutCmd =>
            {
                logoutCmd.Description = "The command to logout of your account.";
                logoutCmd.OnExecute(() => BankingApp.Controller.Logout());
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
                userCmd.Description = "The command to create a new banking app user.";
                userCmd.OnExecute(() => BankingApp.CreateUser());
            });
            // Configure withdrawl command
            app.Command("withdraw", withdrawCmd =>
            {
                withdrawCmd.Description = "The command to record a withdrawl from your account.";
                CommandArgument amountArg = withdrawCmd.Argument("amount", "The amount to withdraw from your account (in cents)");
                amountArg.IsRequired(false, "Please proved an amount to withdraw from your account");
                withdrawCmd.OnExecute(() => {
                    if (UInt32.TryParse(amountArg.Value, out uint amount))
                    {
                        BankingApp.Controller.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse withdrawl amount.");
                    }
                });
            });
            // Configure deposit command
            app.Command("deposit", withdrawCmd =>
            {
                withdrawCmd.Description = "The command to record a deposit to your account.";
                CommandArgument amountArg = withdrawCmd.Argument("amount", "The amount to deposit to your account (in cents)");
                amountArg.IsRequired(false, "Please proved an amount to deposit to your account");
                withdrawCmd.OnExecute(() => {
                    if (UInt32.TryParse(amountArg.Value, out uint amount))
                    {
                        BankingApp.Controller.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse deposit amount.");
                    }
                });
            });
            // Configure balance command
            app.Command("balance", balanceCmd =>
            {
                balanceCmd.Description = "The command to view the balance of your account.";
                balanceCmd.OnExecute(() => BankingApp.Balance());
            });
            // Configure history command
            app.Command("history", historyCmd =>
            {
                historyCmd.Description = "The command to view the transaction history of your account.";
                historyCmd.OnExecute(() => BankingApp.History());
            });
        }

        static void Main(string[] args)
        {
            // Start the main prompt loop
            while (!BankingApp.Exit)
            {
                CommandLineApplication app = new CommandLineApplication();
                ConfigureApp(app);
                string colon = Controller.GetLoggedInUserName() != null ? ":" : "";
                string prompt = $"bankingapp{colon}{Controller.GetLoggedInUserName()}>";
                args = Prompt.GetString(prompt).Split(' ');
                try
                {
                    app.Execute(args);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(CommandParsingException ex)
                {
                    // Prevent a crash if the arguments are able to be parsed.
                }
            }
        }

        private static void CreateUser()
        {
            // Read-in user details
            string firstName = Prompt.GetString("First name:");
            string lastName = Prompt.GetString("Last name:");
            string userName = Prompt.GetString("Select a user name:");
            // Ensure user name is unique.
            while (BankingApp.Controller.IsUserNameTaken(userName))
            {
                Console.WriteLine("That user name is already taken.");
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
        
        private static void Login(string userName)
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

        private static void Balance()
        {
            Console.WriteLine($"Your current account balance is {BankingApp.Controller.GetBalance().ToString("C2")}");
        }

        private static void History()
        {
            Console.WriteLine(BankingApp.Controller.History());
        }
    }
}
