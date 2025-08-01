# Ambev Developer Evaluation

Este é um projeto de avaliação de desenvolvedor da Ambev, implementado em .NET 8 com arquitetura limpa (Clean Architecture) e padrão CQRS.

## Pré-requisitos

- .NET 8 SDK
- Docker Desktop
- PostgreSQL (via Docker)

## Configuração Inicial

### 1. Subir o Docker

Antes de executar o projeto, é necessário subir o container do PostgreSQL:

```bash
# Navegar para o diretório do projeto
cd template/backend

# Subir o container do PostgreSQL
docker-compose up -d ambev.developerevaluation.database
```

### 2. Verificar a Porta do PostgreSQL

Após subir o Docker, verifique qual porta foi atribuída ao PostgreSQL:

```bash
# Listar containers em execução
docker ps

# Ver logs do container para identificar a porta
docker logs ambev_developer_evaluation_database
```

### 3. Atualizar Connection String

Baseado na porta atribuída pelo Docker, atualize a connection string no arquivo:

**Arquivo**: `template/backend/src/Ambev.DeveloperEvaluation.WebApi/appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
  }
}
```

**Importante**: Substitua `5432` pela porta correta atribuída pelo Docker.

### 4. Executar Migrations

Após configurar a connection string, execute as migrations para criar as tabelas:

```bash
# Navegar para o projeto WebApi
cd template/backend/src/Ambev.DeveloperEvaluation.WebApi

# Executar migrations
dotnet ef database update
```

## Executando o Projeto

### 1. Compilar o Projeto

```bash
# Navegar para o projeto WebApi
cd template/backend/src/Ambev.DeveloperEvaluation.WebApi

# Compilar o projeto
dotnet build
```

### 2. Executar a API

```bash
# Executar o projeto
dotnet run
```

A API estará disponível em: `https://localhost:7001` ou `http://localhost:5001`

### 3. Acessar o Swagger

Após executar o projeto, acesse o Swagger UI em:
- `https://localhost:7001/swagger`
- `http://localhost:5001/swagger`

## Estrutura do Banco de Dados

### Tabelas Principais

- **Sale**: Vendas do sistema
- **Product**: Produtos disponíveis
- **Product_Sales**: Itens de venda (relacionamento entre Sale e Product)
- **Branch**: Filiais
- **Users**: Usuários do sistema

### Relacionamentos

- `Sale` → `Product_Sales` (1:N)
- `Product` → `Product_Sales` (1:N)
- `Branch` → `Sale` (1:N)
- `Users` → `Sale` (1:N)

## Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM
- **PostgreSQL**: Banco de dados
- **MediatR**: Implementação do padrão CQRS
- **FluentValidation**: Validação de dados
- **AutoMapper**: Mapeamento de objetos
- **Swagger**: Documentação da API
- **JWT**: Autenticação

## Endpoints Principais

### Autenticação
- `POST /api/auth/login` - Login de usuário

### Vendas
- `GET /api/sale` - Listar vendas
- `GET /api/sale/{id}` - Obter venda específica
- `POST /api/sale` - Criar nova venda
- `DELETE /api/sale/{id}` - Deletar venda

### Usuários
- `GET /api/users` - Listar usuários
- `GET /api/users/{id}` - Obter usuário específico
- `POST /api/users` - Criar novo usuário
- `DELETE /api/users/{id}` - Deletar usuário


## Testes

O projeto inclui testes unitários e de integração:

```bash
# Executar todos os testes
dotnet test

# Executar testes de uma categoria específica
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

## Validações

O projeto utiliza FluentValidation para validação de dados:

- Validação automática via ValidationBehavior
- Validação de email, senha e telefone
- Validação de regras de negócio específicas

## Tratamento de Erros

- Middleware global para tratamento de exceções
- Respostas padronizadas da API
- Logs estruturados com Serilog

## Estrutura de Pacotes

### Application Layer
- MediatR para CQRS
- AutoMapper para mapeamentos
- FluentValidation para validações

### Infrastructure Layer
- Entity Framework Core
- PostgreSQL
- Repositórios implementados

### Common Layer
- Extensões de segurança (JWT)
- Extensões de logging
- Validações comuns
- Health checks

## Segurança

- Autenticação JWT
- Hash de senhas com BCrypt
- Validação de tokens
- Middleware de autenticação

## Monitoramento

- Health checks configurados
- Logs estruturados
- Métricas básicas

## Troubleshooting

### Erro de Connection String
Se houver erro de conexão com o banco:
1. Verifique se o Docker está rodando
2. Confirme a porta correta do PostgreSQL
3. Atualize a connection string

### Erro de Migration
Se houver erro ao executar migrations:
```bash
# Remover migrations existentes
dotnet ef migrations remove

# Adicionar migration inicial
dotnet ef migrations add InitialCreate

# Atualizar banco
dotnet ef database update
```

### Erro de Foreign Key
Se houver erro de foreign key constraint:
- Verifique se as tabelas foram criadas corretamente
- Confirme se os relacionamentos estão mapeados adequadamente

## Licença

Este projeto foi desenvolvido para avaliação de desenvolvedor da Ambev.
Bruno Mazzinghy
