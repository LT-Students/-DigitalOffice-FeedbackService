using FluentValidation;
using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.FeedbackService.Validation.Feedback.Interfaces;

namespace LT.DigitalOffice.FeedbackService.Validation.Feedback
{
  public class EditFeedbackStatusValidator : AbstractValidator<EditFeedbackStatusesRequest>, IEditFeedbackStatusValidator
  {
    public EditFeedbackStatusValidator(IFeedbackRepository repository)
    {
      CascadeMode = CascadeMode.Stop;

      RuleFor(f => f.FeedbackIds)
        .Must(f => f.Count > 0)
        .WithMessage("Feedback ids must be specified.");

      RuleFor(f => f.Status)
        .IsInEnum()
        .WithMessage("Incorrect feedback status.");

      RuleFor(r => r)
        .MustAsync(async (r, _) => !await repository.HaveSameStatusAsync(r.FeedbackIds, r.Status))
        .WithMessage("Some of feedbacks already has specified status.");
    }
  }
}
