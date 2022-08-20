using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Provider;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Data
{
  public class FeedbackRepository : IFeedbackRepository
  {
    private readonly IDataProvider _provider;

    #region private methods

    private IQueryable<DbFeedback> CreateFindPredicate(FindFeedbacksFilter filter)
    {
      IQueryable<DbFeedback> query = _provider.Feedbacks.AsQueryable();

      if (filter.FeedbackType.HasValue)
      {
        query = query.Where(f => f.Type == (int)filter.FeedbackType);
      }

      if (filter.FeedbackStatus.HasValue)
      {
        query = query.Where(f => f.Status == (int)filter.FeedbackStatus);
      }

      return query;
    }

    #endregion

    public FeedbackRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<(List<(DbFeedback dbFeedback, int imagesCount)> dbFeedbacks, int totalCount)> FindAsync(FindFeedbacksFilter filter)
    {
      if (filter is null)
      {
        return (null, 0);
      }

      IQueryable<DbFeedback> query = CreateFindPredicate(filter);

      int totalCount = await query.CountAsync();

      var dbFeedbacks = await (
        from f
        in query.Include(f => f.Images).Skip(filter.SkipCount).Take(filter.TakeCount).OrderByDescending(f => f.CreatedAtUtc)
        select new { Feedback = f, ImagesCount = f.Images.Count }).ToListAsync();

      return
        (dbFeedbacks.Select(f => (f.Feedback, f.ImagesCount)).ToList(),
        totalCount);
    }

    public Task<DbFeedback> GetAsync(Guid feedbackId)
    {
      return _provider.Feedbacks.Include(f => f.Images).FirstOrDefaultAsync(f => f.Id == feedbackId);
    }

    public async Task<Guid?> CreateAsync(DbFeedback dbFeedback)
    {
      if (dbFeedback is null)
      {
        return null;
      }

      _provider.Feedbacks.Add(dbFeedback);
      await _provider.SaveAsync();

      return dbFeedback.Id;
    }
  }
}
