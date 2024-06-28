using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.PaymentContext.Mappings;

public class InstallmentMap : IEntityTypeConfiguration<Installment>
{
    public void Configure(EntityTypeBuilder<Installment> builder)
    {
        builder.ToTable("Installment");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Money)
            .HasColumnName("Money")
            .HasColumnType("decimal(18,4)")
            .IsRequired(true);

        builder.Property(x => x.DueDate)
            .HasColumnName("DueDate")
            .IsRequired(true);

        builder.Property(x => x.PaymentStatus)
            .HasColumnName("PaymentStatus")
            .HasConversion(x => x.ToString(), x => (EPaymentStatus)Enum.Parse(typeof(EPaymentStatus), x))
            .IsRequired(true);
        
        builder.Property(x => x.PaymentMethod)
            .HasColumnName("PaymentMethod")
            .HasConversion(x => x.ToString(), x => (EPaymentMethod)Enum.Parse(typeof(EPaymentMethod), x))
            .IsRequired(true);

        builder.HasOne(x => x.Payment)
            .WithMany(x => x.Installments)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}