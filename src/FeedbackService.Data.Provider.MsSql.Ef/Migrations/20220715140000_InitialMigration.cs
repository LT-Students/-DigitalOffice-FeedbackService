using LT.DigitalOffice.FeedbackService.Data.Provider.MsSql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.OfficeService.Data.Migrations
{
  [DbContext(typeof(FeedbackServiceDbContext))]
  [Migration("20220802120000_InitialMigration")]
  public class InitialCreate : Migration
  {
    #region Create tables

    #endregion

    protected override void Up(MigrationBuilder migrationBuilder)
    {
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
    }
  }
}
