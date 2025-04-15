using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class GetListSalesProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ListSales operation
        /// </summary>
        public GetListSalesProfile()
        {
            CreateMap<Sale, GetListSalesResult>();
            CreateMap<SaleItem, SaleItemResult>();
        }
    }
}
