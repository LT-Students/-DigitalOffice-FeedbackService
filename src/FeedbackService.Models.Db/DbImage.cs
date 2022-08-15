using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.FeedbackService.Models.Db
{
  public class DbImage
  {
    public const string TableName = "Images";
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid CreatedBy { get; set; }
  }

  public class DbImageConfiguration : IEntityTypeConfiguration<DbImage>
  {
    public void Configure(EntityTypeBuilder<DbImage> builder)
    {
      builder
        .ToTable(DbImage.TableName);

      builder
        .HasKey(i => i.Id);
    }
  }
}
