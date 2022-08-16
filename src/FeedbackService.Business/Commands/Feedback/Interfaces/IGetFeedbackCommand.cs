using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces
{
  [AutoInject]
  public interface IGetFeedbackCommand
  {
    Task<OperationResultResponse<FeedbackInfo>> ExecuteAsync(Guid feedbackId);
  }
}
