#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/WhatToBuy.Api/WhatToBuy.Api.csproj", "Systems/Api/WhatToBuy.Api/"]
COPY ["Data/WhatToBuy.Context.Entities/WhatToBuy.Context.Entities.csproj", "Data/WhatToBuy.Context.Entities/"]
COPY ["Data/WhatToBuy.Context/WhatToBuy.Context.csproj", "Data/WhatToBuy.Context/"]
COPY ["Shared/WhatToBuy.Common/WhatToBuy.Common.csproj", "Shared/WhatToBuy.Common/"]
COPY ["Shared/WhatToBuy.Settings/WhatToBuy.Settings.csproj", "Shared/WhatToBuy.Settings/"]
COPY ["Services/WhatToBuy.EmailService/WhatToBuy.EmailService.csproj", "Services/WhatToBuy.EmailService/"]
COPY ["Services/WhatToBuy.Services.Families/WhatToBuy.Services.Families.csproj", "Services/WhatToBuy.Services.Families/"]
COPY ["Services/WhatToBuy.Services.Items/WhatToBuy.Services.Items.csproj", "Services/WhatToBuy.Services.Items/"]
COPY ["Services/WhatToBuy.Services.Settings/WhatToBuy.Services.Settings.csproj", "Services/WhatToBuy.Services.Settings/"]
COPY ["Services/WhatToBuy.Services.ShoppingList/WhatToBuy.Services.ShoppingLists.csproj", "Services/WhatToBuy.Services.ShoppingList/"]
COPY ["Services/WhatToBuy.Services.Users/WhatToBuy.Services.Users.csproj", "Services/WhatToBuy.Services.Users/"]
RUN dotnet restore "Systems/Api/WhatToBuy.Api/WhatToBuy.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/WhatToBuy.Api"
RUN dotnet build "WhatToBuy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatToBuy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatToBuy.Api.dll"]