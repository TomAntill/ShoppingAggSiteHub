using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingAggSite.Migrations
{
    public partial class InitialCreateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Store_StoreId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_StoreId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Location");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Store",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Store",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrandName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Store_BrandId",
                table: "Store",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Store_Id",
                table: "Location",
                column: "Id",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Brand_BrandId",
                table: "Store",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Store_Id",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_Brand_BrandId",
                table: "Store");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Store_BrandId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Store");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Location",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_StoreId",
                table: "Location",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Store_StoreId",
                table: "Location",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
