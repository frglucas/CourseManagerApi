using CourseManagerApi.Core.Contexts.CourseContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.CourseContext.Mappings;

public class CourseMap : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Name)
            .Property(x => x.Value)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Description)
            .Property(x => x.Value)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(512)
            .IsRequired(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true);

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true);

        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .IsRequired(true);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}