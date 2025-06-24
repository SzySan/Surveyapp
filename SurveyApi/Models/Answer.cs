namespace SurveyApi.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Value { get; set; } = string.Empty;
    public int ResponseId { get; set; }
}
