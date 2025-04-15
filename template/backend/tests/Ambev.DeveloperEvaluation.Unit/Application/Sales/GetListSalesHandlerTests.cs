using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Contains unit tests for the <see cref="GetListSalesHandler"/> class.
    /// </summary>
    public class GetListSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetListSalesHandler _handler;

        public GetListSalesHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetListSalesHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid filter When handling list command Then returns filtered list")]
        public async Task Handle_WithValidFilter_ReturnsFilteredSalesList()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale(1001, Guid.NewGuid(), "Hey Jude", Guid.NewGuid(), "Paul McCartney"),
                new Sale(1002, Guid.NewGuid(), "Imagine", Guid.NewGuid(), "John Lennon")
            };

            _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(sales);

            var command = new GetListSalesCommand
            {
                CustomerName = "Paul McCartney"
            };

            var expectedResult = new List<GetListSalesResult>
            {
                new GetListSalesResult
                {
                    SaleNumber = 1001,
                    CustomerName = "Paul McCartney",
                    BranchName = "Hey Jude"
                }
            };

            _mapper.Map<IEnumerable<GetListSalesResult>>(Arg.Any<IEnumerable<Sale>>())
                .Returns(callInfo =>
                {
                    var inputSales = callInfo.Arg<IEnumerable<Sale>>();
                    return expectedResult.Where(r => inputSales.Any(s => s.SaleNumber == r.SaleNumber));
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().ContainSingle(r => r.CustomerName == "Paul McCartney");
        }
    }
}
