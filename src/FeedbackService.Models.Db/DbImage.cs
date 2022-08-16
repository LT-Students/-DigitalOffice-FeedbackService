using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using LT.DigitalOffice.Kernel.BrokerSupport.Attributes.ParseEntity;

namespace LT.DigitalOffice.FeedbackService.Models.Db
{
  public class DbImage
  {
    public const string TableName = "Images";
    public Guid Id { get; set; }
    public Guid FeedbackId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    [IgnoreParse]
    public DbFeedback Feedback { get; set; }
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
