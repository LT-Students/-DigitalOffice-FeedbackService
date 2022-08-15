using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db
{
  public class DbFeedbackMapper : IDbFeedbackMapper
  {
    private readonly IDbImageMapper _imageMapper;

    public DbFeedbackMapper(IDbImageMapper imageMapper)
    {
      _imageMapper = imageMapper;
    }

    public DbFeedback Map(CreateFeedbackRequest request, List<Guid> imageIds, Guid feedbackId)
    {
      if (request is null)
      {
        return null;
      }

      return new DbFeedback
      {
        Id = feedbackId,
        Type = (int)request.Type,
        Content = request.Content,
        Status = (int)FeedbackStatusType.New,
        //TODO: Fill sender full name and ip
        SenderFullName = "",
        SenderId = Guid.NewGuid(),
        SenderIp = "",
        CreatedAtUtc = DateTime.Now
      };
    }
  }
}
