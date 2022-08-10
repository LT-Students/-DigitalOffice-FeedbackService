using FluentValidation;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;

namespace LT.DigitalOffice.FeedbackService.Validation.Feedback
{
  public class CreateFeedbackValidator : AbstractValidator<CreateFeedbackRequest>, ICreateFeedbackValidator
  {
    public CreateFeedbackValidator()
    {
      CascadeMode = CascadeMode.Stop;

      RuleFor(f => f.Type)
        .IsInEnum();
    }
  }
}
