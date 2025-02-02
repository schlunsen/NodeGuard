/*
 * NodeGuard
 * Copyright (C) 2023  Elenpay
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see http://www.gnu.org/licenses/.
 *
 */

﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FundsManager.Migrations
{
    public partial class UtxoRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelOperationRequestUTXO");

            migrationBuilder.DropTable(
                name: "UTXOs");

            migrationBuilder.CreateTable(
                name: "FMUTXOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TxId = table.Column<string>(type: "text", nullable: false),
                    OutputIndex = table.Column<long>(type: "bigint", nullable: false),
                    SatsAmount = table.Column<long>(type: "bigint", nullable: false),
                    CreationDatetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdateDatetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMUTXOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelOperationRequestFMUTXO",
                columns: table => new
                {
                    ChannelOperationRequestsId = table.Column<int>(type: "integer", nullable: false),
                    UtxosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelOperationRequestFMUTXO", x => new { x.ChannelOperationRequestsId, x.UtxosId });
                    table.ForeignKey(
                        name: "FK_ChannelOperationRequestFMUTXO_ChannelOperationRequests_Chan~",
                        column: x => x.ChannelOperationRequestsId,
                        principalTable: "ChannelOperationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelOperationRequestFMUTXO_FMUTXOs_UtxosId",
                        column: x => x.UtxosId,
                        principalTable: "FMUTXOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelOperationRequestFMUTXO_UtxosId",
                table: "ChannelOperationRequestFMUTXO",
                column: "UtxosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelOperationRequestFMUTXO");

            migrationBuilder.DropTable(
                name: "FMUTXOs");

            migrationBuilder.CreateTable(
                name: "UTXOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationDatetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    OutputIndex = table.Column<long>(type: "bigint", nullable: false),
                    SatsAmount = table.Column<long>(type: "bigint", nullable: false),
                    TxId = table.Column<string>(type: "text", nullable: false),
                    UpdateDatetime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTXOs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelOperationRequestUTXO",
                columns: table => new
                {
                    ChannelOperationRequestsId = table.Column<int>(type: "integer", nullable: false),
                    UtxosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelOperationRequestUTXO", x => new { x.ChannelOperationRequestsId, x.UtxosId });
                    table.ForeignKey(
                        name: "FK_ChannelOperationRequestUTXO_ChannelOperationRequests_Channe~",
                        column: x => x.ChannelOperationRequestsId,
                        principalTable: "ChannelOperationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelOperationRequestUTXO_UTXOs_UtxosId",
                        column: x => x.UtxosId,
                        principalTable: "UTXOs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelOperationRequestUTXO_UtxosId",
                table: "ChannelOperationRequestUTXO",
                column: "UtxosId");
        }
    }
}
