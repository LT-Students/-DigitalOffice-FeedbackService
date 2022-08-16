using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using System;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbImageMapper
  {
    DbImage Map(ImageContent image, Guid feedbackId);
  }
}
