using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Orders
{
    public class OrdersWithItemsAndOrderingSpecification :BaseSpecification<Order>
    {

        public OrdersWithItemsAndOrderingSpecification(string email):base(order=> order.BuyerEmail==email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification(int id, string buyerEmail) : base(order=> order.id==id && order.BuyerEmail==buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }

    }
}
