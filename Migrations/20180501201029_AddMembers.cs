using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiabetWeb.Migrations
{
    public partial class AddMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dose = table.Column<double>(nullable: false),
                    F1 = table.Column<double>(nullable: false),
                    F2 = table.Column<double>(nullable: false),
                    F3 = table.Column<double>(nullable: false),
                    K1 = table.Column<double>(nullable: false),
                    K2 = table.Column<double>(nullable: false),
                    K3 = table.Column<double>(nullable: false),
                    MemberLogin = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberItems");
        }
    }
}
