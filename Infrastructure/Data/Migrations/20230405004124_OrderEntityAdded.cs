﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class OrderEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryTime = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    BuyerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    ShipToAddress_LastName = table.Column<string>(type: "TEXT", nullable: false),
                    ShipToAddress_Street = table.Column<string>(type: "TEXT", nullable: false),
                    ShipToAddress_City = table.Column<string>(type: "TEXT", nullable: false),
                    ShipToAddress_State = table.Column<string>(type: "TEXT", nullable: false),
                    ShipToAddress_ZipCode = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryMethodid = table.Column<int>(type: "INTEGER", nullable: false),
                    SubTotal = table.Column<double>(type: "REAL", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentItentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodid",
                        column: x => x.DeliveryMethodid,
                        principalTable: "DeliveryMethods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemOrdered_ProductItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemOrdered_ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ItemOrdered_PictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Orderid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Orderid",
                table: "OrderItems",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodid",
                table: "Orders",
                column: "DeliveryMethodid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");
        }
    }
}
