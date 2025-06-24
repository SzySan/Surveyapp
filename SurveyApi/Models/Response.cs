namespace SurveyApi.Models;

public class Response
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string UserIdentifier { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public List<Answer> Answers { get; set; } = new();
}
