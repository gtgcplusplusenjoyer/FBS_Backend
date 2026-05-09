using DotNetEnv;
using FBS.API.Extensions;
using FBS.Application.Extensions;


var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    Env.Load(); 
    builder.Configuration.AddEnvironmentVariables();
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddValidation();
builder.Services.AddAuthenticationAndJwt(builder.Configuration);
builder.Services.AddSwaggerWithJwtAuth();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.ApplyMigration(); // когда отдаем докер раскомментируем

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();