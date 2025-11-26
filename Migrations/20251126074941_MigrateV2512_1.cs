using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MailArchiver.Migrations
{
    /// <inheritdoc />
    public partial class MigrateV2512_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseModSeq",
                schema: "mail_archiver",
                table: "MailAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MailAccountFolders",
                schema: "mail_archiver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MailAccountId = table.Column<int>(type: "integer", nullable: false),
                    UidValidity = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastSync = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastSyncModSeq = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailAccountFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailAccountFolders_MailAccounts_MailAccountId",
                        column: x => x.MailAccountId,
                        principalSchema: "mail_archiver",
                        principalTable: "MailAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailAccountFolders_MailAccountId",
                schema: "mail_archiver",
                table: "MailAccountFolders",
                column: "MailAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailAccountFolders",
                schema: "mail_archiver");

            migrationBuilder.DropColumn(
                name: "UseModSeq",
                schema: "mail_archiver",
                table: "MailAccounts");

            migrationBuilder.DropColumn(
                name: "BodyUntruncatedHtml",
                schema: "mail_archiver",
                table: "ArchivedEmails");

            migrationBuilder.DropColumn(
                name: "BodyUntruncatedText",
                schema: "mail_archiver",
                table: "ArchivedEmails");

            migrationBuilder.AlterColumn<string>(
                name: "Provider",
                schema: "mail_archiver",
                table: "MailAccounts",
                type: "text",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OAuthRemoteUserId",
                schema: "mail_archiver",
                table: "Users",
                column: "OAuthRemoteUserId",
                unique: true,
                filter: "\"OAuthRemoteUserId\" IS NOT NULL");
        }
    }
}
