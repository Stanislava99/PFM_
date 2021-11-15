using Microsoft.EntityFrameworkCore.Migrations;

namespace pfm.Migrations
{
    public partial class TransactionMigration : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable (
                name: "transactions",
                column: table => new 
                {
                    id = table.Column<int>(nullable: false),
                    beneficiaty_name = table.Column<string> (maxLength:255, nullable:true),
                    dete = table.Column<string>(nullable: false),
                    amount = table.Column<double>(nullable: false),
                    description = table.Column<string>(maxLength: 255,nullable: true),
                    currency = table.Column<string>(maxLength:3, nullable: false),
                    mcc = table.Column<int>(nullable: true),
                    kind = table.Column<string>(maxLength: 3,nullable:false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name:"transactions"
            );
        }
    }
}