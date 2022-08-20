using LT.DigitalOffice.FeedbackService.Mappers.Models.Interfaces;
using LT.DigitalOffice.FeedbackService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto;
using System.Linq;

namespace LT.DigitalOffice.FeedbackService.Mappers.Responses
{
  public class FeedbackResponseMapper : IFeedbackResponseMapper
  {
    private readonly IFeedbackInfoMapper _mapper;
    private readonly IImageMapper _imageMapper;

    public FeedbackResponseMapper(
      IFeedbackInfoMapper mapper,
      IImageMapper imageMapper)
    {
      _mapper = mapper;
      _imageMapper = imageMapper;
    }

    public FeedbackResponse Map(DbFeedback dbFeedback)
    {
      return dbFeedback is null
        ? null
        : new FeedbackResponse()
        {
          Feedback = _mapper.Map(dbFeedback, dbFeedback.Images.Count),
          Images = dbFeedback.Images.Select(_imageMapper.Map).ToList()
        };
    }
  }
}
