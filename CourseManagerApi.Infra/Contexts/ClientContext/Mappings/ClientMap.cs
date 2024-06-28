using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagerApi.Infra.Contexts.ClientContext.Mappings;

public class ClientMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired(true);

        builder.OwnsOne(x => x.Name)
            .Property(x => x.FullName)
            .HasColumnName("FullName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);

        builder.OwnsOne(x => x.Name)
            .Property(x => x.BadgeName)
            .HasColumnName("BadgeName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);

        builder.OwnsOne(x => x.Document)
            .Property(x => x.Number)
            .HasColumnName("DocumentNumber")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(14)
            .IsRequired(true);
        
        builder.OwnsOne(x => x.Document)
            .Property(x => x.HashedNumber)
            .HasColumnName("HashedDocumentNumber")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(256)
            .IsRequired(true);

        builder.OwnsOne(x => x.Document)
            .Property(x => x.Type)
            .HasColumnName("DocumentType")
            .HasConversion(x => x.ToString(), x => (EDocumentType)Enum.Parse(typeof(EDocumentType), x))
            .IsRequired(true);

        builder.OwnsOne(x => x.Gender)
            .Property(x => x.Type)
            .HasColumnName("GenderType")
            .HasConversion(x => x.ToString(), x => (EGenderType)Enum.Parse(typeof(EGenderType), x))
            .IsRequired(true);

        builder.OwnsOne(x => x.Gender)
            .Property(x => x.Detail)
            .HasColumnName("GenderDetail")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired(true);

        builder.Property(x => x.BirthDate)
            .HasColumnName("BirthDate")
            .IsRequired(true);

        builder.Property(x => x.Observation)
            .HasColumnName("Observation")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(512)
            .IsRequired(false);
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired(true);

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired(true);

        builder.Property(x => x.IsSmoker)
            .HasColumnName("IsSmoker")
            .IsRequired(true);

        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .IsRequired(true);
        
        builder.HasOne(x => x.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);
        
        builder.HasOne(x => x.Captivator)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired(true);
        
        builder.HasOne(x => x.Indicator)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull)
            .IsRequired(false);

        builder.HasOne(x => x.Occupation)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasMany(x => x.Addresses)
            .WithOne(x => x.Client)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Tenant)
            .WithMany()
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}