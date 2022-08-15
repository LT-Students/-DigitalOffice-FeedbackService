using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using System;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db
{
  public class DbImageMapper : IDbImageMapper
  {
    public DbImage Map(ImageContent image, Guid feedbackId)
    {
      if (image is null)
      {
        return null;
      }

      return new DbImage
      {
        Id = Guid.NewGuid(),
        ParentId = feedbackId,
        Name = image.Name,
        Content = image.Content,
        Extension = image.Extension,
        CreatedAtUtc = DateTime.Now,
        //TODO: Fix
        CreatedBy = Guid.NewGuid()
      };
    }
  }
}
