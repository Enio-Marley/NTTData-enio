using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Profile for mapping GetSCancelSale feature requests to commands
    /// </summary>
    public class CancelSaleWebProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CancelSale feature
        /// </summary>
        public CancelSaleWebProfile()
        {
            CreateMap<long, Application.Sales.CancelSale.CancelSaleCommand>()
            .ConstructUsing(saleNumber => new Application.Sales.CancelSale.CancelSaleCommand(saleNumber));
            CreateMap<CancelSaleResult, CancelSaleResponse>();
        }
    }
}
