# Developer Evaluation Project - DeveloperStore API

This project is an API developed to manage sales records (Sales) for the DeveloperStore. The application was built using **Domain-Driven Design (DDD)** principles, **Clean Architecture**, and the **CQRS** pattern, ensuring a scalable, testable, and easily maintainable codebase.

## 🚀 Technologies and Patterns Used

- **.NET 8** (C#)
- **Entity Framework Core** (ORM)
- **PostgreSQL** (Relational database)
- **MediatR** (CQRS pattern implementation)
- **AutoMapper** (Object mapping)
- **FluentValidation** (Data validation in the application layer)
- **xUnit, NSubstitute, and Bogus** (Unit and functional testing)
- **Swagger/OpenAPI** (API documentation)

## 🏗️ Project Architecture

The project is divided into the following main layers (Clean Architecture):

- **Domain:** Contains the main business entities (`Sale`, `SaleItem`, `User`), Enums, Domain Events, and repository interfaces.
- **Application:** Contains the business rules orchestrated by MediatR Handlers. Divided into Commands (Write/Update) and Queries (Read), along with validation (FluentValidation).
- **ORM (Infrastructure):** Responsible for data access, mapping entities to the database via Entity Framework Core, and repository implementations.
- **WebApi:** The presentation layer, which exposes RESTful endpoints through Controllers and handles Request and Response DTOs.

## ⚙️ How to Run the Project

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker and Docker Compose](https://www.docker.com/) (to easily run the database locally)

### Execution Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd <project-folder-name>

2. **Start the infrastructure (Database)**
At the root of the project, run docker-compose to start PostgreSQL:
   ```bash
   docker-compose up -d

3. **Apply the Migrations**
Navigate to the API folder and apply the migrations to create the database tables:
   ```bash
   cd src/Ambev.DeveloperEvaluation.WebApi
   dotnet ef database update --project ../Ambev.DeveloperEvaluation.ORM

4. **Start the Application**
Still in the WebApi folder, run the project:
   ```bash
   dotnet run

5. **Access the Documentation (Swagger)**
Open your browser and access:
   https://localhost:8081/swagger (A porta será exibida no terminal após o dotnet run).

## 🧠 Technical Decisions and Adopted Patterns

During the development of this project, several architectural decisions were made to align with business requirements and best practices.

1. "External Identities" Pattern
Since the project uses DDD, we often need to reference entities from other domains or microservices (like Customer or Branch). To avoid direct coupling or the complexity of keeping multiple aggregates synchronized, we used the External Identities pattern.

- How it works: In the Sale entity, we store the ID of the external entity (CustomerId, BranchId) and denormalize the description of that entity (e.g., CustomerName, BranchName).

- Advantage: This allows the Sales domain to query and display basic information without needing complex joins or synchronous calls to other APIs at runtime, thereby increasing performance and resilience.

2. CQRS with MediatR
CRUD operations do not interact directly with repositories through Controllers. Instead, the API emits intentions (Commands for changes and Queries for reads) that are processed by dedicated Handlers via MediatR.

- Advantage: Decoupling between presentation and business rules. Each operation (Create, Update, Get, Delete) has its own isolated scope for validation and execution, adhering to the Single Responsibility Principle (SRP).

3. Domain Events Simulation
The Sales domain anticipates publishing events whenever a sale's state changes (e.g., SaleCreatedEvent, SaleModifiedEvent, SaleCancelledEvent).

- Decision: Since using a real Message Broker (like RabbitMQ or Kafka) was not a mandatory requirement for this prototype, event publishing was simulated using ILogger at the end of every successful operation in the Handlers (Application Layer).

- Advantage: This approach demonstrates an understanding of Event-Driven Architecture and leaves the structure fully prepared (plug and play) for future injection of a real Message Bus without needing to alter the core business logic.

## 🧪 Tests

To run the unit test suite and ensure that business rules and validations work as expected, execute the following command at the root of the project:

```bash
   dotnet test
