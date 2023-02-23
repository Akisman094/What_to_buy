using WhatToBuy.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();

// Add services to the container.
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddAppSwagger();
services.AddAppVersioning();

var app = builder.Build();

app.UseAppSwagger();

app.UseAppCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
