using ContactBook.Data;
using ContactBook.Data.Entities;
using ContactBook.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(Options =>
        {
            Options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "Fast api",
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Scheme = "Bearer"
            });
            Options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
            {
                { 
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
                    },
                    new  string[]{}
                
                }
            });
        });

 



        builder.Services.AddDbContext<ContactContext>( options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
        });

        builder.Services.AddScoped<IContactService, ContactService>();
         builder.Services.AddScoped<IAuthService, AuthServices>();

//Authentication scheme setup
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
    };


});
builder.Services.AddIdentity<AppUser, IdentityRole>(Options =>
{
    //Options.Password.RequiredUniqueChars = 0;
    //Options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<ContactContext>();



var app = builder.Build();  

        // Configure the HTTP request pipeline.
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
