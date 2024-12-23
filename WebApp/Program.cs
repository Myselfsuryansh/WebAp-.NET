
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using WebApp.DataBaseConfiguration;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("localDb")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApp",
        Version = "v1",
        Description = "API using .NET 6"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    options.TagActionsBy(api =>
    {
        
        var controllerName = api.ActionDescriptor.RouteValues["controller"];

        
        if (controllerName != null && (controllerName.Contains("Login", StringComparison.OrdinalIgnoreCase) ||
                                       controllerName.Contains("Register", StringComparison.OrdinalIgnoreCase)))
        {
            return new[] { "Authentication" };
        }

        if (controllerName != null && controllerName.Contains("Authorize", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Authorization" };
        }

        
        if (controllerName != null && controllerName.Contains("Student", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Students" };
        }

        if (controllerName != null && controllerName.Contains("Product", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Product" };
        }
        if (controllerName != null && controllerName.Contains("Aadministrator", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Aadministrator" };
        }
        if (controllerName != null && controllerName.Contains("Banking", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Banking" };
        }
        if (controllerName != null && controllerName.Contains("Patient", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Patient" };
        }

        if (controllerName != null && controllerName.Contains("Doctor", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Doctors" };
        }

        if (controllerName != null && controllerName.Contains("Nurse", StringComparison.OrdinalIgnoreCase))
        {
            return new[] { "Nurses" };
        }

        return new[] { "General API" };
    });

    //options.TagActionsBy(api => api.RelativePath.Contains("Login") ? new[] { "Authentication" } :
    //                           api.RelativePath.Contains("Authorize") ? new[] { "Authorization" } :
    //                           new[] { "General API" });
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings")?.Get<JwtSettings>();

    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtSettings?.Issuer,
        ValidAudience = jwtSettings?.Audience,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),


    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
