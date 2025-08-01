using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username", "Password", "Phone", "Email", "Role", "Status", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 
                        Guid.NewGuid(), 
                        "Ranger Vermelho", 
                        "Ranger_Vermelho", 
                        "31988887777", 
                        "ranger_vermelho@teste.com", 
                        "Admin", 
                        "Active", 
                        DateTime.UtcNow, 
                        (DateTime?)null 
                    },
                    { 
                        Guid.NewGuid(), 
                        "Ranger Azul", 
                        "Ranger_Azul", 
                        "31988887755", 
                        "ranger_azul@teste.com", 
                        "Customer", 
                        "Active", 
                        DateTime.UtcNow, 
                        (DateTime?)null 
                    },
                    { 
                        Guid.NewGuid(), 
                        "Ranger Rosa", 
                        "Ranger_Rosa", 
                        "31988887733", 
                        "ranger_rosa@teste.com", 
                        "Customer", 
                        "Active", 
                        DateTime.UtcNow, 
                        (DateTime?)null 
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Email",
                keyValues: new object[]
                {
                    "ranger_vermelho@teste.com",
                    "ranger_azul@teste.com",
                    "ranger_rosa@teste.com"
                });
        }
    }
}
