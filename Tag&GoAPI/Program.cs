using Tag_GoAPI.Tools;
using Tag_Go.DAL.Repositories;
using Tag_Go.DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Tag_Go.BLL;
using Tag_Go.BLL.Services;
using Tag_GoAPI.Hubs;
using System.Data;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Tag_Go.Models.Services;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Tag_Go.BLL.Interfaces;
using Tag_GoAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#nullable disable

////add constraint of route
builder.Services.Configure<RouteOptions>(options =>
{
    if (options.ConstraintMap.ContainsKey("int"))
    {
        options.ConstraintMap.Remove("int");
    }
    options.ConstraintMap.Add("int", typeof(Tag_GoAPI.IntRouteConstraint));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(o => o.AddPolicy("mypolicy", options => options.WithOrigins("http://localhost:7069"/*, "http://localhost:"*/)
//                        .AllowCredentials()
//                        .AllowAnyHeader()
//                        .AllowAnyMethod()));

builder.Services.AddCors();

//SqlConnection

builder.Services.AddScoped<SqlConnection>(Sc => new SqlConnection(builder.Configuration.GetConnectionString("default")));

// Injections

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IAvatarService, AvatarService>();
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddScoped<IBonusService, BonusService>();
builder.Services.AddScoped<IBonusRepository, BonusRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IMapService, MapService>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IMediaItemService, MediaItemService>();
builder.Services.AddScoped<IMediaItemRepository, MediaItemRepository>();
builder.Services.AddScoped<INEvenementService, NEvenementService>();
builder.Services.AddScoped<INEvenementRepository, NEvenementRepository>();
builder.Services.AddScoped<INIconService, NIconService>();
builder.Services.AddScoped<INIconRepository, NIconRepository>();
builder.Services.AddScoped<INPersonService, NPersonService>();
builder.Services.AddScoped<INPersonRepository, NPersonRepository>();
builder.Services.AddScoped<INUserService, NUserService>();
builder.Services.AddScoped<INUserRepository, NUserRepository>();
builder.Services.AddScoped<INVoteService, NVoteService>();
builder.Services.AddScoped<INVoteRepository, NVoteRepository>();
builder.Services.AddScoped<IOrganisateurService, OrganisateurService>();
builder.Services.AddScoped<IOrganisateurRepository, OrganisateurRepository>();
builder.Services.AddScoped<IRecompenseService, RecompenseService>();
builder.Services.AddScoped<IRecompenseRepository, RecompenseRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

// Add SignalR

builder.Services.AddSignalR();

// Add Hubs

builder.Services.AddSingleton<ActivityHub>();
builder.Services.AddSingleton<AvatarHub>();
builder.Services.AddSingleton<BonusHub>();
builder.Services.AddSingleton<ChatHub>();
builder.Services.AddSingleton<MapHub>();
builder.Services.AddSingleton<MediaItemHub>();
builder.Services.AddSingleton<NEvenementHub>();
builder.Services.AddSingleton<NIconHub>();
builder.Services.AddSingleton<NPersonHub>();
builder.Services.AddSingleton<NUserHub>();
builder.Services.AddSingleton<NVoteHub>();
builder.Services.AddSingleton<OrganisateurHub>();
builder.Services.AddSingleton<RecompenseHub>();
builder.Services.AddSingleton<WeatherForecastHub>();

// Token Generator

builder.Services.AddScoped<TokenGenerator>();

// Security levels
// Declaration of the different security levels to be implemented in the controller using the attribute [Authorize("font_name")]

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("adminpolicy", option => option.RequireRole("admin"));
    o.AddPolicy("modopolicy", option => option.RequireRole("admin", "modo"));
    o.AddPolicy("userpolicy", option => option.RequireAuthenticatedUser());
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
//{
//    option.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenGenerator.secretKey)),
//        ValidateLifetime = true,
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        //ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.MapControllerRoute(
//name: "default",
//pattern: "{controller-Home}/{action=Index}/Iid?}"
//   );

app.MapControllers();

app.MapHub<ActivityHub>("/activityhub");
app.MapHub<AvatarHub>("/avatarhub");
app.MapHub<BonusHub>("/bonushub");
app.MapHub<ChatHub>("/chathub");
app.MapHub<MapHub>("/maphub");
app.MapHub<MediaItemHub>("/mediaItemhub");
app.MapHub<NEvenementHub>("/nevenementhub");
app.MapHub<NIconHub>("/niconhub");
app.MapHub<NPersonHub>("/npersonhub");
app.MapHub<NUserHub>("/nuserhub");
app.MapHub<NVoteHub>("/nvotehub");
app.MapHub<OrganisateurHub>("/organisateurhub");
app.MapHub<RecompenseHub>("/recompensehub");
app.MapHub<WeatherForecastHub>("/weatherforecast");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id:int?}");


app.Run();
