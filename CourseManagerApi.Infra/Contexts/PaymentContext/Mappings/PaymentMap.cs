using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.PaymentContext.Mappings;

public class PaymentMap : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalPrice)
            .HasColumnName("TotalPrice")
            .HasColumnType("decimal(18,4)")
            .IsRequired(true);

        builder.Property(x => x.NumberOfInstallments)
            .HasColumnName("NumberOfInstallments")
            .IsRequired(true);

        builder.Property(x => x.PaymentStatus)
            .HasColumnName("PaymentStatus")
            .HasConversion(x => x.ToString(), x => (EPaymentStatus)Enum.Parse(typeof(EPaymentStatus), x))
            .IsRequired(true);
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true);
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true);

        builder.HasOne(x => x.Contract)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(x => x.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Installments)
            .WithOne(x => x.Payment)
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}