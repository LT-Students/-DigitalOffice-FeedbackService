using LT.DigitalOffice.FeedbackService.Data.Provider.MsSql.Ef;
using LT.DigitalOffice.FeedbackService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.OfficeService.Data.Migrations
{
  [DbContext(typeof(FeedbackServiceDbContext))]
  [Migration("20220802120000_InitialMigration")]
  public class InitialCreate : Migration
  {
    #region Create tables

    private void CreateFeedbacksTable(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbFeedback.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Type = table.Column<int>(nullable: false),
          Content = table.Column<string>(nullable: false),
          Status = table.Column<int>(nullable: false),
          SenderFullName = table.Column<string>(nullable: false),
          SenderIp = table.Column<string>(nullable: false),
          SenderId = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ChangedBy = table.Column<Guid>(nullable: true),
          ChangedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Feedbacks", f => f.Id);
        });
    }

    private void CreateImagesTable(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbImage.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          ParentId = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          Content = table.Column<string>(nullable: false),
          Extension = table.Column<string>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_Images", i => i.Id);
        });
    }

    private void CreateFeedbacksImagesTable(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbFeedbackImage.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FeedbackId = table.Column<Guid>(nullable: false),
          ImageId = table.Column<Guid>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_FeedbacksImages", fi => fi.Id);
        });
    }

    #endregion

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      CreateFeedbacksTable(migrationBuilder);
      CreateImagesTable(migrationBuilder);
      CreateFeedbacksImagesTable(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(DbFeedback.TableName);
      migrationBuilder.DropTable(DbImage.TableName);
      migrationBuilder.DropTable(DbFeedbackImage.TableName);
    }
  }
}
