namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Request model for getting a sale by sale number
    /// </summary>
    public class CancelSaleRequest
    {
        /// <summary>
        /// The sale number identifier of the sale to retrieve
        /// </summary>
        public long SaleNumber { get; set; }
    }
}
