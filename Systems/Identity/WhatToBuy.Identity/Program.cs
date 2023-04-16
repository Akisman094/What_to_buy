using WhatToBuy.Identity.Configuration;
using WhatToBuy.Context;
using WhatToBuy.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext();

services.RegisterAppServices();

services.AddAppHealthChecks();

services.AddAppIdentityServer();

var app = builder.Build();

app.UseAppCors();

app.UseIdentityServer();

app.UseAppHealthChecks();

app.Run();
