namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response returned after successfully update a new sale.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the newly update sale,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the newly update sale.
        /// </summary>
        /// <value>A unique identifier that uniquely identifies the update sale in the system.</value>
        public Guid Id { get; set; }
    }
}
