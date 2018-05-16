namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(e => e.UserId);

            builder.HasIndex(e => new {e.UserId, e.BankAccountId, e.CreditCardId}).IsUnique(); //always one will be null(bankAccountId or creditCardId)

            builder.HasOne(e => e.BankAccount)
                .WithOne(pm => pm.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.BankAccountId); 

            builder.HasOne(e => e.CreditCard)
               .WithOne(pm => pm.PaymentMethod)
               .HasForeignKey<PaymentMethod>(e => e.CreditCardId); 
        }
    }
}
