using GeoComment.AuthHandlers;
using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Options;
using GeoComment.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GeoService>();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<GeoDbContext>();

builder.Services.AddDbContext<GeoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(0, 1);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ApiVersionReader =
        new QueryStringApiVersionReader("api-version");
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("BasicAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Basic scheme."
    });

    options.SwaggerDoc("v0.1", new OpenApiInfo
    {
        Title = "GeoComment v0.1",
        Version = "v0.1"
    });

    options.SwaggerDoc("v0.2", new OpenApiInfo
    {
        Title = "GeoComment v0.2",
        Version = "v0.2"
    });

    options.OperationFilter<MySwaggerOperationsFilter>();
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint($"/swagger/v0.1/swagger.json", "v0.1");
        o.SwaggerEndpoint($"/swagger/v0.2/swagger.json", "v0.2");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider
        .GetRequiredService<GeoService>();

    await database.ResetDb();
}

app.Run();