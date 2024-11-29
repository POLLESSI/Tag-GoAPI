using Tag_GoAPI.Tools;
using Tag_Go.DAL.Repositories;
using Tag_Go.DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Tag_Go.BLL.Services;
using Tag_GoAPI.Hubs;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tag_Go.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#nullable disable

// add constraint of route
builder.Services.Configure<RouteOptions>(options =>
{
    if (options.ConstraintMap.ContainsKey("int"))
    {
        options.ConstraintMap.Remove("int");
    }
    options.ConstraintMap.Add("int", typeof(Tag_GoAPI.IntRouteConstraint));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Authentication

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
//{
//    option.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenGenerator.secretKey)),
//        ValidateLifetime = true,
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

//SqlConnection

builder.Services.AddScoped<SqlConnection>(Sc => new SqlConnection(builder.Configuration.GetConnectionString("default")));

// Injections

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IAvatarService, AvatarService>();
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddScoped<IBonusService, BonusService>();
builder.Services.AddScoped<IBonusRepository, BonusRepository>();
builder.Services.AddScoped<IChatActivityService, ChatActivityService>();
builder.Services.AddScoped<IChatActivityRepository, ChatActivityRepository>();
builder.Services.AddScoped<IChatEvenementService, ChatEvenementService>();
builder.Services.AddScoped<IChatEvenementRepository, ChatEvenementRepository>();
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

builder.Services.AddControllers();

// Add Hubs

builder.Services.AddTransient<ActivityHub>();
builder.Services.AddTransient<AvatarHub>();
builder.Services.AddTransient<BonusHub>();
builder.Services.AddTransient<ChatActivityHub>();
builder.Services.AddTransient<ChatEvenementHub>();
builder.Services.AddTransient<MapHub>();
builder.Services.AddTransient<MediaItemHub>();
builder.Services.AddTransient<NEvenementHub>();
builder.Services.AddTransient<NIconHub>();
builder.Services.AddTransient<NPersonHub>();
builder.Services.AddTransient<NUserHub>();
builder.Services.AddTransient<NVoteHub>();
builder.Services.AddTransient<OrganisateurHub>();
builder.Services.AddTransient<RecompenseHub>();
builder.Services.AddTransient<WeatherForecastHub>();

// CORS Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200") //Adresse client Angular
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); //Nécessaire pour SignalR avec WebSocket
    });
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("adminpolicy", option => option.RequireRole("admin"));
    o.AddPolicy("modopolicy", option => option.RequireRole("admin", "modo"));
    o.AddPolicy("userpolicy", option => option.RequireAuthenticatedUser());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

//app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



// Token Generator

//builder.Services.AddScoped<TokenGenerator>();

// Security levels
// Declaration of the different security levels to be implemented in the controller using the attribute [Authorize("font_name")]

app.MapHub<ActivityHub>("/activityhub");
app.MapHub<AvatarHub>("/avatarhub");
app.MapHub<BonusHub>("/bonushub");
app.MapHub<ChatActivityHub>("/chatactivityhub");
app.MapHub<ChatEvenementHub>("/chatevenementhub");
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

app.Run();
