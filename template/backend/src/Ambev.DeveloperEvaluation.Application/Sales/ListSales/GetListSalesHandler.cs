using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    /// <summary>
    /// Handler for processing GetListSalesCommand requests
    /// </summary>
    public class GetListSalesHandler : IRequestHandler<GetListSalesCommand, IEnumerable<GetListSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetListSalesHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<IEnumerable<GetListSalesResult>> Handle(GetListSalesCommand request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync(cancellationToken);

            #region Filters
            if (request.SaleNumber.HasValue)
                sales = sales.Where(s => s.SaleNumber == request.SaleNumber.Value);

            if (!string.IsNullOrWhiteSpace(request.CustomerName))
                sales = sales.Where(s => s.CustomerName.Contains(request.CustomerName));

            if (!string.IsNullOrWhiteSpace(request.BranchName))
                sales = sales.Where(s => s.BranchName.Contains(request.BranchName));

            if (request.IsCancelled)
                sales = sales.Where(s => s.IsCancelled == request.IsCancelled);

            if (request.DateSaleInitial.HasValue)
                sales = sales.Where(s => s.SaleDate >= request.DateSaleInitial.Value);

            if (request.DateSaleFinal.HasValue)
                sales = sales.Where(s => s.SaleDate <= request.DateSaleFinal.Value);
            #endregion
            

            var result = _mapper.Map<IEnumerable<GetListSalesResult>>(sales);

            return result;
        }
    }
}
