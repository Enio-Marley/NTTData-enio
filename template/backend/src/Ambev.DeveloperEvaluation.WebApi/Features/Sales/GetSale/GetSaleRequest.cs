namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    /// <summary>
    /// Request model for getting a sale by sale number
    /// </summary>
    public class GetSaleRequest
    {
        /// <summary>
        /// The sale number identifier of the sale to retrieve
        /// </summary>
        public long SaleNumber { get; set; }
    }
}
