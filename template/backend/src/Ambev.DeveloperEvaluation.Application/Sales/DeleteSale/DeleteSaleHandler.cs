using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Rebus.Bus;
using static Ambev.DeveloperEvaluation.Domain.Events.SaleRegisteredEvent;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing GetSaleCommand requests
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of GetSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        public DeleteSaleHandler(ISaleRepository saleRepository, IBus bus)
        {
            _saleRepository = saleRepository;
            _bus = bus;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand command
        /// </summary>
        /// <param name="command">The DeleteSale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>

        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _saleRepository.DeleteAsync(command.Id, cancellationToken);
            await _bus.Publish(new SaleDeleted(command.Id, DateTime.UtcNow));


            if (!success)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            return new DeleteSaleResponse { Success = true };
        }
    }
}
