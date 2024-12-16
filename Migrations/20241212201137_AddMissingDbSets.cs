using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRules.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChoreAssignment_Chore_ChoreId",
                table: "ChoreAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreAssignment_UserProfiles_UserProfileId",
                table: "ChoreAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreCompletion_Chore_ChoreId",
                table: "ChoreCompletion");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreCompletion_UserProfiles_UserProfileId",
                table: "ChoreCompletion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChoreCompletion",
                table: "ChoreCompletion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChoreAssignment",
                table: "ChoreAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chore",
                table: "Chore");

            migrationBuilder.RenameTable(
                name: "ChoreCompletion",
                newName: "ChoreCompletions");

            migrationBuilder.RenameTable(
                name: "ChoreAssignment",
                newName: "ChoreAssignments");

            migrationBuilder.RenameTable(
                name: "Chore",
                newName: "Chores");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreCompletion_UserProfileId",
                table: "ChoreCompletions",
                newName: "IX_ChoreCompletions_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreCompletion_ChoreId",
                table: "ChoreCompletions",
                newName: "IX_ChoreCompletions_ChoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreAssignment_UserProfileId",
                table: "ChoreAssignments",
                newName: "IX_ChoreAssignments_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreAssignment_ChoreId",
                table: "ChoreAssignments",
                newName: "IX_ChoreAssignments_ChoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChoreCompletions",
                table: "ChoreCompletions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChoreAssignments",
                table: "ChoreAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chores",
                table: "Chores",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59467d0f-696c-47a1-a346-ef59e5048270", "AQAAAAIAAYagAAAAEP/VSTdYXW0VAuGjdCBjEzlIUiWRFqcYPWVjO8jQh038Q7DlII+tGjUhxSd8cvlBVw==", "b04a70d3-0fce-4bee-bff0-d67b4749920b" });

            migrationBuilder.UpdateData(
                table: "ChoreCompletions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 11, 14, 11, 37, 617, DateTimeKind.Local).AddTicks(7380));

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreAssignments_Chores_ChoreId",
                table: "ChoreAssignments",
                column: "ChoreId",
                principalTable: "Chores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreAssignments_UserProfiles_UserProfileId",
                table: "ChoreAssignments",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreCompletions_Chores_ChoreId",
                table: "ChoreCompletions",
                column: "ChoreId",
                principalTable: "Chores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreCompletions_UserProfiles_UserProfileId",
                table: "ChoreCompletions",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChoreAssignments_Chores_ChoreId",
                table: "ChoreAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreAssignments_UserProfiles_UserProfileId",
                table: "ChoreAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreCompletions_Chores_ChoreId",
                table: "ChoreCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChoreCompletions_UserProfiles_UserProfileId",
                table: "ChoreCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chores",
                table: "Chores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChoreCompletions",
                table: "ChoreCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChoreAssignments",
                table: "ChoreAssignments");

            migrationBuilder.RenameTable(
                name: "Chores",
                newName: "Chore");

            migrationBuilder.RenameTable(
                name: "ChoreCompletions",
                newName: "ChoreCompletion");

            migrationBuilder.RenameTable(
                name: "ChoreAssignments",
                newName: "ChoreAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreCompletions_UserProfileId",
                table: "ChoreCompletion",
                newName: "IX_ChoreCompletion_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreCompletions_ChoreId",
                table: "ChoreCompletion",
                newName: "IX_ChoreCompletion_ChoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreAssignments_UserProfileId",
                table: "ChoreAssignment",
                newName: "IX_ChoreAssignment_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ChoreAssignments_ChoreId",
                table: "ChoreAssignment",
                newName: "IX_ChoreAssignment_ChoreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chore",
                table: "Chore",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChoreCompletion",
                table: "ChoreCompletion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChoreAssignment",
                table: "ChoreAssignment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28afb1c5-5fd9-47bb-b3e7-f438bb44a362", "AQAAAAIAAYagAAAAEFcCS14kT7Wdw3AVxWRQRkh0/rkEmdhRhHu37EYRkpjLYeL/dAlIIG3LZvhYYrxo/w==", "f67bf55f-1e8b-411f-a364-dd8a8886963d" });

            migrationBuilder.UpdateData(
                table: "ChoreCompletion",
                keyColumn: "Id",
                keyValue: 1,
                column: "CompletedOn",
                value: new DateTime(2024, 12, 10, 14, 29, 10, 970, DateTimeKind.Local).AddTicks(2645));

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreAssignment_Chore_ChoreId",
                table: "ChoreAssignment",
                column: "ChoreId",
                principalTable: "Chore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreAssignment_UserProfiles_UserProfileId",
                table: "ChoreAssignment",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreCompletion_Chore_ChoreId",
                table: "ChoreCompletion",
                column: "ChoreId",
                principalTable: "Chore",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChoreCompletion_UserProfiles_UserProfileId",
                table: "ChoreCompletion",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
