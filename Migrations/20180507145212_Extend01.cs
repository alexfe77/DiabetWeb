using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DiabetWeb.Migrations
{
    public partial class Extend01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnergyKC",
                table: "MemberItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnergyKJ",
                table: "MemberItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCarbohydrates",
                table: "MemberItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalFat",
                table: "MemberItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalProtein",
                table: "MemberItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnergyKC",
                table: "FoodItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnergyKJ",
                table: "FoodItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Favorites",
                table: "FoodItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyKC",
                table: "MemberItems");

            migrationBuilder.DropColumn(
                name: "EnergyKJ",
                table: "MemberItems");

            migrationBuilder.DropColumn(
                name: "TotalCarbohydrates",
                table: "MemberItems");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                table: "MemberItems");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "MemberItems");

            migrationBuilder.DropColumn(
                name: "EnergyKC",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "EnergyKJ",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "FoodItems");
        }
    }
}
