using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Handler for processing GetSaleCommand requests
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the GetSaleCommand request
        /// </summary>
        /// <param name="request">The GetSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale details if found</returns>
        public async Task<GetSaleResult> Handle(GetSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"sale with sale number {request.SaleNumber} not found");

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
