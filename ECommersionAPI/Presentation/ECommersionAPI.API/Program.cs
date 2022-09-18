using ECommersionAPI.Application.Validators.Products;
using ECommersionAPI.Infrastructure.Filters;
using ECommersionAPI.Persistence;
using ECommersionAPI.Application;
using FluentValidation.AspNetCore;
using ECommersionAPI.Infrastructure;
using ECommersionAPI.Infrastructure.Services.Storage.Azure;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(setupAction => setupAction.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistanceService();
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationServices();
//builder.Services.AddStorage(StorageType.Local);
builder.Services.AddStorage<AzureStorage>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
}));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin",options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience=true,
                        ValidateIssuer=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,
                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer= builder.Configuration["Token:Issuer"],
                        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
                    };
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
