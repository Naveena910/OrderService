using Contracts.IRepository;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class OrderRepository : IOrderRepository
    {
        public RepositoryContext context;
        public OrderRepository(RepositoryContext repositoryContext)
        {
        context= repositoryContext;
        }
        public void Add(Order  order)
        {
            context.Order.Add(order);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public void AddOrder(IEnumerable<OrderItems> order)
        {
            context.OrderItems.AddRange(order);
        }

        public void AddOrderItems(OrderItems order)
        {
            context.OrderItems.Add(order);
        }
        public List<Order> GetAllOrder(Guid userId)
        {
            return context.Order.Include(o => o.OrderItems).Where(o => o.UserId == userId).ToList();
        }
        public Order GetOrderByOrderId(Guid userId, Guid orderId)
        {
            return context.Order.Include(o => o.OrderItems).FirstOrDefault(o => o.UserId == userId && o.Id == orderId);
        }
        public void DeleteOrderByOrderId(Order order)
        {
            context.Order.Remove(order);
            var q = context.OrderItems.Where(x => x.OrderId == order.Id).ToList();
            context.OrderItems.RemoveRange(q);

        }
        }
    }

