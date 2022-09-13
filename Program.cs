using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using SteamQueue.Context;
using SteamQueue.DTOs;
using SteamQueue.Entities;
using SteamQueue.Extensions;
using SteamQueue.Services.DatabaseInitialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
string sqlConnectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(sqlConnectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        });
});

builder.Services.AddAutoMapper(cfg => {
                cfg.AddExpressionMapping();
                cfg.AddCollectionMappers();
                cfg.CreateMap<AddPositionDtoBase, Position>().ReverseMap();
                cfg.CreateMap<AddAccountDto, SteamAccount>().ReverseMap();
                cfg.CreateMap<AddLineDto, Line>().ReverseMap();
            });

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddSwaggerConfig();

builder.Services.AddCorsConfig();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
    dbInitializer!.Initialize();
    dbInitializer.SeedData();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCustomSwaggerConfig();

app.UseAuthentication();
app.UseAuthorization();




app.MapRazorPages();
app.MapControllers();

app.Run();
