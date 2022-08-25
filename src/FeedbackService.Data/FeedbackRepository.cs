using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Provider;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
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

      if (filter.OrderByDescending)
      {
        query = query.OrderByDescending(f => f.CreatedAtUtc);
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

      IEnumerable<(DbFeedback Feedback, int ImagesCount)> dbfeedbacks = query
        .Skip(filter.SkipCount)
        .Take(filter.TakeCount)
        .Select(f => new { Feedback = f, ImagesCount = f.Images.Count })
        .AsEnumerable()
        .Select(f => (f.Feedback, f.ImagesCount));

      return (dbfeedbacks.ToList(), totalCount);
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

    public async Task<bool> EditStatusesAsync(List<Guid> feedbacksIds, FeedbackStatusType status)
    {
      IQueryable<DbFeedback> dbFeedbacks = _provider.Feedbacks.Where(f => feedbacksIds.Contains(f.Id));
      await dbFeedbacks.ForEachAsync(f => f.Status = (int)status);
      await _provider.SaveAsync();

      return true;
    }

    public Task<bool> HaveSameStatusAsync(List<Guid> feedbackIds, FeedbackStatusType status)
    {
      return _provider.Feedbacks.AnyAsync(f => feedbackIds.Contains(f.Id) && f.Status == (int)status);
    }
  }
}
