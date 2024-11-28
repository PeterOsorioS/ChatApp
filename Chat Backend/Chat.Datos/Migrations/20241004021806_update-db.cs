using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Datos.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdentificadorChat = table.Column<string>(type: "text", nullable: true),
                    UsuarioId1 = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId2 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatSalaId = table.Column<int>(type: "integer", nullable: false),
                    Mensajes = table.Column<string>(type: "text", nullable: true),
                    RemitenteID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMensajes_ChatRooms_ChatSalaId",
                        column: x => x.ChatSalaId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMensajes_ChatSalaId",
                table: "ChatMensajes",
                column: "ChatSalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMensajes");

            migrationBuilder.DropTable(
                name: "ChatRooms");
        }
    }
}
