namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_BillsPaymentSystem.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Ignore(e => e.PaymentMethodId);

            builder.Property(e => e.BankName)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(50);

            builder.Property(e => e.SWIFTCode)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.Property(e => e.BankName)
                .IsRequired();
        }
    }
}
