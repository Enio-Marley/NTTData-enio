using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly CreateSaleHandler _handler;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new CreateSaleHandler(_saleRepository, _mapper, _bus);
        }

        /// <summary>
        /// Tests that a valid user creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            var sale = new Sale(command.SaleNumber, command.BranchId, command.BranchName, command.CustomerId, command.CustomerName);
            sale.Items = command.Items.Select(item =>
                                        new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice))
                                        .ToList();

            var result = new CreateSaleResult
            {
                Id = sale.Id,
            };


            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<CreateSaleResult>(sale).Returns(result);

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(sale);

            // When
            var createSaleResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            createSaleResult.Should().NotBeNull();
            createSaleResult.Id.Should().Be(sale.Id);
            await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        }
    }
}
