using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateSaleCommand</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (existingSale != null)
                throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");

            var sale = new Sale();
            sale = _mapper.Map<Sale>(command);
            sale.Items = command.Items.Select(item =>
                                       new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice))
                                       .ToList();

            var validationResultDomain = sale.Validate();

            if(!validationResultDomain.IsValid)
                throw new ValidationException(validationResult.Errors);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }
    }
}
