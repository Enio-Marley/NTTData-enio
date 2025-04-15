namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Represents the response returned after successfully update a sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the newly update sale,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class CancelSaleResult
    {
        /// <summary>
        /// The sale number identifier of the sale
        /// </summary>
        public long SaleNumber { get; set; }
    }
}
