using IntegracaoWebApi.Controllers;
using IntegracaoWebApi.Core.Entities;
using IntegracaoWebApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IntegracaoWebApi.Tests.ControllersTests
{
    public class EnderecoControllerTest
    {
        private readonly Mock<IEnderecoService> _serviceMock = new();
        private readonly Mock<IEnderecoRepository> _repoMock = new();
        private readonly Mock<ILogger<EnderecosController>> _loggerMock = new();

        [Fact]
        public async Task Deve_retornar_endereco_quando_existir()
        {
            // Arrange
            var cep = "27313130";
            var enderecoMock = new Endereco
            {
                Cep = cep,
                Regiao = "Saudade",
                Cidade = "Barra Mansa",
                Estado = "RJ"
            };

            var enderecoServiceMock = new Mock<IEnderecoService>();
            enderecoServiceMock.Setup(s => s.ImportarPorCep(cep))
                               .ReturnsAsync(enderecoMock);

            var loggerMock = Mock.Of<ILogger<EnderecosController>>();

            var controller = new EnderecosController(
                enderecoServiceMock.Object,
                loggerMock
            );

            // Act
            var result = await controller.ImportarPorCep(cep);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedEndereco = Assert.IsType<Endereco>(createdResult.Value);

            Assert.Equal(cep, returnedEndereco.Cep);
            Assert.Equal(enderecoMock.Cidade, returnedEndereco.Cidade);
            Assert.Equal(enderecoMock.Estado, returnedEndereco.Estado);

            enderecoServiceMock.Verify(s => s.ImportarPorCep(cep), Times.Once);
        }

    }
}
