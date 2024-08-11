using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusValidationIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_statusesValidations_StatusValidationId",
                table: "statusesValidations",
                column: "StatusValidationId");

            migrationBuilder.AddForeignKey(
                name: "FK_statusesValidations_Statuss_StatusValidationId",
                table: "statusesValidations",
                column: "StatusValidationId",
                principalTable: "Statuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_statusesValidations_Statuss_StatusValidationId",
                table: "statusesValidations");

            migrationBuilder.DropIndex(
                name: "IX_statusesValidations_StatusValidationId",
                table: "statusesValidations");
        }
    }
}
