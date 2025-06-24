using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyApi.Data;
using SurveyApi.Dtos;
using SurveyApi.Models;

[ApiController]
[Route("api/[controller]")]
public class SurveysController(AppDbContext db, IMapper mapper) : ControllerBase
{
    /* ---------- CRUD: Surveys ---------- */

    // GET /api/surveys
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SurveyDto>>> GetAll()
    {
        var list = await db.Surveys.Include(s => s.Questions).ToListAsync();
        return Ok(mapper.Map<IEnumerable<SurveyDto>>(list));
    }

    // GET /api/surveys/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SurveyDto>> Get(int id)
    {
        var survey = await db.Surveys.Include(s => s.Questions)
                                     .FirstOrDefaultAsync(s => s.Id == id);
        return survey is null
            ? NotFound()
            : Ok(mapper.Map<SurveyDto>(survey));
    }

    // POST /api/surveys
    [HttpPost]
    public async Task<ActionResult<SurveyDto>> Create(CreateSurveyDto dto)
    {
        var survey = mapper.Map<Survey>(dto);
        db.Surveys.Add(survey);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get),
            new { id = survey.Id },
            mapper.Map<SurveyDto>(survey));
    }

    // PUT /api/surveys/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSurveyDto dto)
    {
        var survey = await db.Surveys.FindAsync(id);
        if (survey is null) return NotFound();

        mapper.Map(dto, survey);
        await db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/surveys/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var survey = await db.Surveys.FindAsync(id);
        if (survey is null) return NotFound();

        db.Surveys.Remove(survey);
        await db.SaveChangesAsync();
        return NoContent();
    }

    /* ---------- CRUD: Questions (scoped pod danym survey) ---------- */

    // POST /api/surveys/5/questions
    [HttpPost("{surveyId:int}/questions")]
    public async Task<ActionResult<QuestionDto>> AddQuestion(
        int surveyId, CreateQuestionDto dto)
    {
        var survey = await db.Surveys.FindAsync(surveyId);
        if (survey is null) return NotFound("Survey not found");

        var q = mapper.Map<Question>(dto);
        q.SurveyId = surveyId;

        db.Questions.Add(q);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuestion),
            new { surveyId, questionId = q.Id },
            mapper.Map<QuestionDto>(q));
    }

    // GET /api/surveys/5/questions/3
    [HttpGet("{surveyId:int}/questions/{questionId:int}")]
    public async Task<ActionResult<QuestionDto>> GetQuestion(int surveyId, int questionId)
    {
        var q = await db.Questions
                        .FirstOrDefaultAsync(x => x.Id == questionId && x.SurveyId == surveyId);
        return q is null ? NotFound() : Ok(mapper.Map<QuestionDto>(q));
    }

    // PUT /api/surveys/5/questions/3
    [HttpPut("{surveyId:int}/questions/{questionId:int}")]
    public async Task<IActionResult> UpdateQuestion(
        int surveyId, int questionId, UpdateQuestionDto dto)
    {
        var q = await db.Questions
                        .FirstOrDefaultAsync(x => x.Id == questionId && x.SurveyId == surveyId);
        if (q is null) return NotFound();

        mapper.Map(dto, q);
        await db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /api/surveys/5/questions/3
    [HttpDelete("{surveyId:int}/questions/{questionId:int}")]
    public async Task<IActionResult> DeleteQuestion(int surveyId, int questionId)
    {
        var q = await db.Questions
                        .FirstOrDefaultAsync(x => x.Id == questionId && x.SurveyId == surveyId);
        if (q is null) return NotFound();

        db.Questions.Remove(q);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
