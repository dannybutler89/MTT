using Domain;
using Domain.Helpers;
using Domain.Maps;
using Domain.Services;
using Domain.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MarkelDbContext>();
builder.Services.AddTransient<IDbService, DbService>();
builder.Services.AddTransient<IDbRepository, DbRepository>();
builder.Services.AddSingleton<DateHelper>();
builder.Services.AddAutoMapper(typeof(CompanyMappings));

builder.Services.AddValidatorsFromAssemblyContaining<UpdateClaimRequestValidator>();

var app = builder.Build();

using var scoped = app.Services.CreateScope();

var services = scoped.ServiceProvider;
var dbContext = services.GetRequiredService<MarkelDbContext>();
await DataInitializer.Initialize(dbContext);

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
