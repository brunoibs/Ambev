using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedBranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bransh",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Matriz" },
                    { Guid.NewGuid(), "Contagem" },
                    { Guid.NewGuid(), "São Paulo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bransh",
                keyColumn: "Name",
                keyValues: new object[]
                {
                    "Matriz",
                    "Contagem",
                    "São Paulo"
                });
        }
    }
}
