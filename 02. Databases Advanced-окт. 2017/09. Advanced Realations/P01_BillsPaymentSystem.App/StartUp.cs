namespace P01_BillsPaymentSystem.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;
    using System.Globalization;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                //CreateDatabase(db);
                
                //SeedDatabase(db);

                //PayBills(db);

                TakeUserDetails(db);
            }
        }

        //Pay bills
        private static void PayBills(BillsPaymentSystemContext db)
        {
            var userId = int.Parse(Console.ReadLine());
            var amount = decimal.Parse(Console.ReadLine());

            //With include
            //var userBankAccounts = db.PaymentMethods.Include(u => u.BankAccount)
            //        .Include(e => e.User)
            //        .Where(u => u.UserId == userId)
            //        .Select(b => b.BankAccount)
            //        .OrderBy(u => u.BankAccountId)
            //        .FirstOrDefault();

            //var userCreditCards = db.PaymentMethods.Include(u => u.CreditCard)
            //       .Include(e => e.User)
            //       .Where(u => u.UserId == userId)
            //       .Select(b => b.CreditCard)
            //       .OrderBy(u => u.CreditCardId)
            //       .FirstOrDefault();



            var user = db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    BankAccounts = u.PaymentMethods.Where(pm => pm.Type == Data.Models.Type.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .OrderBy(ba => ba.BankAccountId)
                        .ToArray(),

                    CreditCards = u.PaymentMethods.Where(pm => pm.Type == Data.Models.Type.CreditCard)
                                   .Select(pm => pm.CreditCard)
                                   .OrderBy(cc => cc.CreditCardId)
                                   .ToArray()
                })
                .FirstOrDefault();


            if (user == null)
            {
                Console.WriteLine($"User with id {userId} does not exists");
                return;
            }

            var userAllMoney = user.BankAccounts.Select(b => b.Balance).Sum() + user.CreditCards.Select(c => c.Limit).Sum();
            
            if (userAllMoney < amount)
            {
                Console.WriteLine("Insufficient funds!");
                return;
            }

            var userBankAccountsMoney = user.BankAccounts.Select(b => b.Balance).Sum();

            amount = PayBillsFromBankAccounts(user.BankAccounts, amount, db);


            var userCreditCardsMoney = user.CreditCards.Select(c => c.LimitLeft).Sum();

            if (userCreditCardsMoney < amount)
            {
                Console.WriteLine("Insufficient funds!");
                return;
            }

            if (amount > 0)
            {
                PayBillsWithCreditCards(user.CreditCards, amount, db);
            }
           

            db.SaveChanges();
            Console.WriteLine("Bills are successfully payed.");
        }

        private static void PayBillsWithCreditCards(CreditCard[] creditCards, decimal amount, BillsPaymentSystemContext db)
        {
            foreach (var creditCard in creditCards)
            {
                db.Entry(creditCard).State = EntityState.Modified; //Change all modified fields in CreditCard.
                db.Entry(creditCard).State = EntityState.Unchanged;

                if(creditCard.LimitLeft >= amount)
                {
                    creditCard.Withdraw(amount); return;
                }

                amount -= creditCard.LimitLeft;
                creditCard.Withdraw(creditCard.LimitLeft);
            }
        }

        private static decimal PayBillsFromBankAccounts(BankAccount[] bankAccounts, decimal amount, BillsPaymentSystemContext db)
        {
            foreach (var account in bankAccounts)
            {
                db.Entry(account).State = EntityState.Modified; //change all modified fields in BankAccount.  
                db.Entry(account).State = EntityState.Unchanged;

                if (account.Balance >= amount)
                {
                    account.Withdraw(amount);
                    amount = 0;
                    break;
                }

                amount -= account.Balance;
                account.Withdraw(account.Balance);
            }

            return amount;
        }

        //Take User Details
        private static void TakeUserDetails(BillsPaymentSystemContext db)
        {
            var userId = int.Parse(Console.ReadLine());

            var userInfo = db.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    UserName = $"{u.FirstName} {u.LastName}",
                    BankAccounts = u.PaymentMethods.Where(pm => pm.Type == Data.Models.Type.BankAccount)
                                    .Select(pm => pm.BankAccount)
                                    .ToArray(),
                    CreditCards = u.PaymentMethods.Where(pm => pm.Type == Data.Models.Type.CreditCard)
                                   .Select(pm => pm.CreditCard)
                                   .ToArray()
                })
                .FirstOrDefault();

            
            if(userInfo == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
            }
            else
            {
                Console.WriteLine($"User: {userInfo.UserName}");

                if (userInfo.BankAccounts.Any())
                {
                    Console.WriteLine("Bank Accounts:");
                    foreach (var account in userInfo.BankAccounts)
                    {
                        Console.WriteLine($"-- ID: {account.BankAccountId}");
                        Console.WriteLine($"--- Balance: {account.Balance:f2}");
                        Console.WriteLine($"--- Bank: {account.BankName}");
                        Console.WriteLine($"--- SWIFT: {account.SWIFTCode}");
                    }
                }

                if (userInfo.CreditCards.Any())
                {
                    Console.WriteLine("Credit Cards:");
                    foreach (var creditCard in userInfo.CreditCards)
                    {
                        Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                        Console.WriteLine($"--- Limit: {creditCard.Limit:f2}");
                        Console.WriteLine($"--- Money Owed: {creditCard.MoneyOwed:f2}");
                        Console.WriteLine($"--- Limit Left: {creditCard.LimitLeft:f2}");
                        Console.WriteLine($"--- Expiration Date: {creditCard.ExpirationDate.ToString("MM/yyyy")}");
                    }
                }
            }

        }

        //Seed
        private static void SeedDatabase(BillsPaymentSystemContext db)
        {
            //Users
            var users = new User[]
            {
                new User()
                {
                    FirstName = "Pesho",
                    LastName = "Stamotov",
                    Email = "pesho@abv.bg",
                    Password = "azsympesho"
                },
                 new User()
                {
                    FirstName = "Guy",
                    LastName = "Gilbert",
                    Email = "gilbert@gmail.com",
                    Password = "Gilbert123"
                },
                new User()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "ivanIvanov@abv.bg",
                    Password = "Ivanov12345"
                }
            };

            db.Users.AddRange(users);

            //Credit Cards
            var rnd= new Random();
            var creditCards = new CreditCard[]
            {
                new CreditCard(100 * rnd.Next(10, 20),  100m * rnd.Next(1, 10), DateTime.Now.AddMonths(rnd.Next(20))),
                new CreditCard(100 * rnd.Next(1, 20), 100m * rnd.Next(1, 10), DateTime.Now.AddMonths(rnd.Next(20))),
                new CreditCard(100 * rnd.Next(1, 20), 100m * rnd.Next(1, 10), DateTime.Now.AddMonths(rnd.Next(20))),
                new CreditCard(100 * rnd.Next(1, 20), 100m * rnd.Next(1, 10), DateTime.Now.AddMonths(rnd.Next(20))),
                new CreditCard(100 * rnd.Next(1, 20), 100m * rnd.Next(1, 10), DateTime.Now.AddMonths(rnd.Next(20)))
            };
            
            db.CreditCards.AddRange(creditCards);

            //Bank Accounts
            var bankAccounts = new BankAccount[]
            {
                new BankAccount(1500m, "Swiss bank","SSWSSBANK"),

                new BankAccount(2000m,"Unicredit Bulbank","UNCRBGSF"),
               
                new BankAccount(1000m,"First Investment Bank","FINVBGSF"),
                
                new BankAccount(40000m,"Swiss bank","SSWSSBANK"),
              
                new BankAccount(60000m, "Swiss bank","SSWSSBANK")
            };

            db.BankAccounts.AddRange(bankAccounts);


            //Payment Methods
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    User = users[0],
                    CreditCard = creditCards[0],
                    Type = Data.Models.Type.CreditCard
                },
                new PaymentMethod()
                {
                    User = users[0],
                    CreditCard = creditCards[1],
                    Type = Data.Models.Type.CreditCard
                },
                new PaymentMethod()
                {
                    User = users[0],
                    BankAccount = bankAccounts[0],
                    Type = Data.Models.Type.BankAccount
                },
                new PaymentMethod()
                {
                    User = users[1],
                    BankAccount = bankAccounts[1],
                    Type = Data.Models.Type.BankAccount
                },
                new PaymentMethod()
                {
                    User = users[1],
                    BankAccount = bankAccounts[2],
                    Type = Data.Models.Type.BankAccount
                },
                new PaymentMethod()
                {
                    User = users[1],
                    CreditCard = creditCards[2],
                    Type = Data.Models.Type.CreditCard
                },
                new PaymentMethod()
                {
                    User = users[2],
                    BankAccount = bankAccounts[3],
                    Type = Data.Models.Type.BankAccount
                },
                new PaymentMethod()
                {
                    User = users[2],
                    CreditCard = creditCards[3],
                    Type = Data.Models.Type.CreditCard
                },
                new PaymentMethod()
                {
                    User = users[2],
                    CreditCard = creditCards[4],
                    Type = Data.Models.Type.CreditCard
                },
            };

            db.PaymentMethods.AddRange(paymentMethods);


            db.SaveChanges(); //Save all changes
        }

        //Create Database
        private static void CreateDatabase(BillsPaymentSystemContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.Migrate();
        }
    }
}
