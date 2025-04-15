using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    /// <summary>
    /// Handler for processing GetListSalesCommand requests
    /// </summary>
    public class GetListSalesHandler : IRequestHandler<GetListSalesCommand, PaginatedList<GetListSalesResult>>
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

        public async Task<PaginatedList<GetListSalesResult>> Handle(GetListSalesCommand request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync(cancellationToken);
            var query = sales.AsQueryable();

            #region Filters
            if (request.SaleNumber.HasValue)
                query = query.Where(s => s.SaleNumber == request.SaleNumber.Value);

            if (!string.IsNullOrWhiteSpace(request.CustomerName))
                query = query.Where(s => s.CustomerName.Contains(request.CustomerName));

            if (!string.IsNullOrWhiteSpace(request.BranchName))
                query = query.Where(s => s.BranchName.Contains(request.BranchName));

            if (request.IsCancelled)
                query = query.Where(s => s.IsCancelled == request.IsCancelled);

            if (request.DateSaleInitial.HasValue)
                query = query.Where(s => s.SaleDate >= request.DateSaleInitial.Value);

            if (request.DateSaleFinal.HasValue)
                query = query.Where(s => s.SaleDate <= request.DateSaleFinal.Value);
            #endregion
            var result = query.Select(s => _mapper.Map<GetListSalesResult>(s)).AsQueryable();

            return await PaginatedList<GetListSalesResult>.CreateAsync(result, request.PageNumber, request.PageSize); ;
        }
    }
}
