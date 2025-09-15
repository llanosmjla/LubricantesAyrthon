using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LubricantesAyrthonAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetail_Sales_Saleid",
                table: "SaleDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetail_Saleid",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Saleid",
                table: "SaleDetail");

            migrationBuilder.RenameTable(
                name: "SaleDetail",
                newName: "SaleDetails");

            migrationBuilder.RenameColumn(
                name: "totalPrice",
                table: "Sales",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "saleDate",
                table: "Sales",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "idSeller",
                table: "Sales",
                newName: "IdSeller");

            migrationBuilder.RenameColumn(
                name: "idCustomer",
                table: "Sales",
                newName: "IdCustomer");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Sales",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "SaleDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "SaleDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "idSale",
                table: "SaleDetails",
                newName: "IdSale");

            migrationBuilder.RenameColumn(
                name: "idProduct",
                table: "SaleDetails",
                newName: "IdProduct");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SaleDetails",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ci",
                table: "Customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ci = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IdCustomer",
                table: "Sales",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_IdSeller",
                table: "Sales",
                column: "IdSeller");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Ci",
                table: "Customers",
                column: "Ci",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_IdProduct",
                table: "SaleDetails",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetails_IdSale",
                table: "SaleDetails",
                column: "IdSale");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Products_IdProduct",
                table: "SaleDetails",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetails_Sales_IdSale",
                table: "SaleDetails",
                column: "IdSale",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_IdCustomer",
                table: "Sales",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Sellers_IdSeller",
                table: "Sales",
                column: "IdSeller",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Products_IdProduct",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleDetails_Sales_IdSale",
                table: "SaleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_IdCustomer",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Sellers_IdSeller",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sales_IdCustomer",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_IdSeller",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Ci",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleDetails",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_IdProduct",
                table: "SaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_SaleDetails_IdSale",
                table: "SaleDetails");

            migrationBuilder.DropColumn(
                name: "Ci",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "SaleDetails",
                newName: "SaleDetail");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Sales",
                newName: "totalPrice");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Sales",
                newName: "saleDate");

            migrationBuilder.RenameColumn(
                name: "IdSeller",
                table: "Sales",
                newName: "idSeller");

            migrationBuilder.RenameColumn(
                name: "IdCustomer",
                table: "Sales",
                newName: "idCustomer");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sales",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "SaleDetail",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "SaleDetail",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "IdSale",
                table: "SaleDetail",
                newName: "idSale");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "SaleDetail",
                newName: "idProduct");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SaleDetail",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Saleid",
                table: "SaleDetail",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleDetail",
                table: "SaleDetail",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleDetail_Saleid",
                table: "SaleDetail",
                column: "Saleid");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleDetail_Sales_Saleid",
                table: "SaleDetail",
                column: "Saleid",
                principalTable: "Sales",
                principalColumn: "id");
        }
    }
}
