using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.ClassContext.Mappings;

public class ContractMap : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("Contract");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Class)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);
        
        builder.HasOne(x => x.Client)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);

        builder.HasOne(x => x.Payment)
            .WithOne(x => x.Contract)
            .HasForeignKey<Payment>("ContractId")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true);
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}