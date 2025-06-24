namespace SurveyApi.Dtos;

public class QuestionDto
{
    public int Id { get; init; }
    public string Text { get; init; } = default!;
}

public class CreateQuestionDto
{
    public string Text { get; set; } = default!;
}

public class UpdateQuestionDto
{
    public string Text { get; set; } = default!;
}
