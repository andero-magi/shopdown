﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCocktails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    strDrink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strDrinkAlternate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strAlcoholic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strGlass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsDE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsFR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsZHHANS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strInstructionsZHHANT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strDrinkThumb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strIngredient15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strMeasure15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strImageSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strImageAttribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strCreativeCommonsConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateModified = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drinks");
        }
    }
}
