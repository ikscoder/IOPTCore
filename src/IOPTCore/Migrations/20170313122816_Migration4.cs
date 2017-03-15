using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IOPTCore.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    objectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    login = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Userid = table.Column<long>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    pathUnit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.id);
                    table.ForeignKey(
                        name: "FK_Models_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppKeys",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    appKey = table.Column<string>(nullable: true),
                    modelid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppKeys", x => x.id);
                    table.ForeignKey(
                        name: "FK_AppKeys_Models_modelid",
                        column: x => x.modelid,
                        principalTable: "Models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    modelId = table.Column<long>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    pathUnit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.id);
                    table.ForeignKey(
                        name: "FK_Objects_Models_modelId",
                        column: x => x.modelId,
                        principalTable: "Models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    objectId = table.Column<long>(nullable: false),
                    pathUnit = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_Properties_Objects_objectId",
                        column: x => x.objectId,
                        principalTable: "Objects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyMap",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    dashboardId = table.Column<long>(nullable: false),
                    isControl = table.Column<bool>(nullable: false),
                    max = table.Column<double>(nullable: true),
                    min = table.Column<double>(nullable: true),
                    propertyid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyMap", x => x.id);
                    table.ForeignKey(
                        name: "FK_PropertyMap_Dashboards_dashboardId",
                        column: x => x.dashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyMap_Properties_propertyid",
                        column: x => x.propertyid,
                        principalTable: "Properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true),
                    pathUnit = table.Column<string>(nullable: true),
                    propertyId = table.Column<long>(nullable: false),
                    value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Scripts_Properties_propertyId",
                        column: x => x.propertyId,
                        principalTable: "Properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppKeys_modelid",
                table: "AppKeys",
                column: "modelid");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMap_dashboardId",
                table: "PropertyMap",
                column: "dashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyMap_propertyid",
                table: "PropertyMap",
                column: "propertyid");

            migrationBuilder.CreateIndex(
                name: "IX_Models_Userid",
                table: "Models",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Objects_modelId",
                table: "Objects",
                column: "modelId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_objectId",
                table: "Properties",
                column: "objectId");

            migrationBuilder.CreateIndex(
                name: "IX_Scripts_propertyId",
                table: "Scripts",
                column: "propertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppKeys");

            migrationBuilder.DropTable(
                name: "PropertyMap");

            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
