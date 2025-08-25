# 🚀 Integração Web API com BrasilAPI

## 📖 Visão Geral
Este projeto é uma **API ASP.NET Core versão 8.0** para integração com a **BrasilAPI**, permitindo consultar e importar informações de bancos e endereços (CEPs) para um banco de dados local.  
O projeto foi desenvolvido seguindo a **arquitetura limpa** (Clean Architecture), separando responsabilidades em camadas:

- **IntegracaoWebApi.Core**: Contém as entidades do domínio, interfaces e exceções personalizadas.
- **IntegracaoWebApi.Application**: Contém os serviços e DTOs para manipulação da lógica de negócio.
- **IntegracaoWebApi.Infrastructure**: Contém a implementação de repositórios, configurações do EF Core, autenticação e acesso a dados.
- **IntegracaoWebApi**: Projeto principal da API com controllers, middlewares e configuração de rotas.
- **IntegracaoWebApi.Tests**: Projeto para testes unitários das controllers.

O projeto implementa **autenticação JWT** e tratamento de erros centralizado através de **Middleware** customizado, retornando mensagens claras em casos de exceção.

---

## ✨ Funcionalidades

### 🏦 Bancos:

- Listar todos os bancos disponíveis na BrasilAPI.
- Listar todos os bancos disponíveis no banco local.
- Consultar banco por código no BrasilApi.
- Importar bancos para o banco de dados local.
- Buscar bancos pelo nome aproximado (usando EF.Functions.Like) -> **Parte da pedida de consultas complexas**
- Query SQL pura -> **Parte da pedida de consultas complexas**
  
### 📍 Endereços:

- Listar todos os endereços (CEPs) armazenados localmente.
- Consultar endereço por CEP.
- Importar endereços da BrasilAPI para o banco de dados local.

### 🔐 Autenticação 

- Controle de autenticação e autorização para operações de importação.
- O cadastro de usuários cria automaticamente o role User.
- Operações de importação (POST) exigem autenticação via JWT Bearer Token.
- Se registre e autentique no endpoint de Login, Copie o token e cole no Cabeçalho Authorize com a palavra Bearer + Token Copiado

## 🛡️ Tratamento de Erros
O projeto utiliza Middleware customizado (ErrorHandlingMiddleware) que captura exceções personalizadas:

- NotFoundException: Retorna 404 Not Found.
- ExternalApiException: Retorna 502 Bad Gateway.
- UnauthorizedException: Retorna 401 Unauthorized.

Qualquer outro erro inesperado retorna 500 Internal Server Error.

## 🧪 Testes Unitários
- As controllers possuem testes unitários no projeto IntegracaoWebApi.Tests.

```bash
dotnet test
```

## 🛠️ Instruções de Instalação

### Pré-requisitos

- .NET 8 SDK
- SQL Server (ou outra base compatível)
- Visual Studio 2022 ou VS Code

### Passos

1. **Clone o repositório**

```bash
git clone <URL_DO_REPOSITORIO>
cd IntegracaoWebApi
```

2. No arquivo appsettings.json, configure a string de conexão do seu banco de dados local:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=IntegracaoWebApiDb;Trusted_Connection=True;"
}

3. Aplicar Migrations para criar o banco de dados e todas as tabelas (Bancos, Enderecos, Users

- Rode dotnet ef migrations add InitialCreate no caminho do projeto Infrastructure/Data
- Rode dotnet ef database update

4. Rodar a API

- dotnet run --project IntegracaoWebApi

## 🔗 DOCUMENTAÇÃO DA API EXTERNNA

- https://brasilapi.com.br/
