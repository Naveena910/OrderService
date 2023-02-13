using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.RequestDto
{
    public class OrderFromCartDto
    {
        public Guid AddressId { get; set; }

        public Guid PaymentId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }

    }
}
