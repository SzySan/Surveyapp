using AutoMapper;
using SurveyApi.Models;
using SurveyApi.Dtos;

namespace SurveyApi.Profiles;

public class SurveyProfile : Profile
{
    public SurveyProfile()
    {
        CreateMap<Survey, SurveyDto>();
        CreateMap<CreateSurveyDto, Survey>();
        CreateMap<UpdateSurveyDto, Survey>();

        CreateMap<Question, QuestionDto>();
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<UpdateQuestionDto, Question>();
    }
}

