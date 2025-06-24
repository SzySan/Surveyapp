namespace SurveyApi.Dtos;

public class SurveyDto
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public List<QuestionDto> Questions { get; init; } = [];
}

public class CreateSurveyDto
{
    public string Title { get; set; } = default!;
    public List<CreateQuestionDto> Questions { get; set; } = [];
}

public class UpdateSurveyDto
{
    public string Title { get; set; } = default!;
}
