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
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}