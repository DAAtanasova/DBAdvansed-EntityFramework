using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(p => p.CreditCardId);

            builder.Property(l => l.Limit)
                .IsRequired();

            builder.Property(mo => mo.MoneyOwed)
                .IsRequired();

            builder.Ignore(l => l.LimitLeft);
            builder.Ignore(l => l.PaymentMethodId);

            builder.Property(d => d.ExpirationDate)
                .IsRequired();
        }
    }
}
