namespace P01_BillsPaymentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class BankAccount
    {
        private const string InsufficientFundsExceptionMessage = "Insufficient funds!";
        private const string NegativeWithdrawExceptionMessage = "Cannot withdraw Negative amount!";
        private const string NegativeDepositExceptionMessage = "Cannot deposit Negative amount!";

        public BankAccount()
        {

        }

        public BankAccount(decimal balance, string bankName,string swiftCode)
        {
            this.Balance = balance;
            this.BankName = bankName;
            this.SWIFTCode = swiftCode;
        }

        public int BankAccountId { get; set; }
        public decimal Balance { get; private set; }
        public string BankName { get; set; }
        public string SWIFTCode { get; set; }


        //to ignore not include in database
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        public void Withdraw(decimal sumToWithdraw)
        {
            if (sumToWithdraw > this.Balance)
            {
                throw new ArgumentException(InsufficientFundsExceptionMessage);
            }

            if (sumToWithdraw < 0)
            {
                throw new ArgumentException(NegativeWithdrawExceptionMessage);
            }

            this.Balance -= sumToWithdraw;

        }

        public void Deposit(decimal sumToDeposit)
        {
            if (sumToDeposit < 0)
            {
                throw new ArgumentException(NegativeDepositExceptionMessage);
            }

            this.Balance += sumToDeposit;
        }
    }
}
