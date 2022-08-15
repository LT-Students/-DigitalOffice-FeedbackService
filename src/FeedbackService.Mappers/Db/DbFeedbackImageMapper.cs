using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using System;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db
{
  public class DbFeedbackImageMapper : IDbFeedbackImageMapper
  {
    public DbFeedbackImage Map(Guid feedbackId, Guid imageId)
    {
      return new DbFeedbackImage
      {
        Id = Guid.NewGuid(),
        FeedbackId = feedbackId,
        ImageId = imageId
      };
    }
  }
}
