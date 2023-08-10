using Microsoft.EntityFrameworkCore;
using StackExchangeQueryTracker.Utilities;
using SearchStatisticsDB;
using SearchStatisticsDB.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISearchStatisticsDBRepository, SearchStatisticsDBRepository>();
builder.Services.AddDbContext<SearchStatisticsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SearchStatisticsDB:SQLite")));

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
