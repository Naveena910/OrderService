using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IService
{
    public interface IOrderService
    {
        public ResponseDto CreateOrder(OrderFromCartDto orderFromCartDto,String token);
        public List<OrderDto> GetAllOrders();
        public OrderDto GetOrderByOrderId(Guid orderId);
        public void DeleteOrderByOrderId(Guid orderId);
    }
}
