namespace P01_BillsPaymentSystem.Data.Models
{
    using System;

    public class CreditCard
    {
        private const string NegativeWithdrawExceptionMessage = "Cannot withdraw Negative amount!";
        private const string InsufficientLimitExceptionMessage = "Insufficient limit!";
        private const string NegativeDepositExceptionMessage = "Cannot deposit Negative amount!";
        private const string DepositTooMuchExceptionMessage = "The deposit is bigger than the owed sum!";

        public CreditCard()
        {

        }

        public CreditCard( decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
            this.ExpirationDate = expirationDate;    
        }

        public int CreditCardId { get; set; }
        public decimal Limit { get; private set; }
        public DateTime ExpirationDate { get; set; }
        public decimal MoneyOwed { get; private set; }

        //to ignore not include in database
        public decimal LimitLeft => this.Limit - MoneyOwed;

        //to ignore not include in database
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public void Withdraw(decimal sumToWithdraw)
        {
            if (sumToWithdraw < 0)
            {
                throw new ArgumentException(NegativeWithdrawExceptionMessage);
            }

            if (this.LimitLeft < sumToWithdraw)
            {
                throw new ArgumentException(InsufficientLimitExceptionMessage);
            }

            this.MoneyOwed += sumToWithdraw;
        }

        public void Deposit(decimal sumToDeposit)
        {
            if (sumToDeposit < 0)
            {
                throw new ArgumentException(NegativeDepositExceptionMessage);
            }

            if (this.MoneyOwed < sumToDeposit)
            {
                throw new ArgumentException(string.Format(DepositTooMuchExceptionMessage, this.MoneyOwed));
            }

            this.MoneyOwed -= sumToDeposit;
        }
    }
}
