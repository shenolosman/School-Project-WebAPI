using GeoComment.Data;
using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<GeoService>();
// Add services to the container.
builder.Services.AddDbContext<GeoDbContext>(
    o => o.UseSqlServer(
        builder.Configuration.GetConnectionString("default")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(config =>
{
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.DefaultApiVersion = new ApiVersion(0, 1);
});

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

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider
        .GetRequiredService<GeoDbContext>();

    ctx.Database.EnsureCreated();
}

app.Run();
