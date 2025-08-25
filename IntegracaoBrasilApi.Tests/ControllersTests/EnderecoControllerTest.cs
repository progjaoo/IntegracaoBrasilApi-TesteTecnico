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
        private readonly Mock<IBrasilApiService> _serviceMock = new();
        private readonly Mock<IEnderecoRepository> _repoMock = new();
        private readonly Mock<ILogger<EnderecosController>> _loggerMock = new();

        [Fact]
        public async Task Deve_retornar_endereco_quando_existir()
        {
            // Arrange
            var cep = "27313130"; //ENDERECO DA MINHA CASA :D
            var enderecoMock = new Endereco
            {
                Cep = cep,
                Regiao = "Saudade",
                Cidade = "Barra Mansa",
                Estado = "RJ"
            };

            var brasilApiServiceMock = new Mock<IBrasilApiService>();
            brasilApiServiceMock.Setup(s => s.GetEnderecoByCepAsync(cep))
                                .ReturnsAsync(enderecoMock);

            var enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            enderecoRepositoryMock.Setup(r => r.AddAsync(enderecoMock))
                                  .Returns(Task.CompletedTask);

            var loggerMock = Mock.Of<ILogger<EnderecosController>>();

            var controller = new EnderecosController(
                brasilApiServiceMock.Object,
                enderecoRepositoryMock.Object,
                loggerMock
            );

            // Act
            var result = await controller.GetByCep(cep);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEndereco = Assert.IsType<Endereco>(okResult.Value);

            Assert.Equal(cep, returnedEndereco.Cep);
            Assert.Equal(enderecoMock.Cidade, returnedEndereco.Cidade);
            Assert.Equal(enderecoMock.Estado, returnedEndereco.Estado);

            brasilApiServiceMock.Verify(s => s.GetEnderecoByCepAsync(cep), Times.Once);
            enderecoRepositoryMock.Verify(r => r.AddAsync(enderecoMock), Times.Once);
        }

    }
}
