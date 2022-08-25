using FluentValidation.Results;
using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback
{
  public class EditFeedbackStatusesCommand : IEditFeedbackStatusesCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IResponseCreator _responseCreator;
    private readonly IFeedbackRepository _repository;
    private readonly IEditFeedbackStatusValidator _validator;

    public EditFeedbackStatusesCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IResponseCreator responseCreator,
      IFeedbackRepository repository,
      IEditFeedbackStatusValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _responseCreator = responseCreator;
      _repository = repository;
      _validator = validator;
    }

    public async Task<OperationResultResponse<bool>> ExecuteAsync(EditFeedbackStatusesRequest request)
    {
      if (!await _accessValidator.IsAdminAsync(_httpContextAccessor.HttpContext.GetUserId()))
      {
        return _responseCreator.CreateFailureResponse<bool>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);
      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<bool>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(e => e.ErrorMessage).ToList());
      }

      return new OperationResultResponse<bool>
      {
        Body = await _repository.EditStatusesAsync(request.FeedbackIds, request.Status)
      };
    }
  }
}
