using Entities.Dtos.RequestDto;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResponseDto
{
    public class OrderDto
    {   
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }

        public Guid PaymentId { get; set; }
        public ICollection<OrderItemDto>? OrderItems { get; set; } = new List<OrderItemDto>();
    }

}
