using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");

            builder.Property(e => e.ContactManager)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contactManager");

            builder.Property(e => e.ContactNumber)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contactNumber");

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country");

            builder.Property(e => e.customer)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("customer");

            builder.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("createAt");

            builder.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("modifiedAt");

            builder.Property(e => e.Status).HasColumnName("status");
        }
    }
}
