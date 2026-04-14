using AdvertisingAgency.Data;
using AdvertisingAgency.Repository;
using AdvertisingAgency.Repository.Authorization;
using AdvertisingApplication.Data;
using AdvertisingApplication.Model.Authorization;
using AdvertisingApplication.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AdvertisingDbContext>();
builder.Services.AddTransient<RoleRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<TaskStatusRepository>();
builder.Services.AddTransient<TaskRepository>();
builder.Services.AddTransient<ClientRepository>();
builder.Services.AddTransient<CampaignStatusRepository>();
builder.Services.AddTransient<CampaignCategoryRepository>();
builder.Services.AddTransient<AdvertisingCampaignRepository>();
builder.Services.AddTransient<AuthService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await DbInitializer.initialize(scope.ServiceProvider,
        app.Configuration);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();
app.Run();
