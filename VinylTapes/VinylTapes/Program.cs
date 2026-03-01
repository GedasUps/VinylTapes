using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VinylTapes.Data;
using VinylTapes.Services; 

var builder = WebApplication.CreateBuilder(args);

// 1. Registruojame DB
builder.Services.AddDbContext<VinylDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.UseVector()));
builder.Services.AddCors(options =>
{
    options.AddPolicy("LaisvasCORS", policy =>
    {
        policy.AllowAnyOrigin() // Leidžia bet kokį adresą (debug tikslu)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// 2. REGISTRUOJAME DISCOGS SERVISĄ (Būtina!)
builder.Services.AddSingleton<VectorService>();
builder.Services.AddHttpClient<DiscogsService>();
builder.Services.AddHttpClient<DiscogsAuthService>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "VinylTapesApp/1.0");
});


builder.Services.AddControllers();

// 3. Pridedame Swagger generavimą
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
var app = builder.Build();

// 4. Įjungiame Swagger UI (Development aplinkoje)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Tai įgalins http://localhost:5195/swagger
}
app.UseCors("LaisvasCORS"); 
app.UseHttpsRedirection();
app.UseAuthentication(); // BŪTINA PRIEŠ UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();