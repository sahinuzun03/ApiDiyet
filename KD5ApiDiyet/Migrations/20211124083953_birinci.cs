using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KD5ApiDiyet.Migrations
{
    public partial class birinci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Kalori = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.ID);
                    table.CheckConstraint("CHK_Yiyecek_KolariSifirdanBüyük", "[kalori]>0");
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Day = table.Column<DateTime>(type: "date", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => new { x.Day, x.Hour });
                });

            migrationBuilder.CreateTable(
                name: "OgunYiyecek",
                columns: table => new
                {
                    FoodsID = table.Column<int>(type: "int", nullable: false),
                    MealsDay = table.Column<DateTime>(type: "date", nullable: false),
                    MealsHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgunYiyecek", x => new { x.FoodsID, x.MealsDay, x.MealsHour });
                    table.ForeignKey(
                        name: "FK_OgunYiyecek_Foods_FoodsID",
                        column: x => x.FoodsID,
                        principalTable: "Foods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgunYiyecek_Meals_MealsDay_MealsHour",
                        columns: x => new { x.MealsDay, x.MealsHour },
                        principalTable: "Meals",
                        principalColumns: new[] { "Day", "Hour" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Name",
                table: "Foods",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OgunYiyecek_MealsDay_MealsHour",
                table: "OgunYiyecek",
                columns: new[] { "MealsDay", "MealsHour" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgunYiyecek");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
