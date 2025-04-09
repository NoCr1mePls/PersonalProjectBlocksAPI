using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectBlocksAPI.Services;
using SmartHealth.WebApi.Interfaces.Services;

//Get the connection string from the user secrets
var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot config = new ConfigurationBuilder()
     .AddUserSecrets<Program>()
     .Build();
string sqlConnectionString = config["ConnectionString"];
if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");

// Adding the HTTP Context accessor to be injected. This is needed by the AspNetIdentityUserRepository
// to resolve the current user.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();

//Add the database communication as service
builder.Services.AddTransient<IDatabaseRepository, DatabaseCommunicationService>(o => new DatabaseCommunicationService(sqlConnectionString));

//Add authorisation, rules and dapper
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 10;

    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 10;
}).AddDapperStores(options =>
{ 
    options.ConnectionString = sqlConnectionString;
});

//Add BearerToken options
builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options =>
{
    options.BearerTokenExpiration = TimeSpan.FromMinutes(60);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthorization();
app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.MapPost("/account/logout", //WIP
    async (SignInManager<IdentityUser> signInManager,
    [FromBody] object empty) =>
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    }).RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
