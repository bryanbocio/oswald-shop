using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShipToAddress, address =>
            {
                address.WithOwner();
            });

            builder.Navigation(order => order.ShipToAddress).IsRequired();

            builder.Property(order => order.Status).HasConversion(
                        orderStatus => orderStatus.ToString(),
                        orderStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), orderStatus)
                    );


            builder.HasMany(order=>order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
