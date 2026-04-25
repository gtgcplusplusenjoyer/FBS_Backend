using FBS.API.Extensions;
using FBS.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddAuthenticationAndJwt(builder.Configuration);
builder.Services.AddSwaggerWithJwtAuth();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

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