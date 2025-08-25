🚀 Integração Web API com BrasilAPI
📖 Visão Geral
Este projeto é uma API desenvolvida em ASP.NET Core 8.0 que se integra com a BrasilAPI para consultar e importar informações de bancos e endereços (por CEP) para uma base de dados local.

A solução foi estruturada seguindo os princípios da Arquitetura Limpa (Clean Architecture), garantindo a separação de responsabilidades em camadas bem definidas:

IntegracaoWebApi.Core: Contém as entidades de domínio, interfaces e exceções personalizadas.

IntegracaoWebApi.Application: Contém os serviços de aplicação, DTOs e a lógica de negócio.

IntegracaoWebApi.Infrastructure: Implementa o acesso a dados com Entity Framework Core, repositórios, e a lógica de autenticação.

IntegracaoWebApi: Camada de apresentação da API (Controllers), middlewares e configurações de inicialização.

IntegracaoWebApi.Tests: Projeto dedicado aos testes unitários das controllers.

A segurança é garantida por autenticação via JWT Bearer Token, e o tratamento de erros é centralizado através de um Middleware customizado para fornecer respostas claras e consistentes.

✨ Funcionalidades
🏦 Bancos
GET /api/bancos/externo: Lista todos os bancos disponíveis na BrasilAPI.

GET /api/bancos/local: Lista todos os bancos já importados para o banco de dados local.

GET /api/bancos/externo/{codigo}: Consulta um banco específico na BrasilAPI pelo seu código.

POST /api/bancos/importar: Importa todos os bancos da BrasilAPI para o banco de dados local. (Requer autenticação)

📍 Endereços (CEP)
GET /api/enderecos: Lista todos os endereços (CEPs) armazenados localmente.

GET /api/enderecos/{cep}: Consulta um endereço específico na BrasilAPI pelo CEP.

POST /api/enderecos/importar/{cep}: Consulta um CEP na BrasilAPI e o importa para o banco de dados local. (Requer autenticação)

🔐 Autenticação
POST /api/auth/registrar: Registra um novo usuário (com a role "User" padrão).

POST /api/auth/login: Autentica um usuário e retorna um JWT Token.

Autorização: Os endpoints de importação (POST) são protegidos e exigem um JWT Bearer Token válido no cabeçalho Authorization.

🛡️ Tratamento de Erros
A API utiliza um ErrorHandlingMiddleware customizado que captura exceções e retorna os seguintes status codes:

NotFoundException: Retorna 404 Not Found.

ExternalApiException: Retorna 502 Bad Gateway (falha na comunicação com a BrasilAPI).

UnauthorizedException: Retorna 401 Unauthorized.

Qualquer outra exceção não tratada retorna 500 Internal Server Error.

🧪 Testes Unitários
O projeto IntegracaoWebApi.Tests contém testes unitários para as controllers. Para executá-los, utilize o comando:

```
dotnet test
```

🛠️ Instruções de Instalação
Pré-requisitos
.NET 8 SDK

SQL Server (ou outro banco de dados compatível com EF Core)

Visual Studio 2022 ou VS Code

Passos
Clone o repositório:

```git clone <URL_DO_REPOSITORIO>
cd IntegracaoWebApi
Configure a String de Conexão:
```
No arquivo appsettings.json, ajuste a DefaultConnection para apontar para o seu banco de dados:

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=IntegracaoWebApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

Aplique as Migrations:
Para criar o banco de dados e as tabelas, execute os seguintes comandos a partir da pasta raiz do projeto:

Bash

# Cria o arquivo de migration (se ainda não existir na pasta Infrastructure/Data)
```
dotnet ef migrations add InitialCreate
```
# Aplica a migration no banco de dados
```
dotnet ef database update
```
Execute a API:
```
dotnet run --project IntegracaoWebApi
```

🔗 Documentação da API Externa
As consultas externas são realizadas através da BrasilAPI. A documentação completa pode ser encontrada em:

https://brasilapi.com.br/
