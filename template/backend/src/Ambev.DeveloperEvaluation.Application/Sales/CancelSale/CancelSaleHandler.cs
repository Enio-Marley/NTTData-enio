using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handler for processing CancelSaleCommand requests
    /// </summary>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CancelSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CancelSaleHandler(ISaleRepository saleRepository, IMapper mapper)
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
        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"sale with sale number {request.SaleNumber} not found");

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return _mapper.Map<CancelSaleResult>(sale);
        }
    }
}
