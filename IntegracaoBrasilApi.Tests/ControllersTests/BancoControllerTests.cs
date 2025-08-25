using IntegracaoWebApi.Controllers;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IntegracaoWebApi.Tests.ControllersTests
{
    public class BancoControllerTests()
    {
        private readonly Mock<IBancoService> _serviceMock = new();
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
                Nome = "SANTINVEST S.A. - CFI",
                Ispb = "00122327"
            };

            var bancoServiceMock = new Mock<IBancoService>();
            bancoServiceMock.Setup(s => s.ImportarBancoPorCodigo(codigo))
                            .ReturnsAsync(bancoMock);

            var loggerMock = Mock.Of<ILogger<BancoController>>();

            var controller = new BancoController(
                bancoServiceMock.Object,
                loggerMock
            );

            // Act
            var result = await controller.ImportarPorCode(codigo);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedBanco = Assert.IsType<Banco>(createdResult.Value);

            Assert.Equal(codigo, returnedBanco.Codigo);
            Assert.Equal(bancoMock.Nome, returnedBanco.Nome);
            Assert.Equal(bancoMock.Ispb, returnedBanco.Ispb);

            bancoServiceMock.Verify(s => s.ImportarBancoPorCodigo(codigo), Times.Once);
        }
    }
}