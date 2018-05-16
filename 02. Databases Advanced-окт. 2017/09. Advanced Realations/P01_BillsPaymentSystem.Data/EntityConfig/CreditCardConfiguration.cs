namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
   
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(e => e.CreditCardId);

            builder.Ignore(e => e.LimitLeft);

            builder.Ignore(e => e.PaymentMethodId);

            builder.Property(e => e.Limit)
                .IsRequired();

            builder.Property(e => e.MoneyOwed)
                .IsRequired();

            builder.Property(e => e.ExpirationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
