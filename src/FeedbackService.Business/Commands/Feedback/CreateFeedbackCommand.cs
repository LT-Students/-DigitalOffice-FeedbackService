using FluentValidation.Results;
using LT.DigitalOffice.FeedbackService.Business.Commands.Feedback.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Business.Commands.Feedback
{
  public class CreateFeedbackCommand : ICreateFeedbackCommand
  {
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IImageRepository _imageRepository;
    private readonly ICreateFeedbackValidator _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbFeedbackMapper _feedbackMapper;
    private readonly IDbImageMapper _imageMapper;
    private readonly IResponseCreator _responseCreator;
    private readonly ILogger<CreateFeedbackCommand> _logger;

    public CreateFeedbackCommand(
      IFeedbackRepository feedbackRepository,
      IImageRepository imageRepository,
      ICreateFeedbackValidator validator,
      IHttpContextAccessor httpContextAccessor,
      IDbFeedbackMapper feedbackMapper,
      IDbImageMapper imageMapper,
      IResponseCreator responseCreator,
      ILogger<CreateFeedbackCommand> logger)
    {
      _feedbackRepository = feedbackRepository;
      _imageRepository = imageRepository;
      _validator = validator;
      _httpContextAccessor = httpContextAccessor;
      _feedbackMapper = feedbackMapper;
      _imageMapper = imageMapper;
      _responseCreator = responseCreator;
      _logger = logger;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateFeedbackRequest request)
    {
      //TODO: Fix
      var s = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
      _logger.LogInformation($"Remote IP is {s}");

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      Guid feedbackId = Guid.NewGuid();
      List<Guid> imageIds = await _imageRepository.CreateAsync(request.FeedbackImages
        .Select(ic => _imageMapper.Map(ic, feedbackId))
        .ToList());
      DbFeedback dbFeedback = _feedbackMapper.Map(request, imageIds, feedbackId);

      OperationResultResponse<Guid?> response = new();
      response.Body = await _feedbackRepository.CreateAsync(dbFeedback);

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response.Body is null
        ? _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest)
        : response;
    }
  }
}
