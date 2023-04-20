using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Orders;

namespace RoutePlanning.Infrastructure.Database.Orders;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Start).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.End).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.User).WithMany().IsRequired(false);

        builder.Property(x => x.TrackingNumber).HasMaxLength(12);
    }
}
