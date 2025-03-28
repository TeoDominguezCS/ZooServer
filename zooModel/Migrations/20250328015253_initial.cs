using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zooModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "zoos",
                columns: table => new
                {
                    zoo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zoos", x => x.zoo_id);
                });

            migrationBuilder.CreateTable(
                name: "habitats",
                columns: table => new
                {
                    habitat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    population = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    zoo_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_habitats", x => x.habitat_id);
                    table.ForeignKey(
                        name: "FK_habitats_zoos",
                        column: x => x.zoo_id,
                        principalTable: "zoos",
                        principalColumn: "zoo_id");
                });

            migrationBuilder.CreateTable(
                name: "animals",
                columns: table => new
                {
                    animal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    adult_num = table.Column<int>(type: "int", nullable: false),
                    child_num = table.Column<int>(type: "int", nullable: false),
                    male_num = table.Column<int>(type: "int", nullable: false),
                    female_num = table.Column<int>(type: "int", nullable: false),
                    endangered = table.Column<bool>(type: "bit", nullable: false),
                    population = table.Column<int>(type: "int", nullable: false),
                    habitat_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animals", x => x.animal_id);
                    table.ForeignKey(
                        name: "FK_animals_habitats",
                        column: x => x.habitat_id,
                        principalTable: "habitats",
                        principalColumn: "habitat_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_animals_habitat_id",
                table: "animals",
                column: "habitat_id");

            migrationBuilder.CreateIndex(
                name: "IX_habitats_zoo_id",
                table: "habitats",
                column: "zoo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animals");

            migrationBuilder.DropTable(
                name: "habitats");

            migrationBuilder.DropTable(
                name: "zoos");
        }
    }
}
