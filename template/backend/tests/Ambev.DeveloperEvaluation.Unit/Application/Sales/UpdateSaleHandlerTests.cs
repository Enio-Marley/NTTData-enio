using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Rebus.Bus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    /// <summary>
    /// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;
        private readonly IBus _bus;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _bus = Substitute.For<IBus>();
            _handler = new UpdateSaleHandler(_saleRepository, _mapper, _bus);
        }

        [Fact(DisplayName = "Given valid update request When handling Then updates sale and returns result")]
        public async Task Handle_ValidUpdateCommand_ReturnsUpdatedSaleResult()
        {
            // Arrange
            var command = new UpdateSaleCommand
            {
                Id = Guid.NewGuid(),
                SaleNumber = 123456,
                BranchId = Guid.NewGuid(),
                BranchName = "Beatles Maniacos",
                CustomerId = Guid.NewGuid(),
                CustomerName = "Integrantes Atualizado",
                Items = new List<UpdateSaleItemCommand>
                {
                    new UpdateSaleItemCommand
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        ProductName = "This boy",
                        Quantity = 5,
                        UnitPrice = 1500
                    }
                }
            };

            var existingSale = new Sale(command.SaleNumber, command.BranchId, command.BranchName, command.CustomerId, command.CustomerName);

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(existingSale);

            _mapper.Map(command, existingSale).Returns(existingSale);
            _mapper.Map<UpdateSaleResult>(existingSale).Returns(new UpdateSaleResult { Id = command.Id });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(command.Id);
            await _saleRepository.Received(1).UpdateAsync(existingSale, Arg.Any<CancellationToken>());
        }
    }
}
