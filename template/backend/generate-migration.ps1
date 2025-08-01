# Script para gerar migration do Entity Framework
# Execute este script no diretório raiz do projeto

Write-Host "Gerando migration para criar todas as tabelas..." -ForegroundColor Green

# Navegar para o diretório do projeto ORM
Set-Location "src/Ambev.DeveloperEvaluation.ORM"

# Gerar a migration
dotnet ef migrations add CreateAllTables --startup-project ../Ambev.DeveloperEvaluation.WebApi

Write-Host "Migration gerada com sucesso!" -ForegroundColor Green
Write-Host "Para aplicar a migration, execute: dotnet ef database update --startup-project ../Ambev.DeveloperEvaluation.WebApi" -ForegroundColor Yellow 