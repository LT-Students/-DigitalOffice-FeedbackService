using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Data.Interfaces
{
  [AutoInject]
  public interface IFeedbackRepository
  {
    Task<(List<DbFeedback> dbFeedbacks, int totalCount)> FindAsync(FindFeedbacksFilter filter);
    Task<DbFeedback> GetAsync(Guid feedbackId);
    Task<Guid?> CreateAsync(DbFeedback dbFeedback);
  }
}
