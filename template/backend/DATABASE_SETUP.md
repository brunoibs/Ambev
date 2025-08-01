# Configuração do Banco de Dados PostgreSQL

## Configurações Atualizadas

### Dados de Conexão
- **Host**: localhost
- **Porta**: 50564
- **Database**: developer_evaluation
- **Username**: developer
- **Password**: ev@luAt10n

### Arquivos Atualizados
1. **appsettings.json** - String de conexão atualizada para PostgreSQL
2. **appsettings.Development.json** - String de conexão para desenvolvimento
3. **docker-compose.yml** - Porta do PostgreSQL alterada para 50564
4. **Configurações de Mapeamento** - Todas as entidades configuradas para PostgreSQL

## Como Gerar e Aplicar a Migration

### Opção 1: Usando o Script PowerShell
```powershell
# Execute no diretório raiz do projeto
.\generate-migration.ps1
```

### Opção 2: Comando Manual
```bash
# Navegar para o projeto ORM
cd src/Ambev.DeveloperEvaluation.ORM

# Gerar a migration
dotnet ef migrations add CreateAllTables --startup-project ../Ambev.DeveloperEvaluation.WebApi

# Aplicar a migration
dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi
```

## Tabelas que Serão Criadas

1. **Bransh** - Tabela de filiais
2. **Customer** - Tabela de clientes
3. **Product** - Tabela de produtos
4. **Sale** - Tabela de vendas
5. **Product_Sales** - Tabela de itens de venda
6. **Users** - Tabela de usuários

## Configurações de Mapeamento

Todas as entidades foram configuradas com:
- Nomes de tabelas conforme especificação
- Tipos de dados PostgreSQL apropriados
- Constraints e validações necessárias
- Identity columns onde aplicável
- Conversões de enum para string

## Docker Compose

Para executar o banco de dados via Docker:
```bash
docker-compose up -d ambev.developerevaluation.database
```

O PostgreSQL estará disponível na porta 50564 com as credenciais configuradas. 