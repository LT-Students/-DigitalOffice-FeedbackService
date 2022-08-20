using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FeedbackService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IFeedbackResponseMapper
  {
    FeedbackResponse Map(DbFeedback dbFeedback);
  }
}
