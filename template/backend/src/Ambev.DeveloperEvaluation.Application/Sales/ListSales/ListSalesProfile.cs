using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalSaleAmount));
        }
    }
}
