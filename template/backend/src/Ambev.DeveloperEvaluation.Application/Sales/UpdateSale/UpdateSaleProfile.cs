using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and UpdateSaleResponse
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSale operation
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleCommand, Sale>().ForMember(dest => dest.Items, opt => opt.Ignore())
                                                .ForMember(dest => dest.SaleNumber, opt => opt.Ignore())
                                                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Sale, UpdateSaleResult>();
        }
    }
}
