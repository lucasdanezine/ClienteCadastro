# ClienteCadastro - Teste Técnico

Projeto de cadastro de clientes com integração ViaCEP, envio de eventos via RabbitMQ e persistência em PostgreSQL. Desenvolvido com .NET 8, Clean Architecture e Docker.

## 🚀 Tecnologias

- .NET 8 (ASP.NET Core Web API)
- PostgreSQL
- RabbitMQ
- Entity Framework Core
- AutoMapper
- Refit (ViaCEP)
- Docker + Docker Compose

## 📦 Estrutura do Projeto

```text
ClienteCadastro
├── ClienteCadastro (API)
├── ClienteCadastro.Application
├── ClienteCadastro.Domain
├── ClienteCadastro.Infrastructure
└── docker-compose.yml

Após clonar o repositório Suba o ambiente:
docker-compose up --build


✅ Comandos úteis

    Gerar migrations:

dotnet ef migrations add NomeMigration --project ClienteCadastro.Infrastructure --startup-project ClienteCadastro

Aplicar migrations:

dotnet ef database update --project ClienteCadastro.Infrastructure --startup-project ClienteCad
