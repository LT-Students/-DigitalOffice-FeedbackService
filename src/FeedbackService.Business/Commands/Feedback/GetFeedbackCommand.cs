using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback
{
  public class GetFeedbackCommand : IGetFeedbackCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFeedbackRepository _repository;
    private readonly IFeedbackResponseMapper _mapper;

    public GetFeedbackCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      IHttpContextAccessor httpContextAccessor,
      IFeedbackRepository repository,
      IFeedbackResponseMapper mapper)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _httpContextAccessor = httpContextAccessor;
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<OperationResultResponse<FeedbackResponse>> ExecuteAsync(Guid feedbackId)
    {
      if (!await _accessValidator.IsAdminAsync(_httpContextAccessor.HttpContext.GetUserId()))
      {
        return _responseCreator.CreateFailureResponse<FeedbackResponse>(HttpStatusCode.Forbidden);
      }

      if (feedbackId == default)
      {
        return _responseCreator.CreateFailureResponse<FeedbackResponse>(HttpStatusCode.BadRequest);
      }

      DbFeedback feedback = await _repository.GetAsync(feedbackId);

      if (feedback is null)
      {
        _responseCreator.CreateFailureResponse<FeedbackResponse>(HttpStatusCode.NotFound);
      }

      return new OperationResultResponse<FeedbackResponse>(
        body: _mapper.Map(feedback));
    }
  }
}
