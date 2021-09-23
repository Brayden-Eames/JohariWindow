using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdjectiveId",
                table: "FriendResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "FriendResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "FriendResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdjectiveId",
                table: "ClientResponse",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientResponse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendResponse_AdjectiveId",
                table: "FriendResponse",
                column: "AdjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendResponse_ClientId",
                table: "FriendResponse",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendResponse_FriendId",
                table: "FriendResponse",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientResponse_AdjectiveId",
                table: "ClientResponse",
                column: "AdjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientResponse_ClientId",
                table: "ClientResponse",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientResponse_Adjective_AdjectiveId",
                table: "ClientResponse",
                column: "AdjectiveId",
                principalTable: "Adjective",
                principalColumn: "AdjectiveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientResponse_Client_ClientId",
                table: "ClientResponse",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendResponse_Adjective_AdjectiveId",
                table: "FriendResponse",
                column: "AdjectiveId",
                principalTable: "Adjective",
                principalColumn: "AdjectiveId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendResponse_Client_ClientId",
                table: "FriendResponse",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendResponse_Friend_FriendId",
                table: "FriendResponse",
                column: "FriendId",
                principalTable: "Friend",
                principalColumn: "FriendId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientResponse_Adjective_AdjectiveId",
                table: "ClientResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientResponse_Client_ClientId",
                table: "ClientResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendResponse_Adjective_AdjectiveId",
                table: "FriendResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendResponse_Client_ClientId",
                table: "FriendResponse");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendResponse_Friend_FriendId",
                table: "FriendResponse");

            migrationBuilder.DropIndex(
                name: "IX_FriendResponse_AdjectiveId",
                table: "FriendResponse");

            migrationBuilder.DropIndex(
                name: "IX_FriendResponse_ClientId",
                table: "FriendResponse");

            migrationBuilder.DropIndex(
                name: "IX_FriendResponse_FriendId",
                table: "FriendResponse");

            migrationBuilder.DropIndex(
                name: "IX_ClientResponse_AdjectiveId",
                table: "ClientResponse");

            migrationBuilder.DropIndex(
                name: "IX_ClientResponse_ClientId",
                table: "ClientResponse");

            migrationBuilder.DropColumn(
                name: "AdjectiveId",
                table: "FriendResponse");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FriendResponse");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "FriendResponse");

            migrationBuilder.DropColumn(
                name: "AdjectiveId",
                table: "ClientResponse");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientResponse");
        }
    }
}
