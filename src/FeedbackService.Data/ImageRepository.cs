using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Provider;
using LT.DigitalOffice.FeedbackService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.FeedbackService.Data
{
  public class ImageRepository : IImageRepository
  {
    private readonly IDataProvider _provider;

    public ImageRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbImage> dbImages)
    {
      if (dbImages is null || !dbImages.Any())
      {
        return null;
      }

      await _provider.Images.AddRangeAsync(dbImages);

      return dbImages.Select(i => i.Id).ToList();
    }
  }
}
