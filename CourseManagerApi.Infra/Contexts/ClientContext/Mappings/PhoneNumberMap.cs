using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.ClientContext.Mappings;

public class PhoneNumberMap : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.ToTable("PhoneNumber");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AreaCode)
            .HasColumnName("AreaCode")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(2)
            .IsRequired(true);

        builder.Property(x => x.Number)
            .HasColumnName("Number")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(9)
            .IsRequired(true);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.PhoneNumbers)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Ignore(x => x.TenantId);
    }
}