using LT.DigitalOffice.FeedbackService.Mappers.Models.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Models;

namespace LT.DigitalOffice.FeedbackService.Mappers.Models
{
  public class ImageMapper : IImageMapper
  {
    public ImageContent Map(DbImage dbImage)
    {
      return dbImage is null
        ? null
        : new()
        {
          Name = dbImage.Name,
          Content = dbImage.Content,
          Extension = dbImage.Extension
        };
    }
  }
}
