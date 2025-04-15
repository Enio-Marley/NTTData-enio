using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Command for retrieving a sale by their ID
    /// </summary>
    public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of DeleteSaleCommand
        /// </summary>
        /// <param name="id">The unique identifier of the sale to retrieve</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
}
}
