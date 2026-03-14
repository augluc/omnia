using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Perfil do AutoMapper para converter a entidade do domínio no resultado da aplicação.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<Sale, CreateSaleResult>();
        }
    }
}
