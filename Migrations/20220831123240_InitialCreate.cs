﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamQueue.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionPeriod = table.Column<long>(type: "bigint", nullable: false),
                    LineStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LineFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RegistrationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SteamAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_Accounts_SteamAccountId",
                        column: x => x.SteamAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TelegramRequesterId = table.Column<long>(type: "bigint", nullable: false),
                    BotId = table.Column<long>(type: "bigint", nullable: true),
                    NumberInTheQueue = table.Column<int>(type: "int", nullable: false),
                    Requester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimelineStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TimelineFinish = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DescriptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_SteamAccountId",
                table: "Lines",
                column: "SteamAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_LineId",
                table: "Positions",
                column: "LineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
