using System.Text.Json.Serialization;
using webApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Logging 
builder.WebHost.ConfigureLogging((hostingContext, logger) =>
{
    logger.AddFile($"{Directory.GetCurrentDirectory()}/Logs/log.txt");
});

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions((option) =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


// Add Context
builder.Services.AddDbContext<ShopContext>();

// Add Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

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