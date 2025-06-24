using Microsoft.EntityFrameworkCore;
using SurveyApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SurveyApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Response> Responses => Set<Response>();
    public DbSet<Answer> Answers => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Question>()
         .HasOne<Survey>()
         .WithMany(s => s.Questions)
         .HasForeignKey(q => q.SurveyId);

        b.Entity<Answer>()
         .HasOne<Response>()
         .WithMany(r => r.Answers)
         .HasForeignKey(a => a.ResponseId);
    }
}