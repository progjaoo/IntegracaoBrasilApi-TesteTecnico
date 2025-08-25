using IntegracaoWebApi.Controllers;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using IntegracaoWebApi.Infrastructure.Data;
using IntegracaoWebApi.Infrastructure.Repositories;
using IntegracaoWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IntegracaoWebApi.Tests.ControllersTests
{
    public class BancoControllerTests()
    {
        private readonly Mock<IBrasilApiService> _serviceMock = new();
        private readonly Mock<IBancoRepository> _repoMock = new();
        private readonly Mock<ILogger<BancoController>> _loggerMock = new();

        [Fact]
        public async Task Deve_retornar_banco_quando_existir()
        {
            // Arrange
            var codigo = 539;
            var bancoMock = new Banco
            {
                Codigo = codigo,
                Nome = "Banco Teste",
                Ispb = "12345678"
            };

            var brasilApiServiceMock = new Mock<IBrasilApiService>();
            brasilApiServiceMock.Setup(s => s.GetBancoByCodeAsync(codigo))
                                .ReturnsAsync(bancoMock);

            var bancoRepositoryMock = new Mock<IBancoRepository>();
            bancoRepositoryMock.Setup(r => r.AddRangeAsync(It.IsAny<List<Banco>>()))
                               .Returns(Task.CompletedTask);

            var loggerMock = Mock.Of<ILogger<BancoController>>();

            var controller = new BancoController(
                brasilApiServiceMock.Object,
                bancoRepositoryMock.Object,
                loggerMock
            );

            // Act
            var result = await controller.GetByCode(codigo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedBanco = Assert.IsType<Banco>(okResult.Value);

            Assert.Equal(codigo, returnedBanco.Codigo);
            Assert.Equal(bancoMock.Nome, returnedBanco.Nome);
            Assert.Equal(bancoMock.Ispb, returnedBanco.Ispb);

            brasilApiServiceMock.Verify(s => s.GetBancoByCodeAsync(codigo), Times.Once);
            bancoRepositoryMock.Verify(r => r.AddRangeAsync(It.Is<List<Banco>>(l => l.Contains(bancoMock))), Times.Once);
        }
    }
}