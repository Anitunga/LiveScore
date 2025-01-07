using LiveScore.Data;
using LiveScore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Register all your services here
// AddScoped creates a new instance of the service for each HTTP request. 
// AddSingleton creates a single instance of the service that is shared throughout the entire application. 
// AddTransient creates a new instance of the service every time it is requested. Are short-lived and are created every time they are needed.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<QuarterService>();
builder.Services.AddScoped<FoulService>();
builder.Services.AddScoped<SubstitutionService>();
builder.Services.AddScoped<CoachService>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add services to the container
builder.Services.AddHttpClient(); // This registers IHttpClientFactory
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add services to the container.
builder.Services.AddDbContext<LiveScoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateActor = true, //idnetify parts
        ValidateAudience = true, // avoid attacks (a website can't use the same token twice)
        ValidateLifetime = true, //limit the lifetime of the token
        ValidateIssuerSigningKey = true, //ask the user to sign
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //token sender 
        ValidAudience = builder.Configuration["Jwt:Audience"], //token receiver
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), //Encrypt key
        ClockSkew = TimeSpan.Zero //by default it's 5 min. Represent the authorized time between the client and the server
    };
});

builder.Services.AddAuthorization();  // Ensure authorization services are added.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Basketball LiveScore API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});


builder.Services.AddSignalR();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basketball LiveScore API V1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//Auth(Respect the Order)
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
