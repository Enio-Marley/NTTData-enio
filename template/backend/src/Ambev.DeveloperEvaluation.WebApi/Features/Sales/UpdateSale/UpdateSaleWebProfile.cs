using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Application and API UpdateSale responses
    /// </summary>
    public class UpdateSaleWebProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale feature
        /// </summary>
        public UpdateSaleWebProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
            CreateMap<SaleItemRequest, UpdateSaleItemCommand>(); 
        }
    }
}
