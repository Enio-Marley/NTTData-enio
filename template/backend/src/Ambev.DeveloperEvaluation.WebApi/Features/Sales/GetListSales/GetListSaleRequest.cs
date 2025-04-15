﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSales
{
    public class GetListSaleRequest
    {
        /// <summary>
        /// The unique identifier of the sale to retrieve
        /// </summary>
        public long? SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer name for the sale.
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the branch name for the sale.
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the initial date of the sale filter.
        /// </summary>
        public DateTime? DateSaleInitial { get; set; }

        /// <summary>
        /// Gets or sets the final date of the sale filter.
        /// </summary>
        public DateTime? DateSaleFinal { get; set; }
    }
}
