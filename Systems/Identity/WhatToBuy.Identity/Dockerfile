#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/WhatToBuy.Identity/WhatToBuy.Identity.csproj", "Systems/Identity/WhatToBuy.Identity/"]
COPY ["Data/WhatToBuy.Context.Entities/WhatToBuy.Context.Entities.csproj", "Data/WhatToBuy.Context.Entities/"]
COPY ["Data/WhatToBuy.Context/WhatToBuy.Context.csproj", "Data/WhatToBuy.Context/"]
COPY ["Shared/WhatToBuy.Common/WhatToBuy.Common.csproj", "Shared/WhatToBuy.Common/"]
COPY ["Shared/WhatToBuy.Settings/WhatToBuy.Settings.csproj", "Shared/WhatToBuy.Settings/"]
RUN dotnet restore "Systems/Identity/WhatToBuy.Identity/WhatToBuy.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/WhatToBuy.Identity"
RUN dotnet build "WhatToBuy.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatToBuy.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatToBuy.Identity.dll"]