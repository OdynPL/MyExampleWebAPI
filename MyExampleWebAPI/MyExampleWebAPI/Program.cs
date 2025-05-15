using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MyExampleWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Additional JSON parameters, options and configurator
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

// Allow API endpoints exploring
builder.Services.AddEndpointsApiExplorer();

// Register all additional/custom services used
builder.Services.AddScoped<IMemberService, MemberService>();

// Register repositories
builder.Services.AddScoped<IMemberRepository, MemberRepository>(); // Rejestracja repozytorium

// Allow CORS for developement
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
});


// Register Entity Framework Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add OpenAPI (Swagger)
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Pass our DB Context for EF CORE ORM
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Execute automatical migrations
    dbContext.Database.Migrate();
    // Init Seed Data
    SeedData.Initialize(dbContext);  
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Additional API options
app.UseCors("AllowAll");
app.UseHttpsRedirection(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
