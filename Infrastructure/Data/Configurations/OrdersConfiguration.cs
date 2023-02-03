using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {

            builder.ToTable("orders");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");

            builder.Property(e => e.CustomerId).HasColumnName("customerId");

            builder.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");

            builder.Property(e => e.ShipAddress)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shipAddress");

            builder.Property(e => e.ShipAddress2)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shipAddress2");

            builder.Property(e => e.Shipped).HasColumnName("shipped");

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");

            builder.Property(e => e.Zip)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("zip");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_customers");
        }
    }
}
