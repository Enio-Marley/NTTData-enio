using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleRegisteredEvent
    {
        public Sale Sale { get; }

        public SaleRegisteredEvent(Sale sale)
        {
            Sale = sale;
        }
        public record SaleCreated(Guid SaleId, long SaleNumber, DateTime CreatedAt);
        public record SaleModified(Guid SaleId, DateTime ModifiedAt);
        public record SaleCancelled(Guid SaleId, DateTime CancelledAt);
        public record SaleDeleted(Guid SaleId, DateTime DeletedAt);
    }
}
