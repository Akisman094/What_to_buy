using WhatToBuy.Api.Configuration;
using WhatToBuy.Context;

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

services.AddAppDbContext();

var app = builder.Build();

app.UseAppSwagger();

app.UseAppCors();
app.UseAuthorization();

app.MapControllers();

DbInitializer.Execute(app.Services);

app.Run();
