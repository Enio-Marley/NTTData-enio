using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Profile for mapping GetSale feature requests to commands
    /// </summary>
    public class GetSaleWebProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetSale feature
        /// </summary>
        public GetSaleWebProfile()
        {
            CreateMap<long, Application.Sales.GetSale.GetSaleCommand>()
            .ConstructUsing(saleNumber => new Application.Sales.GetSale.GetSaleCommand(saleNumber));
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<SaleItemResult, SaleItemResponse>();
        }
    }
}
