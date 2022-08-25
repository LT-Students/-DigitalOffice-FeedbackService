using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Dto;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FeedbackController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<FeedbackResponse>> GetAsync(
      [FromQuery] Guid feedbackId,
      [FromServices] IGetFeedbackCommand command)
    {
      return await command.ExecuteAsync(feedbackId);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<FeedbackInfo>> FindAsync(
      [FromQuery] FindFeedbacksFilter filter,
      [FromServices] IFindFeedbacksCommand command)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromBody] CreateFeedbackRequest request,
      [FromServices] ICreateFeedbackCommand command)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpPut("editstatus")]
    public async Task<OperationResultResponse<bool>> EditStatusAsync(
      [FromBody] EditFeedbackStatusesRequest request,
      [FromServices] IEditFeedbackStatusesCommand command)
    {
      return await command.ExecuteAsync(request);
    }
  }
}
