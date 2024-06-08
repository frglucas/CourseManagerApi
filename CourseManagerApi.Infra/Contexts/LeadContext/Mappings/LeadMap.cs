using CourseManagerApi.Core.Contexts.LeadContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.LeadContext.Mappings;

public class LeadMap : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.ToTable("Lead");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired(false);

        builder.OwnsOne(x => x.Name)
            .Property(x => x.Value)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);

        builder.OwnsOne(x => x.PhoneNumber)
            .Property(x => x.AreaCode)
            .HasColumnName("AreaCode")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(2)
            .IsRequired(false);

        builder.OwnsOne(x => x.PhoneNumber)
            .Property(x => x.Number)
            .HasColumnName("PhoneNumber")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(9)
            .IsRequired(false);

        builder.Property(x => x.Observation)
            .HasColumnName("Observation")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.IsAdhered)
            .HasColumnName("IsAdhered")
            .IsRequired(true);

        builder.HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}