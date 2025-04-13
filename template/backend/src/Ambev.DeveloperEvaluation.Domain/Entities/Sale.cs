using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public sealed class Sale : BaseEntity
    {
        public long SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;


        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; } = string.Empty;

       
        public bool IsCancelled { get; private set; }
        public decimal TotalAmount => IsCancelled ? 0 : Items.Sum(i => i.TotalAmount);
        public List<SaleItem> Items { get; private set; } = new();

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}
