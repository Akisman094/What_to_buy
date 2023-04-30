using WhatToBuy.Api;
using WhatToBuy.Api.Configuration;
using WhatToBuy.Context;
using WhatToBuy.Context.Setup;
using WhatToBuy.Services.Settings;
using WhatToBuy.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var identitySettings = Settings.Load<IdentitySettings>("Identity");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

// Add services to the container.
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();
services.AddEndpointsApiExplorer();
services.AddAppSwagger(swaggerSettings, identitySettings);
services.AddAppVersioning();
services.AddAppAutoMappers();
services.AddAppHealthChecks();
services.AddAppAuth(identitySettings);
services.AddAppDbContext();
services.AddAppController();
services.RegisterAppServices();

var app = builder.Build();

app.UseAppSwagger(swaggerSettings);
app.UseAppCors();
app.UseAuthorization();
app.UseAppMiddlewares();
app.UseAppHealthChecks();
app.UseAppController();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

app.Run();
