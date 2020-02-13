using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mason_Supply.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Name = table.Column<string>(nullable: true),
                    Customer_Contact = table.Column<string>(nullable: true),
                    Order_date = table.Column<DateTime>(nullable: false),
                    Estimated_Cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    ShapeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Leg_Num = table.Column<int>(nullable: false),
                    Total_Cost = table.Column<double>(nullable: false),
                    Rebar_Type = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.ShapeID);
                    table.ForeignKey(
                        name: "FK_Shapes_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Angle",
                columns: table => new
                {
                    AngleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheAngle = table.Column<double>(nullable: false),
                    Mandrel = table.Column<double>(nullable: false),
                    ShapeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angle", x => x.AngleID);
                    table.ForeignKey(
                        name: "FK_Angle_Shapes_ShapeID",
                        column: x => x.ShapeID,
                        principalTable: "Shapes",
                        principalColumn: "ShapeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leg",
                columns: table => new
                {
                    LegID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(nullable: false),
                    ShapeID = table.Column<int>(nullable: true),
                    ShapeID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leg", x => x.LegID);
                    table.ForeignKey(
                        name: "FK_Leg_Shapes_ShapeID",
                        column: x => x.ShapeID,
                        principalTable: "Shapes",
                        principalColumn: "ShapeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leg_Shapes_ShapeID1",
                        column: x => x.ShapeID1,
                        principalTable: "Shapes",
                        principalColumn: "ShapeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angle_ShapeID",
                table: "Angle",
                column: "ShapeID");

            migrationBuilder.CreateIndex(
                name: "IX_Leg_ShapeID",
                table: "Leg",
                column: "ShapeID");

            migrationBuilder.CreateIndex(
                name: "IX_Leg_ShapeID1",
                table: "Leg",
                column: "ShapeID1");

            migrationBuilder.CreateIndex(
                name: "IX_Shapes_OrderID",
                table: "Shapes",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angle");

            migrationBuilder.DropTable(
                name: "Leg");

            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
