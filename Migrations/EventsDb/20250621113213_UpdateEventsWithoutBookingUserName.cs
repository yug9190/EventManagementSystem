using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Migrations.EventsDb
{
    /// <inheritdoc />
    public partial class UpdateEventsWithoutBookingUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove BookingUserName column (if present)
            migrationBuilder.DropColumn(
                name: "BookingUserName",
                table: "EventsTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add BookingUserName column if we rollback
            migrationBuilder.AddColumn<string>(
                name: "BookingUserName",
                table: "EventsTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
