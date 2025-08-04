# Solução para Erro no CreateSale

## Problema Identificado

O erro que você está enfrentando pode ser causado por vários fatores. Vou fornecer uma solução completa.

## Passos para Resolver

### 1. Verificar o JSON

O JSON que você está enviando tem alguns problemas de nomenclatura. Use este formato correto:

```json
{
  "dtSale": "2024-01-15T14:30:00Z",
  "idCustomer": "7784319a-5eaf-408f-b8e5-40d6506ffd54",
  "idCreate": "7784319a-5eaf-408f-b8e5-40d6506ffd54",
  "idBranch": "51d8f4bb-5fb6-4f3e-b1c6-0966a9f0f34a",
  "productSales": [
    {
      "idProduct": "039e444c-028e-4d7e-b564-b30a2ca11167",
      "amount": 10,
      "price": 7.00,
      "total": 70.00
    },
    {
      "idProduct": "bbe8a82a-58ea-41d6-a6cf-073ac3adc8d9",
      "amount": 10,
      "price": 3.00,
      "total": 30.00
    }
  ]
}
```

**Principais correções:**
- `IdBranch` → `idBranch` (minúsculo)
- `productSales` → `productSales` (já estava correto)

### 2. Executar o Projeto Corretamente

#### Opção A: Usando o Script Automatizado
```powershell
# No diretório raiz do projeto
.\run-project.ps1
```

#### Opção B: Passo a Passo Manual

1. **Iniciar o Docker Desktop**

2. **Iniciar o banco de dados:**
```powershell
docker-compose up -d ambev.developerevaluation.database
```

3. **Aguardar o banco estar pronto (10-15 segundos)**

4. **Navegar para o projeto WebApi:**
```powershell
cd src/Ambev.DeveloperEvaluation.WebApi
```

5. **Restaurar pacotes:**
```powershell
dotnet restore
```

6. **Aplicar migrations:**
```powershell
dotnet ef database update --startup-project .
```

7. **Executar o projeto:**
```powershell
dotnet run --urls "https://localhost:44312"
```

### 3. Testar a API

1. **Acesse o Swagger UI:**
   - URL: https://localhost:44312/swagger

2. **Teste o endpoint POST /api/sale**
   - Use o JSON corrigido acima
   - Clique em "Try it out"
   - Cole o JSON no campo Request body
   - Clique em "Execute"

### 4. Verificar Logs

Se ainda houver erro, verifique:

1. **Console da aplicação** - Mensagens de erro detalhadas
2. **Docker logs** - Se o banco está rodando:
```powershell
docker-compose logs ambev.developerevaluation.database
```

### 5. Problemas Comuns e Soluções

#### Problema: "Arquivo de projeto não existe"
**Solução:** Execute o comando do diretório correto:
```powershell
cd template/backend
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj --urls "https://localhost:44312"
```

#### Problema: "Connection refused" no banco
**Solução:** Verifique se o Docker está rodando e o container do PostgreSQL está ativo:
```powershell
docker ps
docker-compose logs ambev.developerevaluation.database
```

#### Problema: "Validation failed"
**Solução:** Verifique se:
- Todos os IDs são GUIDs válidos
- Os totais correspondem ao cálculo (preço × quantidade)
- As quantidades estão entre 1 e 20
- Os preços são maiores que zero

### 6. Melhorias Implementadas

1. **Mapeamento AutoMapper melhorado** - Configurações mais explícitas
2. **Validação melhorada** - Tolerância para diferenças de ponto flutuante
3. **Tratamento de erros melhorado** - Mensagens mais claras
4. **Docker Compose configurado** - Portas mapeadas corretamente

### 7. Endpoints Disponíveis

- **POST /api/sale** - Criar venda
- **GET /api/sale** - Listar vendas
- **GET /api/sale/{id}** - Buscar venda por ID
- **DELETE /api/sale/{id}** - Deletar venda

### 8. Estrutura do JSON de Venda

```json
{
  "dtSale": "2024-01-15T14:30:00Z",        // Data da venda (ISO 8601)
  "idCustomer": "guid-do-cliente",           // ID do cliente
  "idCreate": "guid-do-usuario",             // ID do usuário que cria
  "idBranch": "guid-da-filial",              // ID da filial
  "productSales": [                          // Lista de produtos
    {
      "idProduct": "guid-do-produto",        // ID do produto
      "amount": 10,                          // Quantidade (1-20)
      "price": 7.00,                         // Preço unitário
      "total": 70.00                         // Total do item (preço × quantidade)
    }
  ]
}
```

### 9. Validações Implementadas

- Data da venda obrigatória
- IDs obrigatórios (customer, create, branch)
- Pelo menos um item na venda
- Quantidade entre 1 e 20
- Preço maior que zero
- Total deve corresponder ao cálculo
- Produtos devem existir no banco (opcional)

### 10. Contato

Se ainda houver problemas, verifique:
1. Logs do console da aplicação
2. Logs do Docker
3. Configuração do banco de dados
4. Versão do .NET (deve ser 8.0 ou superior) 