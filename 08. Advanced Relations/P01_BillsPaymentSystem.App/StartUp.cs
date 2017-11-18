using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.App
{
    public class StartUp
    {
        static void Main()
        {
            var context = new BillsPaymentSystemContext();

            using (context)
            {
                //RestDatabase(context);
                //UserFinanceInfo(context);

                //BankAccountWithdraw(context);
                //BankAccountDeposit(context);

                //CreditCardWithdraw(context);
                //CreditCardDeposit(context);


                // PayBills
                Console.Write("Input userId: ");
                int userId = int.Parse(Console.ReadLine());
                Console.Write("Input bills amount: ");
                decimal billsAmount = decimal.Parse(Console.ReadLine());

                try
                {
                    PayBills(userId, billsAmount, context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void PayBills(int userId, decimal billsAmount, BillsPaymentSystemContext context)
        {

            var userBankAccounts = context.PaymentMethods
                .Include(pm => pm.BankAccount)
                .Where(u => u.UserId == userId && u.Type == PaymentMethodType.BankAccount)
                .Select(u => new
                {
                    u.UserId,
                    BankAccountId = u.BankAccountId,
                    Balance = u.BankAccount.Balance,
                })
                .ToList();

            var userCreditCards = context.PaymentMethods
                .Include(b => b.CreditCard)
                .Where(u => u.UserId == userId && u.Type == PaymentMethodType.CreditCard)
                .Select(u => new
                {
                    u.UserId,
                    u.CreditCardId,
                    LimitLeft = u.CreditCard.LimitLeft
                }).ToList();

            if (userBankAccounts.Count==0 && userCreditCards.Count==0)
            {
                throw new ArgumentException($"No user with id {userId}");
            }

            decimal allBankAccountBalance = userBankAccounts.Sum(a => a.Balance);
            decimal allCreditCardsBalance = userCreditCards.Sum(a => a.LimitLeft);

            decimal allBalance = allBankAccountBalance + allCreditCardsBalance;
            if (billsAmount > allBalance)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            bool isBillsPaid = false;
            foreach (var bankAcc in userBankAccounts.OrderBy(x=>x.BankAccountId))
            {
                var currentAccount = context.BankAccounts.FirstOrDefault(ba=>ba.BankAccountId == bankAcc.BankAccountId);
                if(currentAccount.Balance>billsAmount)
                {
                    currentAccount.Withdraw(billsAmount);
                    isBillsPaid = true;
                }
                else
                {
                    billsAmount = billsAmount - currentAccount.Balance;
                    currentAccount.Withdraw(currentAccount.Balance);
                }

                if (isBillsPaid)
                {
                    context.SaveChanges();
                    return;
                }
            }

            if (!isBillsPaid)
            {
                foreach (var cc in userCreditCards.OrderBy(a=>a.CreditCardId))
                {
                    var currentCard = context.CreditCards.FirstOrDefault(c => c.CreditCardId == cc.CreditCardId);
                    if (currentCard.LimitLeft > billsAmount)
                    {
                        currentCard.Withdraw(billsAmount);
                        isBillsPaid = true;
                    }
                    else
                    {
                        billsAmount = billsAmount - currentCard.LimitLeft;
                        currentCard.Withdraw(currentCard.LimitLeft);
                    }

                    if (isBillsPaid)
                    {
                        context.SaveChanges();
                        return;
                    }
                }
            }
        }

        private static void CreditCardDeposit(BillsPaymentSystemContext context)
        {
            Console.Write($"Input CreditCard Id = ");
            var ccId = int.Parse(Console.ReadLine());
            var creditCard = context.CreditCards.Find(ccId);
            if (creditCard == null)
            {
                throw new ArgumentException($"No creditCard with id {ccId} ");
            }

            Console.Write("Input deposit amount = ");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                creditCard.Deposit(amount);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreditCardWithdraw(BillsPaymentSystemContext context)
        {
            Console.Write($"Input CreditCard Id = ");
            var ccId = int.Parse(Console.ReadLine());
            var creditCard = context.CreditCards.Find(ccId);
            if(creditCard == null)
            {
                throw new ArgumentException($"No creditCard with id {ccId} ");
            }

            Console.Write("Input amount = ");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                creditCard.Withdraw(amount);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void BankAccountDeposit(BillsPaymentSystemContext context)
        {
            Console.Write("BankAccount Id = ");
            var bankAccountId = int.Parse(Console.ReadLine());
            var bankAcc = context.BankAccounts.Find(bankAccountId);

            if (bankAcc == null)
            {
                throw new ArgumentException($"No bankAccount with  id {bankAccountId}");
            }

            Console.Write("Input deposit amount = ");
            var deposit = decimal.Parse(Console.ReadLine());

            try
            {
                bankAcc.Deposit(deposit);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void BankAccountWithdraw(BillsPaymentSystemContext context)
        {
            Console.Write("BankAccount Id = ");
            var bankAccountId = int.Parse(Console.ReadLine());
            var bankAcc = context.BankAccounts.Find(bankAccountId);

            if (bankAcc == null)
            {
                throw new ArgumentException($"No bankAccount with id {bankAccountId}");
            }

            Console.Write("Input amount = ");
            var inputAmount = decimal.Parse(Console.ReadLine());
            try
            {
                bankAcc.Withdraw(inputAmount);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void UserFinanceInfo(BillsPaymentSystemContext context)
        {
            Console.Write("Input User Id = ");
            int inputId = int.Parse(Console.ReadLine());
            using (context)
            {
                try
                {
                    User user = context.Users.Find(inputId);
                    var userBankAccounts = context.PaymentMethods
                        .Include(u => u.BankAccount)
                        .Where(pm => pm.UserId == inputId && pm.Type == PaymentMethodType.BankAccount)
                        .ToList();
                    var userCreditCards = context.PaymentMethods
                         .Include(c => c.CreditCard)
                         .Where(pm => pm.UserId == inputId && pm.Type == PaymentMethodType.CreditCard)
                         .ToList();

                    Console.WriteLine($"User: {user.FirstName} {user.LastName}");
                    if (userBankAccounts.Any())
                    {
                        Console.WriteLine("Bank Accounts:");
                        foreach (var bankAcc in userBankAccounts)
                        {
                            Console.WriteLine($"--ID: {bankAcc.BankAccount.BankAccountId}");
                            Console.WriteLine($"---Balance: {bankAcc.BankAccount.Balance:f2}");
                            Console.WriteLine($"---Bank: {bankAcc.BankAccount.BankName}");
                            Console.WriteLine($"---SWIFT: {bankAcc.BankAccount.SWIFTCode}");
                        }
                    }

                    if (userCreditCards.Any())
                    {
                        Console.WriteLine("Credit Cards:");
                        foreach (var creditCards in userCreditCards)
                        {
                            Console.WriteLine($"--ID: {creditCards.CreditCard.CreditCardId}");
                            Console.WriteLine($"---Limit: {creditCards.CreditCard.Limit:f2}");
                            Console.WriteLine($"---Money Owed: {creditCards.CreditCard.MoneyOwed:f2}");
                            Console.WriteLine($"---Limit Left: {creditCards.CreditCard.LimitLeft:f2}");
                            DateTime expirationDate = creditCards.CreditCard.ExpirationDate;
                            Console.WriteLine($"---Expiration Date: {expirationDate: yyyy'/'MM}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"User with id {inputId} not found! ");
                }
            }
        }

        private static void RestDatabase(BillsPaymentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(BillsPaymentSystemContext context)
        {
            var users = new List<User>
           {
               new User {FirstName = "Pasho",LastName = "Petkov",Email = "p@p.bg",Password ="111"},
               new User {FirstName = "Mery",LastName = "Christmas",Email = "merry@ch.com",Password ="222"},
               new User {FirstName = "Ivan",LastName = "Ivanov",Email = "iv@iv.bg",Password ="147a"},
               new User {FirstName = "Nikola",LastName = "Savov",Email = "n@n.bg",Password ="abc"},
               new User {FirstName = "Katq",LastName = "Minkova",Email = "kMin@p.bg",Password ="katq"},
           };
            context.Users.AddRange(users);
            var creditCards = new List<CreditCard>
            {
                new CreditCard{Limit = 1000, MoneyOwed = 50, ExpirationDate = DateTime.Parse("12/2018")},
                new CreditCard{Limit = 10000, MoneyOwed = 2000, ExpirationDate = DateTime.Parse("10/2025")},
                new CreditCard{Limit = 500, MoneyOwed = 25.40m, ExpirationDate = DateTime.Parse("07/2020")},
                new CreditCard{Limit = 1500, MoneyOwed = 30, ExpirationDate = DateTime.Parse("12/2017")},
                new CreditCard{Limit = 2500, MoneyOwed = 1500, ExpirationDate = DateTime.Parse("10/2018")},
                new CreditCard{Limit = 5000, MoneyOwed = 3125, ExpirationDate = DateTime.Parse("05/2022")},
                new CreditCard{Limit = 3000, MoneyOwed = 100, ExpirationDate = DateTime.Parse("12/2021")},
            };
            context.CreditCards.AddRange(creditCards);

            var bankAccounts = new List<BankAccount>
            {
                new BankAccount { Balance = 2500, BankName = "UniCredit", SWIFTCode = "1234"},
                new BankAccount { Balance = 50, BankName = "RaifaizenBank", SWIFTCode = "0000"},
                new BankAccount { Balance = 10500, BankName = "ExpressBank", SWIFTCode = "4567"},
                new BankAccount { Balance = 500, BankName = "UniCredit", SWIFTCode = "5555"},
                new BankAccount { Balance = 756.55m, BankName = "ExpressBank", SWIFTCode = "7896"},
                new BankAccount { Balance = 1253, BankName = "DSK", SWIFTCode = "2314"},
                new BankAccount { Balance = 900, BankName = "DSK", SWIFTCode = "4567"}
            };
            context.BankAccounts.AddRange(bankAccounts);

            var payments = new List<PaymentMethod>
            {
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[0], BankAccount = bankAccounts[0]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[1], BankAccount = bankAccounts[6]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[2], BankAccount = bankAccounts[5]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[3], BankAccount = bankAccounts[4]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[4], BankAccount = bankAccounts[3]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[4], BankAccount = bankAccounts[2]},
                new PaymentMethod {Type = PaymentMethodType.BankAccount, User = users[3], BankAccount = bankAccounts[1]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[0], CreditCard = creditCards[4]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[1], CreditCard = creditCards[3]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[2], CreditCard = creditCards[2]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[4], CreditCard = creditCards[1]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[4], CreditCard = creditCards[0]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[2], CreditCard = creditCards[5]},
                new PaymentMethod {Type = PaymentMethodType.CreditCard, User = users[2], CreditCard = creditCards[6]},

                //proverka za unikalen userId,creditcardId,bankaAccountId
                //new PaymentMethod { Type = PaymentMethodType.CreditCard, User = users[0], CreditCard = creditCards[4]}
            };
            context.PaymentMethods.AddRange(payments);

            context.SaveChanges();
        }
    }
}
