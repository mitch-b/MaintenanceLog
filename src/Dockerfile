# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0-jammy AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY MaintenanceLog/*.csproj MaintenanceLog/
COPY MaintenanceLog.Client/*.csproj MaintenanceLog.Client/
RUN dotnet restore MaintenanceLog/MaintenanceLog.csproj

# copy and build app and libraries
COPY MaintenanceLog/ MaintenanceLog/
COPY MaintenanceLog.Client/ MaintenanceLog.Client/

# test stage -- exposes optional entrypoint
# target entrypoint with: docker build --target test
##################
# FROM build AS test
# 
# COPY MaintenanceLog.Tests/*.csproj MaintenanceLog.Tests/
# WORKDIR /source/MaintenanceLog.Tests
# RUN dotnet restore
# 
# COPY MaintenanceLog.Tests/ .
# RUN dotnet build --no-restore
# 
# ENTRYPOINT ["dotnet", "test", "--logger:trx", "--no-build"]
##################

FROM build AS publish
WORKDIR /source/MaintenanceLog
RUN dotnet publish --no-restore -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/nightly/aspnet:8.0-jammy-chiseled-extra
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MaintenanceLog.dll"]