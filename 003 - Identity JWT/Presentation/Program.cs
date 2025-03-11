using Applications.Interfaces;
using Applications.Services;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.Identity.Configurations;
using Infrastructure.Identity.Hashing;
using Infrastructure.Identity.Jwt;
using System.Text;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories.RoleRepository;
using Infrastructure.Persistence.Repositories.UnitOfWork;
using Infrastructure.Persistence.Repositories.UserRepository;
using Infrastructure.Persistence.Repositories.UserRoleRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Applications.Mappings.UserProfile;
using Applications.Mappings.UserRoleProfile;
using Applications.Mappings.RoleProfile;
using AspectCore.Extensions.DependencyInjection;
using Applications.DependencyInjection.AspectCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserRoleService,UserRoleService>();
builder.Services.AddScoped<IUserRoleRepository,UserRoleRepository>();

builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(UserRoleProfile));
builder.Services.AddAutoMapper(typeof(RoleProfile));




builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddSingleton<JwtSettings>();
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });



builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
builder.Services.AddAspectCoreServices();



builder.Services.AddDbContext<TrinkappContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Trinkapp")));


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kimlik Doğrulama Middleware'i
app.UseAuthentication();

// Yetkilendirme Middleware'i
app.UseAuthorization();

app.MapControllers();
app.Run();
