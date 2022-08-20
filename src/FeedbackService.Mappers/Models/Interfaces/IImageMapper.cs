using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.FeedbackService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IImageMapper
  {
    ImageContent Map(DbImage dbImage);
  }
}
