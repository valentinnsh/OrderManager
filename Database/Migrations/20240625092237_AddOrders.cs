using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordersdb");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ordersdb",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    weight = table.Column<long>(type: "bigint", nullable: false),
                    collection_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    shipper_town = table.Column<string>(type: "text", nullable: false),
                    consignee_town = table.Column<string>(type: "text", nullable: false),
                    shipper_address = table.Column<string>(type: "text", nullable: false),
                    consignee_address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders",
                schema: "ordersdb");
        }
    }
}
