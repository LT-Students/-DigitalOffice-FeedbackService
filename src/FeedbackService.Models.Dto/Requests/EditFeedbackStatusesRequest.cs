using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Requests
{
  public record EditFeedbackStatusesRequest
  {
    [Required]
    public List<Guid> FeedbackIds;

    [Required]
    public FeedbackStatusType Status;
  }
}
