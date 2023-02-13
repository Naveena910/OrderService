using AutoMapper;
using Entities;
using Entities.Dtos.RequestDto;
using Entities.Dtos.ResponseDto;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Mappers : Profile
    {
        public Mappers()
            {
            CreateMap<OrderFromCartDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItems, OrderItemDto>();
            CreateMap<OrderItems, CartDto>();
            CreateMap<OrderItems, OrderFromCartDto>();
            CreateMap<OrderFromCartDto, OrderItems>();
            CreateMap<CartDto, OrderItems>();
            CreateMap<OrderItems, OrderItemDto>();

        }
    }
}
