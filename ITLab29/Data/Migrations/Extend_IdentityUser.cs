using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITLab29.Data.Migrations {
    public class Extend_IdentityUser : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "AspNetUsers",
                nullable: false
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                    name: "userId",
                    table: "AspNetUsers"
                );
        }
    }
}
