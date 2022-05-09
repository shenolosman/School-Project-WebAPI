using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Options;
using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<GeoService>();
// Add services to the container.
builder.Services.AddDbContext<GeoDbContext>(
    o => o.UseSqlServer(
        builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<GeoDbContext>();

//Options for Basic Auth 
//***Start***
builder.Services.AddAuthentication();
//***End***

//Services for Version control
// ***Start***
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    //Options for Basic Auth 
    //***Start***
    x.AddSecurityDefinition("BasicAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Basic scheme."
    });
    x.OperationFilter<SecurityRequirementsOperationFilter>();
    //***End***
    x.OperationFilter<MySwaggerOperationsFilter>();
});
builder.Services.ConfigureOptions<ConfigureSwaggerOption>();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(0, 1);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new QueryStringApiVersionReader("api-version");
});
builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "api-version";
    o.SubstituteApiVersionInUrl = true;
});
// ***End***

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        var provide = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provide.ApiVersionDescriptions)
        {
            option.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.ApiVersion.ToString()
            );
        }

    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider
        .GetRequiredService<GeoDbContext>();

    ctx.Database.EnsureCreated();
}

app.Run();