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

        builder.Property(x => x.Number)
            .HasColumnName("Number")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(11)
            .IsRequired(true);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.PhoneNumbers)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Ignore(x => x.TenantId);
    }
}