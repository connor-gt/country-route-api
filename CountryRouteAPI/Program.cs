var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
