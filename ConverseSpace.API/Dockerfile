﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConverseSpace.API/ConverseSpace.API.csproj", "ConverseSpace.API/"]
RUN dotnet restore "ConverseSpace.API/ConverseSpace.API.csproj"
COPY . .
WORKDIR "/src/ConverseSpace.API"
RUN dotnet build "ConverseSpace.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ConverseSpace.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER root 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConverseSpace.API.dll"]