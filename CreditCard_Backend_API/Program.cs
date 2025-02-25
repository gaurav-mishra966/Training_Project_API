using System.Text;
using CreditCard_Backend_API.Data;
using CreditCard_Backend_API.Repositories;
using CreditCard_Backend_API.Repositories.Implementation;
using CreditCard_Backend_API.Repositories.Interface;
using CreditCard_Backend_API.Repositories.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//Adding CORS services to the application for making interation with UI Components
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        else
        {
            policy.WithOrigins("http://localhost:4200/","*", "http://localhost:4200/add-product")
            .AllowAnyMethod()
            .AllowAnyHeader();
        }

    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecting AddDbContext with a type ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CreditApplicationConnectionString"));
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    //change if you want that user and roles are to be in seperate database
    options.UseSqlServer(builder.Configuration.GetConnectionString("CreditApplicationConnectionString"));
});


builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IServicesRepository, ServiceRepository>();

builder.Services.AddScoped<UserService>();
//Adding Dependencies to the servies for role based identity providers
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CreditCardApp")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

//identity opytions
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 1;
});

//authentication and token validation
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            AuthenticationType = "Jwt",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwagger();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Credit Card Backend API V1");
    //    c.RoutePrefix = string.Empty;
    //});
}

app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
