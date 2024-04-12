using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.ClientContext.Mappings;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Street)
            .HasColumnName("Street")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(x => x.Number)
            .HasColumnName("AddressNumber")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(10)
            .IsRequired(true);
        
        builder.Property(x => x.Neighborhood)
            .HasColumnName("Neighborhood")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(x => x.State)
            .HasColumnName("State")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(x => x.Country)
            .HasColumnName("Country")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(x => x.ZipCode)
            .HasColumnName("ZipCode")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(8)
            .IsRequired(false);

        builder.Property(x => x.AddOnAddress)
            .HasColumnName("AddOnAddress")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne(x => x.Client)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Ignore(x => x.TenantId);
    }
}