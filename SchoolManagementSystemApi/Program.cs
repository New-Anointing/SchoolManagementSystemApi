using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.ClassSubjects;
using SchoolManagementSystemApi.Services.Parent;
using SchoolManagementSystemApi.Services.RolesInitializer;
using SchoolManagementSystemApi.Services.SchoolEvents;
using SchoolManagementSystemApi.Services.SchoolRegistration;
using SchoolManagementSystemApi.Services.Student;
using SchoolManagementSystemApi.Services.StudentClass;
using SchoolManagementSystemApi.Services.Teacher;
using SchoolManagementSystemApi.Services.TimeTables;
using SchoolManagementSystemApi.Services.UserAuthentication;
using SchoolManagementSystemApi.Services.UserAuthorization;
using SchoolManagementSystemApi.Services.UserResolver;
using Swashbuckle.AspNetCore.Filters;

using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

///Connect interfaces with services
///
builder.Services.AddScoped<IRegServices, RegServices>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<IClassRoomServices, ClassRoomServices>();
builder.Services.AddScoped<IRolesDbInitializer, RolesDbInitializer>();
builder.Services.AddScoped<IUserResolverServices, UserResolverService>();
builder.Services.AddScoped<ISubjectsServices, SubjectsServices>();
builder.Services.AddScoped<ITimeTableServices, TimeTableServices>();
builder.Services.AddScoped<IEventsServices, EventsServices>();
builder.Services.AddScoped<ITeachersServices, TeachersServices>();
builder.Services.AddScoped<IStudentsServices, StudentsServices>();
builder.Services.AddScoped<IParentServices, ParentServices>();


//
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standrad Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management Syetem Api", Version="v1"});
});
builder.Services.AddDbContext<ApiDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("JwtTokens:Key").Value)),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });

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

app.UseDeveloperExceptionPage();

SeedDatabase();

app.Run();

void SeedDatabase()
{
    using(var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IRolesDbInitializer>();
        dbInitializer.Initialize();
    }
}
