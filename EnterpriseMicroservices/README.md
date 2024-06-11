
# EnterpriseMicroservices

## Descrição do Projeto

O projeto EnterpriseMicroservices é uma aplicação composta por três micro-serviços: `UserService`, `OrderService` e `NotificationService`. Cada micro-serviço foi desenvolvido utilizando .NET 8, seguindo as melhores práticas de arquitetura de software, incluindo princípios de SOLID, Clean Code, TDD (Test-Driven Development) e integração com RabbitMQ e Docker.

## Micro-serviços

### UserService

**Descrição:** O `UserService` é responsável pela gestão de usuários dentro do sistema. Ele oferece APIs para criar, atualizar, deletar e consultar informações de usuários.

**Tecnologias Utilizadas:**
- .NET 8
- Entity Framework Core
- SQL Server
- Docker

### OrderService

**Descrição:** O `OrderService` gerencia os pedidos feitos pelos usuários. Ele permite criar, atualizar, deletar e consultar pedidos. Além disso, publica mensagens no RabbitMQ para notificar outros serviços sobre novos pedidos.

**Tecnologias Utilizadas:**
- .NET 8
- Entity Framework Core
- SQL Server
- RabbitMQ
- Docker

### NotificationService

**Descrição:** O `NotificationService` consome mensagens do RabbitMQ enviadas pelo `OrderService` e processa notificações relacionadas aos pedidos. Este serviço exemplifica o uso de comunicação assíncrona entre micro-serviços.

**Tecnologias Utilizadas:**
- .NET 8
- RabbitMQ
- Docker

## Tecnologias e Ferramentas Utilizadas

### .NET 8

Utilizado como framework principal para o desenvolvimento dos micro-serviços, proporcionando alta performance, segurança e facilidade de desenvolvimento com C#.

### Entity Framework Core

Utilizado para mapeamento objeto-relacional (ORM), facilitando a interação com bancos de dados relacionais (SQL Server).

### RabbitMQ

Utilizado para comunicação assíncrona entre micro-serviços através de mensagens, garantindo a escalabilidade e resiliência da aplicação.

### Docker

Utilizado para containerização dos micro-serviços, proporcionando um ambiente consistente e fácil de distribuir.

### Test-Driven Development (TDD)

Prática de desenvolvimento onde os testes são escritos antes do código funcional, garantindo a qualidade e robustez do software desde o início.

### Arquitetura de Micro-serviços

A arquitetura de micro-serviços foi adotada para dividir a aplicação em serviços pequenos, independentes e focados em um único propósito. Isso facilita a escalabilidade, manutenção e desenvolvimento de novas funcionalidades.


## Configuração e Execução

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

### Passos para Configuração

1. **Clone o Repositório**

   ```bash
   git clone https://github.com/seu-usuario/EnterpriseMicroservices.git
   cd EnterpriseMicroservices
   ```

2. **Executar RabbitMQ com Docker**

   ```bash
   docker-compose up -d
   ```

3. **Executar os Micro-serviços**

   Navegue até o diretório de cada micro-serviço e execute o comando:

   ```bash
   dotnet run
   ```

   Exemplo para o `UserService`:

   ```bash
   cd UserService
   dotnet run
   ```

### Testes

1. **Executar os Testes do OrderService**

   Navegue até o diretório do projeto de testes `OrderService.Tests` e execute:

   ```bash
   cd OrderService.Tests
   dotnet test
   ```

## TDD (Test-Driven Development)

Os testes foram escritos antes do código funcional para garantir a qualidade e robustez dos micro-serviços. Utilizei xUnit para escrever e executar os testes. 

Exemplo de um teste no `OrderService`:

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;
using OrderService.Repositories;
using Xunit;

namespace OrderService.Tests.Repositories
{
    public class InMemoryOrderRepositoryTests
    {
        private readonly InMemoryOrderRepository _repository;

        public InMemoryOrderRepositoryTests()
        {
            _repository = new InMemoryOrderRepository();
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            var order = new Order
            {
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            var orders = await _repository.GetAllOrdersAsync();

            Assert.Single(orders);
            Assert.Equal(order.Product, orders.First().Product);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            var retrievedOrder = await _repository.GetOrderByIdAsync(1);

            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.Product, retrievedOrder.Product);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldRemoveOrder()
        {
            var order = new Order
            {
                Id = 1,
                UserId = 1,
                Product = "Test Product",
                Quantity = 2,
                OrderDate = DateTime.UtcNow
            };

            await _repository.AddOrderAsync(order);
            await _repository.DeleteOrderAsync(1);
            var orders = await _repository.GetAllOrdersAsync();

            Assert.Empty(orders);
        }
    }
}
```

## Contribuição

Se você deseja contribuir com este projeto, por favor siga os passos abaixo:

1. Fork o repositório
2. Crie uma branch com a nova funcionalidade (`git checkout -b nova-funcionalidade`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova funcionalidade'`)
4. Push para a branch (`git push origin nova-funcionalidade`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

Se precisar de mais alguma coisa, estou à disposição!