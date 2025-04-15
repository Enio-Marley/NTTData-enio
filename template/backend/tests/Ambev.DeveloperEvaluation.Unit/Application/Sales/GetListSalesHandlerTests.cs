
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using MockQueryable.NSubstitute;
using MockQueryable;

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
                SaleTestData.GenerateValidSale(),
                SaleTestData.GenerateValidSale()
            };

            var mockQueryable = sales.BuildMock().BuildMockDbSet();

            _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(mockQueryable);
            var saleExpected = sales.First();

            var command = new GetListSalesCommand
            {
                CustomerName = saleExpected.CustomerName,
                PageNumber = 1,
                PageSize = 10
            };

            var expectedResult = new List<GetListSalesResult>
            {
                new GetListSalesResult
                {
                    SaleNumber = saleExpected.SaleNumber,
                    CustomerName = saleExpected.CustomerName,
                    BranchName = saleExpected.BranchName,
                }
            };

            _mapper.Map<GetListSalesResult>(Arg.Any<Sale>())
                .Returns(callInfo =>
                {
                    var sale = callInfo.Arg<Sale>();
                    return new GetListSalesResult
                    {
                        SaleNumber = sale.SaleNumber,
                        CustomerName = sale.CustomerName,
                        BranchName = sale.BranchName
                    };
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().ContainSingle(r => r.CustomerName == saleExpected.CustomerName);
        }
    }
}
