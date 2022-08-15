using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Db;

namespace LT.DigitalOffice.FeedbackService.Data.Interfaces
{
  [AutoInject]
  public interface IImageRepository
  {
    Task<List<Guid>> CreateAsync(List<DbImage> dbImages);
  }
}
