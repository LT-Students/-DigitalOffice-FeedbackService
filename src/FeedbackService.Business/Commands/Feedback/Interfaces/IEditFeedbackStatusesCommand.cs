using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces
{
  [AutoInject]
  public interface IEditFeedbackStatusesCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(EditFeedbackStatusesRequest request);
  }
}
