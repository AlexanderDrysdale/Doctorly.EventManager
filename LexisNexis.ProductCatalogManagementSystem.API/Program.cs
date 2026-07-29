using Doctorly.EventManager.Api.Application.UseCases;
using Doctorly.EventManager.Api.Infastructure.Config;
using Doctorly.EventManager.Api.Infastructure.Persistence;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Implementations;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Doctorly.EventManager.Api.Infastructure.Services.Implementations;
using Doctorly.EventManager.Api.Infastructure.Services.Interfaces;
using Doctorly.EventManager.Api.WebApi.Middlewares;
using Doctorly.EventManager.Application.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductCatalogDb"));

// Application Handlers
builder.Services.AddScoped<GetEventsHandler>();
builder.Services.AddScoped<GetEventByIdHandler>();
builder.Services.AddScoped<CreateEventHandler>();
builder.Services.AddScoped<UpdateEventHandler>();
builder.Services.AddScoped<DeleteEventHandler>();

// Domain/Application services
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<IEventSearchService, EventSearchService>();
builder.Services.AddSingleton<SearchCacheService>();

builder.Services.AddSingleton<SearchCacheService>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDev",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200", 
                "http://localhost:57522", 
                "http://localhost:5008",
                "https://localhost:7044",
                "http://localhost:5008") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin();
        });
});

var app = builder.Build();

app.UseMiddleware<RequestValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowDev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API v1");
    c.RoutePrefix = string.Empty; // Swagger at root (http://localhost:<port>/)
});

app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

app.Run();
