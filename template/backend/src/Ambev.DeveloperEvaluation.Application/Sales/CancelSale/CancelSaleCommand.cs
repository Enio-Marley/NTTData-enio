using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Command for retrieving a sale by their ID
    /// </summary>
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public long SaleNumber { get; }

        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="saleNumber">The sale number of the sale to retrieve</param>
        public CancelSaleCommand(long saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}
