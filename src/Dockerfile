#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MaintenanceLog/MaintenanceLog.csproj", "MaintenanceLog/"]
COPY ["MaintenanceLog.Client/MaintenanceLog.Client.csproj", "MaintenanceLog.Client/"]
RUN dotnet restore "./MaintenanceLog/MaintenanceLog.csproj"

COPY . .
WORKDIR "/src/MaintenanceLog"
RUN dotnet build "./MaintenanceLog.csproj" -c $BUILD_CONFIGURATION -o /app/build

# test stage -- exposes optional entrypoint
# target entrypoint with: docker build --target test
##################
# FROM build AS test
# 
# COPY MaintenanceLog.Tests/*.csproj MaintenanceLog.Tests/
# WORKDIR /src/MaintenanceLog.Tests
# RUN dotnet restore "./MaintenanceLog.Tests.csproj"
# 
# COPY MaintenanceLog.Tests/ .
# RUN dotnet build --no-restore
# 
# ENTRYPOINT ["dotnet", "test", "--logger:trx", "--no-build"]
##################

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MaintenanceLog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceLog.dll"]