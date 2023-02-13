using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Order : CommonModel
    {
        public Guid UserId { get; set; }

        public Guid AddressId { get; set; }

        public Guid? PaymentId { get; set; }

        public int TotalAmount { get; set; } = 0;

        public ICollection<OrderItems>? OrderItems { get; set; } = new List<OrderItems>();
    }
}
