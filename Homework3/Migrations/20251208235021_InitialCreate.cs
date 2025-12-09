using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Homework3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "City", "Email", "FirstName", "Grade", "LastName", "ParentName", "PhoneNumber", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2008, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "samanta.william@email.com", "Samanta", "VII A", "William", "Mana William", "+1234567890", "#123456789" },
                    { 2, new DateTime(2007, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "tony.soap@email.com", "Tony", "VII B", "Soap", "James Soap", "+1234567891", "#123456789" },
                    { 3, new DateTime(2008, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "karen.hope@email.com", "Karen", "VII C", "Hope", "Justin Hope", "+1234567892", "#123456789" },
                    { 4, new DateTime(2009, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "jordan.nico@email.com", "Jordan", "VII A", "Nico", "Amanda Nico", "+1234567893", "#123456789" },
                    { 5, new DateTime(2008, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "nadila.adja@email.com", "Nadila", "VII A", "Adja", "Jack Adja", "+1234567894", "#123456789" },
                    { 6, new DateTime(2007, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "johnny.ahmad@email.com", "Johnny", "VII A", "Ahmad", "Danny Ahmad", "+1234567895", "#123456789" },
                    { 7, new DateTime(2008, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bandung", "alice.cooper@email.com", "Alice", "VII B", "Cooper", "David Cooper", "+1234567896", "#123456790" },
                    { 8, new DateTime(2007, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Surabaya", "michael.johnson@email.com", "Michael", "VII C", "Johnson", "Sarah Johnson", "+1234567897", "#123456791" },
                    { 9, new DateTime(2008, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "emily.brown@email.com", "Emily", "VII A", "Brown", "Robert Brown", "+1234567898", "#123456792" },
                    { 10, new DateTime(2009, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medan", "david.martinez@email.com", "David", "VII B", "Martinez", "Lisa Martinez", "+1234567899", "#123456793" },
                    { 11, new DateTime(2008, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "sarah.garcia@email.com", "Sarah", "VII C", "Garcia", "Carlos Garcia", "+1234567800", "#123456794" },
                    { 12, new DateTime(2007, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Semarang", "james.wilson@email.com", "James", "VII A", "Wilson", "Patricia Wilson", "+1234567801", "#123456795" },
                    { 13, new DateTime(2008, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "jessica.anderson@email.com", "Jessica", "VII B", "Anderson", "Mark Anderson", "+1234567802", "#123456796" },
                    { 14, new DateTime(2009, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yogyakarta", "daniel.taylor@email.com", "Daniel", "VII C", "Taylor", "Nancy Taylor", "+1234567803", "#123456797" },
                    { 15, new DateTime(2008, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jakarta", "laura.thomas@email.com", "Laura", "VII A", "Thomas", "Steven Thomas", "+1234567804", "#123456798" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
