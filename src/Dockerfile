#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release

RUN echo "Target: $TARGETARCH"
RUN echo "Configuration: $BUILD_CONFIGURATION"

WORKDIR /src
COPY ["MaintenanceLog/MaintenanceLog.csproj", "MaintenanceLog/"]
COPY ["MaintenanceLog.Common/MaintenanceLog.Common.csproj", "MaintenanceLog.Common/"]
COPY ["MaintenanceLog.Data/MaintenanceLog.Data.csproj", "MaintenanceLog.Data/"]
COPY ["MaintenanceLog.Client/MaintenanceLog.Client.csproj", "MaintenanceLog.Client/"]
COPY ["Directory.*.props", "."]
RUN dotnet restore "./MaintenanceLog/MaintenanceLog.csproj" -a $TARGETARCH
COPY . .
WORKDIR "/src/MaintenanceLog"
RUN dotnet build "./MaintenanceLog.csproj" -c $BUILD_CONFIGURATION -o /app/build -a $TARGETARCH

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
RUN dotnet publish "./MaintenanceLog.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceLog.dll"]
