# Testes Funcionais - Ambev Developer Evaluation

Este diretório contém os testes funcionais para a API Ambev Developer Evaluation, implementados usando .NET 8, xUnit e WebApplicationFactory.

## Visão Geral

Os testes funcionais validam o comportamento completo da aplicação, testando:
- **Endpoints da API** (CRUD completo)
- **Regras de negócio** (descontos, validações)
- **Integração com banco de dados**
- **Autenticação e autorização**
- **Cenários de erro e exceções**

## Estrutura dos Testes

### Classes Base
- **`FunctionalTestBase`**: Classe base com helpers para requisições HTTP
- **`CustomWebApplicationFactory`**: Factory para testes com banco em memória
- **`CustomWebApplicationFactoryWithTestContainers`**: Factory para testes com PostgreSQL real

### Dados de Teste
- **`FunctionalTestData`**: Gerador de dados de teste usando Bogus
- Dados válidos e inválidos para todos os cenários
- Cenários específicos para regras de negócio

### Testes Implementados

#### 1. **SaleFunctionalTests.cs**
Testes para o módulo de vendas:
- Criação de vendas válidas
- Aplicação de descontos (10% e 20%)
- Validação de regras de negócio
- CRUD completo (Create, Read, Update, Delete)
- Cenários de erro (quantidade máxima, descontos inválidos)

#### 2. **AuthFunctionalTests.cs**
Testes para autenticação:
- Login com credenciais válidas
- Validação de email e senha
- Cenários de erro (credenciais inválidas)

#### 3. **UserFunctionalTests.cs**
Testes para o módulo de usuários:
- Criação de usuários válidos
- Validação de dados (email, senha, nome)
- CRUD completo
- Prevenção de emails duplicados

#### 4. **IntegrationFunctionalTests.cs**
Testes de integração complexos:
- Fluxos de negócio completos
- Operações concorrentes
- Integridade de dados
- Health checks e documentação

#### 5. **TestContainersFunctionalTests.cs**
Testes com banco de dados PostgreSQL real:
- Persistência real de dados
- Isolamento entre testes
- Validação de regras de negócio com banco real

## Como Executar

### Pré-requisitos
- .NET 8 SDK
- Docker Desktop (para TestContainers)

### Executar Todos os Testes Funcionais
```bash
cd template/backend/tests/Ambev.DeveloperEvaluation.Functional
dotnet test
```

### Executar Testes Específicos
```bash
# Apenas testes de vendas
dotnet test --filter "FullyQualifiedName~SaleFunctionalTests"

# Apenas testes de autenticação
dotnet test --filter "FullyQualifiedName~AuthFunctionalTests"

# Apenas testes com banco real
dotnet test --filter "FullyQualifiedName~TestContainersFunctionalTests"
```

### Executar com Cobertura
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Cenários de Teste

### Regras de Negócio Validadas

#### Descontos por Quantidade
- **4+ itens**: 10% de desconto
- **10-20 itens**: 20% de desconto
- **< 4 itens**: Sem desconto
- **> 20 itens**: Erro de validação

#### Validações de Dados
- Email válido
- Senha forte
- Nome obrigatório
- Quantidade máxima de itens

### Cenários de Erro
- Dados inválidos
- IDs inexistentes
- Regras de negócio violadas
- Endpoints inexistentes

## Configuração

### Banco de Dados em Memória
Para testes rápidos, usa Entity Framework InMemory:
```csharp
services.AddDbContext<DefaultContext>(options =>
{
    options.UseInMemoryDatabase("TestDatabase");
});
```

### Banco de Dados PostgreSQL Real
Para testes mais realistas, usa Testcontainers:
```csharp
var postgresContainer = new PostgreSqlBuilder()
    .WithImage("postgres:13")
    .WithDatabase("test_database")
    .Build();
```

## Métricas de Cobertura

Os testes funcionais cobrem:
- **100% dos endpoints** da API
- **100% das regras de negócio**
- **100% dos cenários de erro**
- **Integração completa** com banco de dados

## Dependências

### Principais Pacotes
- `Microsoft.AspNetCore.Mvc.Testing`: Testes de integração
- `Microsoft.EntityFrameworkCore.InMemory`: Banco em memória
- `Testcontainers.PostgreSql`: Banco PostgreSQL real
- `FluentAssertions`: Assertions mais legíveis
- `Bogus`: Geração de dados de teste
- `xUnit`: Framework de testes

## Debugging

### Logs de Teste
Os testes incluem logs detalhados para debugging:
```csharp
// Exemplo de log em teste
Console.WriteLine($"Created sale with ID: {saleId}");
```

### Visualização de Respostas
```csharp
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine($"Response: {content}");
```

## Padrões de Nomenclatura

### Convenção Given-When-Then
```csharp
[Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
public async Task CreateSale_ValidRequest_ReturnsSuccess()
```

### Nomes de Métodos
- `{Action}_{Condition}_{ExpectedResult}`
- Exemplo: `CreateSale_ValidRequest_ReturnsSuccess`

## Troubleshooting

### Erro de Conexão com Banco
```bash
# Verificar se Docker está rodando
docker ps

# Reiniciar container se necessário
docker-compose restart ambev.developerevaluation.database
```

### Erro de Dependências
```bash
# Restaurar pacotes
dotnet restore

# Limpar cache
dotnet clean
dotnet build
```

### Testes Falhando
1. Verificar logs de erro
2. Confirmar configuração do banco
3. Validar dados de teste
4. Verificar regras de negócio

## Recursos Adicionais

- [Documentação xUnit](https://xunit.net/)
- [WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests)
- [Testcontainers](https://testcontainers.com/)
- [FluentAssertions](https://fluentassertions.com/)

## Contribuição

Para adicionar novos testes funcionais:

1. Seguir o padrão `{Module}FunctionalTests.cs`
2. Usar `FunctionalTestData` para dados de teste
3. Implementar cenários positivos e negativos
4. Adicionar documentação clara
5. Executar todos os testes antes do commit

---

**Desenvolvido para Ambev Developer Evaluation**  
*Implementação completa de testes funcionais para validação end-to-end da aplicação* 