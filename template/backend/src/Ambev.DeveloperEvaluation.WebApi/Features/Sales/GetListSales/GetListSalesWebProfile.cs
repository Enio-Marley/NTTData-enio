using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSales
{
    public class GetListSalesWebProfile : Profile
    {
        public GetListSalesWebProfile()
        {
            CreateMap<GetListSalesResult, GetListSaleResponse>().ReverseMap(); 
            CreateMap<GetListSaleRequest, GetListSalesCommand>().ReverseMap();
            CreateMap<SaleItemResult, SaleItemResponse>().ReverseMap();
        }
    }
}
