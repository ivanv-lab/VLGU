using System.Reflection;
using System.Text;
using AdvertisingAgency.Data;
using AdvertisingAgency.Model.Authorization;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingAgency.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AdvertisingDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults
            .AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults
            .AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy
        .RequireRole("Admin"));
    options.AddPolicy("RequireManagerOrAdmin", policy => policy
        .RequireRole("Manager", "Admin"));
});

builder.Services.AddControllers();

builder.Services.AddScoped<AdvertisingDbContext>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<AuthService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Advertising Company API",
        Version = "v1",
        Description = "API для управления рекламной компанией",
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Ruslana Golubeva",
            Email = "golubevaruslana33@yandex.ru"
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.UseAllOfToExtendReferenceSchemas();
});

//builder.Services.AddScoped<AdvertisingDbContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await DbInitializer.initialize(scope.ServiceProvider,
        app.Configuration);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Advertising Company API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Advertising Company API Documentation";
        c.DefaultModelsExpandDepth(2);
        c.DefaultModelExpandDepth(2);
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
