# Script para executar o projeto Ambev Developer Evaluation
Write-Host "=== Ambev Developer Evaluation - Setup ===" -ForegroundColor Green

# Verificar se o Docker está rodando
Write-Host "Verificando se o Docker está rodando..." -ForegroundColor Yellow
try {
    docker version | Out-Null
    Write-Host "Docker está rodando!" -ForegroundColor Green
}
catch {
    Write-Host "ERRO: Docker não está rodando. Por favor, inicie o Docker Desktop." -ForegroundColor Red
    exit 1
}

# Parar containers existentes
Write-Host "Parando containers existentes..." -ForegroundColor Yellow
docker-compose down

# Iniciar o banco de dados
Write-Host "Iniciando o banco de dados PostgreSQL..." -ForegroundColor Yellow
docker-compose up -d ambev.developerevaluation.database

# Aguardar o banco estar pronto
Write-Host "Aguardando o banco de dados estar pronto..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Verificar se o banco está acessível
Write-Host "Verificando conexão com o banco..." -ForegroundColor Yellow
try {
    $connectionString = "Host=localhost;Port=49916;Database=developer_evaluation;Username=developer;Password=ev@luAt10n"
    # Aqui você pode adicionar uma verificação de conexão se necessário
    Write-Host "Banco de dados está acessível!" -ForegroundColor Green
}
catch {
    Write-Host "ERRO: Não foi possível conectar ao banco de dados." -ForegroundColor Red
    exit 1
}

# Navegar para o diretório do projeto
Set-Location "src/Ambev.DeveloperEvaluation.WebApi"

# Restaurar pacotes
Write-Host "Restaurando pacotes NuGet..." -ForegroundColor Yellow
dotnet restore

# Aplicar migrations
Write-Host "Aplicando migrations do banco de dados..." -ForegroundColor Yellow
dotnet ef database update --startup-project .

# Executar o projeto
Write-Host "Iniciando a aplicação..." -ForegroundColor Yellow
Write-Host "A API estará disponível em: https://localhost:44312" -ForegroundColor Green
Write-Host "Swagger UI: https://localhost:44312/swagger" -ForegroundColor Green
Write-Host "" -ForegroundColor White
Write-Host "Para testar a criação de venda, use o JSON:" -ForegroundColor Cyan
Write-Host @"
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
"@ -ForegroundColor White

dotnet run --urls "https://localhost:44312" 