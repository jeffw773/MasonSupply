using Mason_Supply.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Mason_Supply.Data
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public List<Order> Orders
        {
            //get { return context.Orders.ToList(); }
            get { return context.Orders.Include(o => o.ShapeList).ToList(); }
        }

        public OrderRepository(ApplicationDbContext appContext)
        {
            context = appContext;
        }

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }


        public void AddShape(Order order, Shape shape)
        {
            order.AddOrderShape(shape);
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public Order GetOrderByID(int ID)
        {
            Order order;
            order = context.Orders.First(id => id.OrderID == ID);
            return order;
        }
    }
}

