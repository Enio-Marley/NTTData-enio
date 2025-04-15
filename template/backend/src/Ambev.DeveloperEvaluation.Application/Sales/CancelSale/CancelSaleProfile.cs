using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Profile for mapping between Sale entity and CreateSaleResponse
    /// </summary>
    public class CancelSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateSale operation
        /// </summary>
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleCommand, Sale>();
            CreateMap<Sale, CancelSaleResult>();
        }
    }
}
