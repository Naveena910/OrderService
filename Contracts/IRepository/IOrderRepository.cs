using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public void AddOrder(IEnumerable<OrderItems> order);
        public void SaveChanges();
        public void AddOrderItems(OrderItems order);
        public List<Order> GetAllOrder(Guid userId);
        public Order GetOrderByOrderId(Guid userId, Guid orderId);
        //public void DeleteOrderByOrderId(Guid orderId);
        public void DeleteOrderByOrderId(Order order);
    }
}
