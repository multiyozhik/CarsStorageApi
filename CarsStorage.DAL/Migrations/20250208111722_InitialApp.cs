using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarsStorage.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Make = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    IsAccassible = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleEntityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RoleClaims = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleEntityId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserEntityId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    AccessToken = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserEntityId);
                });

            migrationBuilder.CreateTable(
                name: "RoleEntityUserEntity",
                columns: table => new
                {
                    RolesListRoleEntityId = table.Column<int>(type: "integer", nullable: false),
                    UsersListUserEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntityUserEntity", x => new { x.RolesListRoleEntityId, x.UsersListUserEntityId });
                    table.ForeignKey(
                        name: "FK_RoleEntityUserEntity_Roles_RolesListRoleEntityId",
                        column: x => x.RolesListRoleEntityId,
                        principalTable: "Roles",
                        principalColumn: "RoleEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleEntityUserEntity_Users_UsersListUserEntityId",
                        column: x => x.UsersListUserEntityId,
                        principalTable: "Users",
                        principalColumn: "UserEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserEntityId = table.Column<int>(type: "integer", nullable: false),
                    RoleEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserEntityId, x.RoleEntityId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleEntityId",
                        column: x => x.RoleEntityId,
                        principalTable: "Roles",
                        principalColumn: "RoleEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "UserEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Color", "Count", "IsAccassible", "Make", "Model" },
                values: new object[,]
                {
                    { 1, "красный", 4, true, "Kalina", "Lada" },
                    { 2, "белый", 8, true, "J7", "JAC" },
                    { 3, "синий", 6, true, "Granta", "Lada" },
                    { 4, "черный", 5, true, "G8", "Audi" },
                    { 5, "серый", 2, true, "Tigo 4", "Cherry" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleEntityId", "Name", "RoleClaims" },
                values: new object[,]
                {
                    { 1, "admin", new[] { 2, 3 } },
                    { 2, "manager", new[] { 4, 1 } },
                    { 3, "user", new[] { 1 } }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserEntityId", "AccessToken", "Email", "Hash", "RefreshToken", "Salt", "UserName" },
                values: new object[,]
                {
                    { 1, null, "admin@mail.ru", "JRxt+pwxtgCpgGS7TLXEjsPxV1ll/IZhLZSvN6QDzdc=", null, "+9jY4swyva9aJKzTI/5mHQ==", "admin" },
                    { 2, null, "manager@mail.ru", "adA8MaXGA/uwLaAtQ+ikggavMypELr5NK+V+KyB4l4U=", null, "Dxid1tYUAovqRAvao2lBtQ==", "manager" },
                    { 3, null, "user3@mail.ru", "Y0M0qRE2drIzyrcznkG1DsEhkrCDmI1GzkzXEpsv9yU=", null, "LolXd97/5wfAEqROD/8bMg==", "user3" },
                    { 4, null, "user4@mail.ru", "DQaXnrG7s5V9HUHoQiWAwhMpe661ZJlJdQ661J+n8gQ=", null, "BgqW+f6XxuldT0UltY6vMg==", "user4" },
                    { 5, null, "user5@mail.ru", "eZw/yjr7gGpoaLUABw8Fgz2rNIMwLnqAN6o0S8tLS+8=", null, "tQwRgbrUoOK519vJt2X10Q==", "user5" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleEntityId", "UserEntityId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntityUserEntity_UsersListUserEntityId",
                table: "RoleEntityUserEntity",
                column: "UsersListUserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleEntityId",
                table: "UsersRoles",
                column: "RoleEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "RoleEntityUserEntity");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
