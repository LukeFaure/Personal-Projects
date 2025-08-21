using System;
using System.Collections.Generic;
using System.IO;
using AccountData;

namespace BankingMain
{
    public static class Program
    {
        public static List<string> accountNames = new List<string>();
        public static List<int> accountPins = new List<int>();
        public static List<int> accountBsbs = new List<int>();
        public static List<object[]> accounts = new List<object[]>();
        public static void Main(string[] args)
        {
            var acc1 = new AccountData.AccountRecords("Luke", 1234, 060606);
            Console.WriteLine(acc1);
        }
        public static void ViewAccount(string accountName, int accountPin)
        {

        }
    }
}