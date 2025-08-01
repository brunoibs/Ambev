using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "Price", "Amount" },
                values: new object[,]
                {
                    { 
                        Guid.NewGuid(), 
                        "Skol", 
                        10.0m, 
                        100 
                    },
                    { 
                        Guid.NewGuid(), 
                        "Guarana", 
                        7.0m, 
                        100 
                    },
                    { 
                        Guid.NewGuid(), 
                        "Agua", 
                        3.0m, 
                        100 
                    },
                    { 
                        Guid.NewGuid(), 
                        "Suco", 
                        5.0m, 
                        100 
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "Skol",
                    "Guarana",
                    "Agua",
                    "Suco"
                });
        }
    }
}
