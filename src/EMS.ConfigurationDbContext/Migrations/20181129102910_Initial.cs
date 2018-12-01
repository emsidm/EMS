using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMS.ConfigurationDbContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WorkerName = table.Column<string>(nullable: false),
                    ApiKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataContextConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ContextType = table.Column<string>(nullable: true),
                    Connector = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: true),
                    DbConnector = table.Column<string>(nullable: true),
                    DbContextType = table.Column<string>(nullable: true),
                    WorkerConfigurationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataContextConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataContextConfiguration_Workers_WorkerConfigurationId",
                        column: x => x.WorkerConfigurationId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataSourceConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ContextType = table.Column<string>(nullable: true),
                    Connector = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: true),
                    WorkerConfigurationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSourceConfiguration_Workers_WorkerConfigurationId",
                        column: x => x.WorkerConfigurationId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataTargetConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ContextType = table.Column<string>(nullable: true),
                    Connector = table.Column<string>(nullable: false),
                    ConnectionString = table.Column<string>(nullable: true),
                    WorkerConfigurationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTargetConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataTargetConfiguration_Workers_WorkerConfigurationId",
                        column: x => x.WorkerConfigurationId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataContextConfiguration_WorkerConfigurationId",
                table: "DataContextConfiguration",
                column: "WorkerConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSourceConfiguration_WorkerConfigurationId",
                table: "DataSourceConfiguration",
                column: "WorkerConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_DataTargetConfiguration_WorkerConfigurationId",
                table: "DataTargetConfiguration",
                column: "WorkerConfigurationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataContextConfiguration");

            migrationBuilder.DropTable(
                name: "DataSourceConfiguration");

            migrationBuilder.DropTable(
                name: "DataTargetConfiguration");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
