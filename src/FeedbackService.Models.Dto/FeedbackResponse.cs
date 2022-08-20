using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using System.Collections.Generic;

namespace LT.DigitalOffice.FeedbackService.Models.Dto
{
  public record FeedbackResponse
  {
    public FeedbackInfo Feedback { get; set; }
    public List<ImageContent> Images { get; set; }
  }
}
