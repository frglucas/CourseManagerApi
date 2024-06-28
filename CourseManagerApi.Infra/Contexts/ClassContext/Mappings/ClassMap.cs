using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.ClassContext.Mappings;

public class ClassMap : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Class");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Course)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);

        builder.HasOne(x => x.Minister)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);

        builder.OwnsOne(x => x.Name)
            .Property(x => x.Value)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);

        builder.Property(x => x.AddressOrLink)
            .HasColumnName("AddressOrLink")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(512)
            .IsRequired(true);

        builder.Property(x => x.ScheduledDate)
            .HasColumnName("ScheduledDate")
            .IsRequired(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true);
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true);

        builder.Property(x => x.IsOnline)
            .HasColumnName("IsOnline")
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}