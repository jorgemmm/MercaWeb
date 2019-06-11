using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MvcTpv.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HighDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "SaleMan",
                columns: table => new
                {
                    SaleManID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HighDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleMan", x => x.SaleManID);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categoria",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ProviderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HighDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.ProviderID);
                    table.ForeignKey(
                        name: "FK_Provider_Categoria_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categoria",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: true),
                    Fecha_Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Num_comprobante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleManID = table.Column<int>(type: "int", nullable: false),
                    Serie_comprobante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo_Comprobante = table.Column<int>(type: "int", nullable: true),
                    TotalSale = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sale_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sale_SaleMan_SaleManID",
                        column: x => x.SaleManID,
                        principalTable: "SaleMan",
                        principalColumn: "SaleManID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Input",
                columns: table => new
                {
                    InputID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha_hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Impuesto = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    ProviderID = table.Column<int>(type: "int", nullable: false),
                    TipoComprobante = table.Column<string>(name: "Tipo Comprobante", type: "nvarchar(max)", nullable: true),
                    TotalInput = table.Column<decimal>(type: "money", nullable: true),
                    NumeroComprobante = table.Column<string>(name: "Numero Comprobante", type: "nvarchar(max)", nullable: true),
                    SerieComprobante = table.Column<string>(name: "Serie Comprobante", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Input", x => x.InputID);
                    table.ForeignKey(
                        name: "FK_Input_Provider_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Provider",
                        principalColumn: "ProviderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleDetail",
                columns: table => new
                {
                    SaleDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<decimal>(type: "money", nullable: false),
                    PVP = table.Column<decimal>(type: "money", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SaleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleDetail", x => x.SaleDetailID);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Producto_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Producto",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleDetail_Sale_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sale",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InputDetail",
                columns: table => new
                {
                    InputDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    InputID = table.Column<int>(type: "int", nullable: false),
                    PNETO = table.Column<decimal>(type: "money", nullable: true),
                    PVP = table.Column<decimal>(type: "money", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputDetail", x => x.InputDetailID);
                    table.ForeignKey(
                        name: "FK_InputDetail_Input_InputID",
                        column: x => x.InputID,
                        principalTable: "Input",
                        principalColumn: "InputID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InputDetail_Producto_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Producto",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Input_ProviderID",
                table: "Input",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_InputDetail_InputID",
                table: "InputDetail",
                column: "InputID");

            migrationBuilder.CreateIndex(
                name: "IX_InputDetail_ProductID",
                table: "InputDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoryID",
                table: "Producto",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Provider_CategoryID",
                table: "Provider",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_CustomerID",
                table: "Sale",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_SaleManID",
                table: "Sale",
                column: "SaleManID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_ProductID",
                table: "SaleDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_SaleID",
                table: "SaleDetail",
                column: "SaleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputDetail");

            migrationBuilder.DropTable(
                name: "SaleDetail");

            migrationBuilder.DropTable(
                name: "Input");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "SaleMan");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
