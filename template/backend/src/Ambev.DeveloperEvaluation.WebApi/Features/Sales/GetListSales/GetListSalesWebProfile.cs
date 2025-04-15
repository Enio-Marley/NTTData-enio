using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSales
{
    public class GetListSalesWebProfile : Profile
    {
        public GetListSalesWebProfile()
        {
            CreateMap<GetListSalesResult, GetListSaleResponse>(); 
            CreateMap<GetListSaleRequest, GetListSalesCommand>();
            CreateMap<SaleItemResult, SaleItemResponse>();
        }
    }
}
