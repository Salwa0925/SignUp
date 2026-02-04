using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserApi;
using Microsoft.EntityFrameworkCore;


//The main program class for the User API application.
//Builds is .
var builder = WebApplication.CreateBuilder(args);

//AddOpenApi is an extension method that configures services for OpenAPI documentation.
builder.Services.AddOpenApi();

//AddControllers adds support for controllers in the application.
builder.Services.AddControllers();

//Registers the IUserService interface with its implementation UserService as a singleton service.
builder.Services.AddScoped<IUserService, UserService>();

//Adds support for API endpoint exploration and Swagger generation.
builder.Services.AddEndpointsApiExplorer();

//Adds Swagger generation services to the application.
builder.Services.AddSwaggerGen();


// JWT token authentication som kanskje, kanskje ikke vil funke :D
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!)),
        
        ValidateIssuer =true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true
    };
});

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite("Data Source=users.db"));

//Builds the application.
var app = builder.Build();
if(app.Environment.IsDevelopment())
{
    //Enables the developer exception page in the development environment.
    app.UseDeveloperExceptionPage();
    //Configures the HTTP request pipeline to use Swagger and Swagger UI.
    app.UseSwagger();

    //Configures the HTTP request pipeline to use Swagger UI.
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

//Maps controller routes to the application.
app.MapControllers();

//Runs the application.
app.Run();

