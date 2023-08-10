using Microsoft.EntityFrameworkCore;
using Search_Statistics;
using StarTrack_Work_Assessment.Utilities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Search_Statistics_Context>(options => options.UseSqlite(SearchStatisticsConfigurations.configuration.GetConnectionString("SearchStatisticsDB:SQLite")));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
