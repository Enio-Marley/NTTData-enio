using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Rebus.Bus;
using static Ambev.DeveloperEvaluation.Domain.Events.SaleRegisteredEvent;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a update instance of UpdateSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="bus">The Rebus instance</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IBus bus)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _bus = bus;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand command
        /// </summary>
        /// <param name="command">The UpdateSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the update operation</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if(sale == null)
                throw new InvalidOperationException($"Sale with identifier {command.Id} not exists");

            sale = _mapper.Map(command, sale);

            var updatedItemIds = command.Items.Select(i => i.Id).ToHashSet();

            sale.Items.RemoveAll(item => !updatedItemIds.Contains(item.Id));

            foreach (var itemCmd in command.Items)
            {
                var existingItem = sale.Items.SingleOrDefault(i => i.Id == itemCmd.Id);
                if (existingItem != null)
                {
                    existingItem.Update(itemCmd.ProductId, itemCmd.ProductName, itemCmd.Quantity, itemCmd.UnitPrice);
                }
                else
                {
                    sale.Items.Add(new SaleItem(itemCmd.ProductId, itemCmd.ProductName, itemCmd.Quantity, itemCmd.UnitPrice));
                }
            }

            await _saleRepository.UpdateAsync(sale, cancellationToken);
            await _bus.Publish(new SaleModified(sale.Id, DateTime.UtcNow));

            var result = _mapper.Map<UpdateSaleResult>(sale);
            return result;
        }
        
    }
}
