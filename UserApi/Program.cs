

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


//Maps controller routes to the application.
app.MapControllers();

//Runs the application.
app.Run();

