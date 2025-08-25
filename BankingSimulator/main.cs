using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq.Expressions;
using System.Transactions;
using AccountData;
using AccountManagement;
using MoneyLogic;

namespace BankingMain
{
    public static class Program
    {
        public static Random rnd = new Random();
        public static List<string> accountNames = new List<string>();
        public static List<int> accountPins = new List<int>();
        public static List<int> accountBsbs = new List<int>();
        public static List<object[]> accounts = new List<object[]>();
        public static string logPath = "banklog.txt";
        public static bool running = true;
        public static int frame = 0;
        public static int menu = 0;
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Faure Bank\n");
            Console.WriteLine("[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");
            while (running)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                try
                {
                    new UI(keyPress);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void ViewAccount(string accountName, int accountPin)
        {

        }
    }
    public class UI
    {
        public ConsoleKeyInfo PressedKey { get; set; }
        public UI(ConsoleKeyInfo pressedKey)
        {
            if (pressedKey.Key != ConsoleKey.UpArrow && pressedKey.Key != ConsoleKey.DownArrow && pressedKey.Key != ConsoleKey.Enter && pressedKey.Key != ConsoleKey.Escape)
                throw new ArgumentException("Error 007; invalid key press");
            else
            {
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    Program.frame--;
                    Console.Clear();
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    Program.frame++;
                    Console.Clear();
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    try
                    {
                        ExecuteMethod.ExecuteSelected();
                        Console.Clear();
                        Program.menu = 0;
                        Program.frame = 0;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Program.menu++;
                }
                else if (pressedKey.Key == ConsoleKey.Escape)
                {
                    Program.menu--;
                    Console.Clear();
                    if (Program.menu < 0)
                        Environment.Exit(0);
                }
                switch (Program.frame)
                {
                    case 0:
                        if (Program.menu == 0)

                            Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\n[create account]\nremove account\nedit account\nview account\n");

                        break;
                    case 1:
                        if (Program.menu == 0)

                            Console.WriteLine("\nmanage accounts\n[transfer funds]\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\ncreate account\n[remove account]\nedit account\nview account\n");

                        break;
                    case 2:
                        if (Program.menu == 0)

                            Console.WriteLine("\nmanage accounts\ntransfer funds\n[withdraw funds]\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\ncreate account\nremove account\n[edit account]\nview account\n");

                        break;
                    case 3: 
                        if (Program.menu == 0)
                            Console.WriteLine("\nmanage accounts\ntransfer funds\nwithdraw funds\n[deposit funds]\n");

                        else if (Program.menu == 1)
                            Console.WriteLine("\ncreate account\nremove account\nedit account\n[view account]\n");
                        break;
                    default:
                        Program.frame = 0;
                        if (Program.menu == 0)

                            Console.WriteLine("\n[manage accounts]\ntransfer funds\nwithdraw funds\ndeposit funds\n");

                        else if (Program.menu == 1)

                            Console.WriteLine("\n[create account]\nremove account\nedit account\nview account\n");

                        break;
                }
            }
        }
    }
    public static class ExecuteMethod
    {
        public static void ExecuteSelected()
        {
            if (Program.menu == 1)
            {
                if (Program.frame == 0)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    if (accountName == null)
                        throw new ArgumentException("Error 009; name cannot be null, please try again");
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    if (userPin == null)
                        throw new ArgumentException("Error 010; pin cannot be null, please try again");
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    AccountManaging.CreateAccount(accountName, accountPin);
                    Console.WriteLine("\nNew Account Created\n");
                }
                else if (Program.frame == 1)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    AccountManaging.RemoveAccount(accountName, accountPin);
                }
                else if (Program.frame == 2)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    Console.WriteLine("Enter New Account Name: ");
                    string? newAccountName = Console.ReadLine();
                    if (newAccountName == null)
                        throw new ArgumentException("Error 009; name cannot be null, please try again");
                    Console.WriteLine("Enter New Account Pin: ");
                    string? newUserPin = Console.ReadLine();
                    if (newUserPin == null)
                        throw new ArgumentException("Error 010; pin cannot be null, please try again");
                    int newAccountPin;
                    int.TryParse(newUserPin, out newAccountPin);
                    AccountManaging.EditAccount(accountName, accountPin, newAccountName, newAccountPin);
                }
                else if (Program.frame == 3)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    int index = Program.accounts.FindIndex(a =>
                                                            a[0].Equals(accountName) &&
                                                            a[1].Equals(accountPin));
                    if (index >= 0)
                        Console.WriteLine(Program.accounts[index]);
                }
            }
            else if (Program.menu == 0)
            {
                if (Program.frame == 1)
                {
                    Console.WriteLine("Enter First Account Name: ");
                    string? firstAccountName = Console.ReadLine();
                    Console.WriteLine("Enter First Account Pin: ");
                    string? firstUserPin = Console.ReadLine();
                    int firstAccountPin;
                    int.TryParse(firstUserPin, out firstAccountPin);
                    Console.WriteLine("Enter First Account BSB: ");
                    string? firstUserBsb = Console.ReadLine();
                    int firstAccountBsb;
                    int.TryParse(firstUserBsb, out firstAccountBsb);
                    Console.WriteLine("Enter Second Account Name: ");
                    string? secondAccountName = Console.ReadLine();
                    Console.WriteLine("Enter Second Account Pin: ");
                    string? secondUserPin = Console.ReadLine();
                    int secondAccountPin;
                    int.TryParse(secondUserPin, out secondAccountPin);
                    Console.WriteLine("Enter Second Account BSB: ");
                    string? secondUserBsb = Console.ReadLine();
                    int secondAccountBsb;
                    int.TryParse(secondUserBsb, out secondAccountBsb);
                    Console.WriteLine("Enter Amount To Transfer: ");
                    string? userMoney = Console.ReadLine();
                    int moneyToTransfer;
                    int.TryParse(userMoney, out moneyToTransfer);
                    MoneyMain.TransferMoney(firstAccountName, firstAccountPin, firstAccountBsb, secondAccountName, secondAccountPin, secondAccountBsb, moneyToTransfer);
                }
                else if (Program.frame == 2)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    Console.WriteLine("Enter Account BSB: ");
                    string? userBsb = Console.ReadLine();
                    int accountBsb;
                    int.TryParse(userBsb, out accountBsb);
                    Console.WriteLine("Enter Amount To Withdraw: ");
                    string? userMoney = Console.ReadLine();
                    int moneyToWithdraw;
                    int.TryParse(userMoney, out moneyToWithdraw);
                    MoneyMain.WithdrawMoney(accountName, accountPin, accountBsb, moneyToWithdraw);
                }
                else if (Program.frame == 3)
                {
                    Console.WriteLine("Enter Account Name: ");
                    string? accountName = Console.ReadLine();
                    Console.WriteLine("Enter Account Pin: ");
                    string? userPin = Console.ReadLine();
                    int accountPin;
                    int.TryParse(userPin, out accountPin);
                    Console.WriteLine("Enter Account BSB: ");
                    string? userBsb = Console.ReadLine();
                    int accountBsb;
                    int.TryParse(userBsb, out accountBsb);
                    Console.WriteLine("Enter Amount To Deposit: ");
                    string? userMoney = Console.ReadLine();
                    int moneyToDeposit;
                    int.TryParse(userMoney, out moneyToDeposit);
                    MoneyMain.DepositMoney(accountName, accountPin, accountBsb, moneyToDeposit);
                }
            }
        }
    }
}