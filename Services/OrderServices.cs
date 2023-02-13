using AutoMapper;
using Contracts.IRepository;
using Contracts.IService;
using Entities;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using log4net.Core;
using Services.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class OrderServices:IOrderService
    {
        private readonly IServices _services;
        private ProductClient _ProductClient;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderrepository;
        public OrderServices(IServices services,ProductClient productClient,IMapper mapper,IOrderRepository repository)
        {
            _services= services;
            _ProductClient = productClient;
            _mapper = mapper;
            _orderrepository = repository;
        }
        public ResponseDto CreateOrder(OrderFromCartDto orderFromCartDto,string token)
        {
             Guid userId= _services.Usercheck();
            _ProductClient.GetAddress(userId,orderFromCartDto.AddressId,token);
            _ProductClient.GetPayment(userId,orderFromCartDto.PaymentId,token);
            if (orderFromCartDto.ProductId != null)
            {
                ProductDto productDto= _ProductClient.GetProductId(orderFromCartDto.ProductId,token);
                Order order = _mapper.Map<Order>(orderFromCartDto);
                order.UserId = userId;
                order.TotalAmount = productDto.Price;
                _orderrepository.Add(order);
                OrderItems items = new OrderItems();
                items.ProductId = (Guid)orderFromCartDto.ProductId;
                items.Quantity = (int)orderFromCartDto.Quantity;
                items.OrderId = order.Id;
                _orderrepository.AddOrderItems(items);
            }
            if(orderFromCartDto.ProductId==null)
            {

               IEnumerable<CartDto> cartItems = _ProductClient.GetCartItemsAsync(userId,token);
                Order order = _mapper.Map<Order>(orderFromCartDto);
                order.UserId = userId;
               
                List<OrderItems> orderItems = new List<OrderItems>();
                foreach(CartDto items in cartItems)
                {
                    OrderItems Orderitem = new OrderItems();
                    if(orderFromCartDto.ProductId==null)
                    {
                        ProductDto productDto = _ProductClient.GetProductId(items.ProductId,token);
                        order.TotalAmount = productDto.Price;
                        Orderitem.ProductId = items.ProductId;

                    }
                    Orderitem.OrderId = order.Id;
                    Orderitem.Quantity = items.Quantity;
                    orderItems.Add(Orderitem);
                }
                _orderrepository.Add(order);
                _orderrepository.AddOrder(orderItems);
                _ProductClient.DeleteCart(userId,token);
                _orderrepository.SaveChanges();
                return new ResponseDto { Id = order.Id };
            }
            return null;

        }
        public List<OrderDto>GetAllOrders()
        {
            Guid userId = _services.Usercheck();
            List<Order> orderDtos = _orderrepository.GetAllOrder(userId);
            if (orderDtos == null)
                throw new NotFoundException("No orders found ");
            return _mapper.Map<List<OrderDto>>(orderDtos);

        }
        public OrderDto GetOrderByOrderId(Guid orderId)
        {
            Guid userId = _services.Usercheck();

            Order order =_orderrepository.GetOrderByOrderId(userId, orderId);

            if (order == null)
            {
                throw new NotFoundException("No order found with this id");
            }

            return _mapper.Map<OrderDto>(order);
        }
        public void DeleteOrderByOrderId(Guid orderId)
        {
            Guid userId = _services.Usercheck();
            Order order = _orderrepository.GetOrderByOrderId(userId, orderId);

            if (order == null)
            {
                throw new NotFoundException("No order found with this id");
            }
            _orderrepository.DeleteOrderByOrderId(order);
            _orderrepository.SaveChanges();

           

          
        }
    }
}
