using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeamBuilder.Data.Migrations
{
    public partial class DeleteChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Teams_TeamId",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Teams_TeamId",
                table: "Invitations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Teams_TeamId",
                table: "Invitations");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Teams_TeamId",
                table: "Invitations",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
