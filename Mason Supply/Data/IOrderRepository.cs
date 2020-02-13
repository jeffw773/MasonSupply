using System.Linq;
using Mason_Supply.Models;
using System.Collections.Generic;

namespace Mason_Supply.Data
{
    public interface IOrderRepository
    {
        List<Order> Orders { get; }
        void AddOrder(Order order);
        void AddShape(Order order, Shape shape);
        //Order GetPostByAppUser(string user);
        Order GetOrderByID(int ID);
    }
}
