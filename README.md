# Developer Evaluation Project - DeveloperStore API

Este projeto é uma API desenvolvida para gerir o registo de vendas (Sales) da DeveloperStore. A aplicação foi construída utilizando princípios de **Domain-Driven Design (DDD)**, **Clean Architecture** e o padrão **CQRS**, garantindo uma base de código escalável, testável e de fácil manutenção.

## 🚀 Tecnologias e Padrões Utilizados

- **.NET 8** (C#)
- **Entity Framework Core** (ORM)
- **PostgreSQL** (Base de dados relacional)
- **MediatR** (Implementação do padrão CQRS)
- **AutoMapper** (Mapeamento de objetos)
- **FluentValidation** (Validação de dados na camada de aplicação)
- **xUnit, NSubstitute e Bogus** (Testes unitários e funcionais)
- **Swagger/OpenAPI** (Documentação da API)

## 🏗️ Arquitetura do Projeto

O projeto está dividido nas seguintes camadas principais (Clean Architecture):

- **Domain:** Contém as entidades principais de negócio (`Sale`, `SaleItem`, `User`), Enums, Eventos de Domínio e as interfaces de repositórios.
- **Application:** Contém as regras de negócio orquestradas pelos Handlers do MediatR. Dividida em Commands (Escrita/Alteração) e Queries (Leitura), juntamente com a validação (FluentValidation).
- **ORM (Infrastructure):** Responsável pelo acesso a dados, mapeamento das entidades para o banco de dados via Entity Framework Core e implementações dos repositórios.
- **WebApi:** A camada de apresentação, que expõe os endpoints RESTful através de Controllers e lida com os DTOs de Request e Response.

## ⚙️ Como Executar o Projeto

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker e Docker Compose](https://www.docker.com/) (para rodar a base de dados localmente de forma simples)

### Passos para execução

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd <nome-da-pasta-do-projeto>

2. **Suba a infraestrutura (Base de Dados)**
Na raiz do projeto, execute o docker-compose para iniciar o PostgreSQL:
   ```bash
   docker-compose up -d

3. **Aplique as Migrations**
Navegue até a pasta da API e aplique as migrações para criar as tabelas na base de dados:
   ```bash
   cd src/Ambev.DeveloperEvaluation.WebApi
   dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM

4. **Inicie a Aplicação**
Ainda na pasta WebApi, execute o projeto:
   ```bash
   dotnet run

5. **Acesse a Documentação (Swagger)**
Abra o seu navegador e acesse:
   https://localhost:8080/swagger (A porta será exibida no terminal após o dotnet run).

## 🧠 Decisões Técnicas e Padrões Adotados

Durante o desenvolvimento deste projeto, algumas decisões arquiteturais foram tomadas para alinhar com os requisitos de negócio e boas práticas.

1. Padrão "External Identities" (Identidades Externas)
Como o projeto utiliza DDD, é comum precisarmos de referenciar entidades de outros domínios ou microserviços (como Customer ou Branch). Para evitar o acoplamento direto ou a complexidade de manter várias agregações sincronizadas, utilizámos o padrão External Identities.

- **Como funciona:** Na entidade Sale, guardamos o ID da entidade externa (CustomerId, BranchId) e realizamos a desnormalização da descrição dessa entidade (ex: CustomerName, BranchName).

- **Vantagem:** Isso permite que o domínio de Vendas consulte e exiba informações básicas sem a necessidade de fazer joins complexos ou chamadas síncronas a outras APIs em tempo de execução, aumentando a performance e a resiliência.

2. CQRS com MediatR
A operação de CRUD não interage diretamente com os repositórios através dos Controllers. Em vez disso, a API emite intenções (Commands para alterações e Queries para leituras) que são processadas por Handlers dedicados via MediatR.

- **Vantagem:** Desacoplamento entre a apresentação e as regras de negócio. Cada operação (Create, Update, Get, Delete) tem o seu próprio escopo isolado de validação e execução, cumprindo o Princípio da Responsabilidade Única (SRP).

3. Simulação de Eventos de Domínio (Domain Events)
O domínio de Vendas prevê a publicação de eventos sempre que o estado de uma venda é alterado (ex: SaleCreatedEvent, SaleModifiedEvent, SaleCancelledEvent).

- **Decisão:** Como o uso de um Message Broker real (como RabbitMQ ou Kafka) não era um requisito obrigatório para este protótipo, a publicação dos eventos foi simulada utilizando o ILogger no final de cada operação com sucesso nos Handlers (Application Layer).

- **Vantagem:** Esta abordagem demonstra o entendimento de arquiteturas orientadas a eventos (Event-Driven Architecture) e deixa a estrutura totalmente preparada (plug and play) para a futura injeção de um Message Bus real sem a necessidade de alterar a lógica de negócio principal.

## 🧪 Testes

Para executar a suíte de testes unitários e garantir que as regras de negócio e validações funcionam como esperado, execute o seguinte comando na raiz do projeto:

```bash
   dotnet test
