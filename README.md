# Home Maintenance Log

Self-hosted web app for logging maintenance tasks around the house.

* [Screenshots](#screenshots)
* [Getting Started](#getting-started)
* [Features](#features)
* [Technical Things](#technical-things)
* [Possible Features](#possible-features)
* [Development](#development)
* [License](#license)

## Screenshots

_To do..._

## Getting Started

To quickstart with Docker,

```bash
docker run -d -p 8080:8080 ghcr.io/mitch-b/maintenance-log:latest
```

Then open your browser to `http://localhost:8080`.

To run with a sidecar SQL database container, use the provided [`docker-compose.yml`](./docker-compose.yml) file.

## Features

* Add a new log entry
  * Task, datetime, description, duration, notes ...
  * Can log parts/products used
* Set reminders for future maintenance (?)

## Technical Things

* .NET 8 Blazor Web App (SSR+WASM rendering)
* Entity Framework Core
* SQL Server (or other)
* Containers
* GitHub Actions (to CI/CD container images)

## Possible Features

* Expose as sensors to Home Assistant (?) via MQTT (?)
* Multiple homes
* SSO/OAuth
* Webhooks for events

## Development

If you're a GitHub user, try in Codespaces!

When in Codespaces, the database starts automatically, so you're instantly ready to go.

After setting up Codespaces secrets in your repo in GitHub, you can apply secrets.

```bash
cd src/MaintenanceLog
dotnet user-secrets set EmailConfig:SmtpUser $EMAILCONFIG_SMTPUSER
dotnet user-secrets set EmailConfig:SmtpPass $EMAILCONFIG_SMTPPASS
dotnet user-secrets set EmailConfig:SmtpHost $EMAILCONFIG_SMTPHOST
dotnet user-secrets set EmailConfig:SmtpPort $EMAILCONFIG_SMTPPORT
```

To use `sqlite`, use the following appsettings:

```json
{
  "ConnectionStrings": {
    "MaintenanceLogDb": "DataSource=/data/maintenancelogdb.db;Cache=Shared"
  },
  "DbProvider": "sqlite"
}
```

To use `mssql` in a sidecar container in the Codespace, use the following appsettings:

```json
{
  "ConnectionStrings": {
    "MaintenanceLogDb": "Server=db;Database=maintenancelogdb;User Id=sa;Password='This is publ1c, so whatever works!';MultipleActiveResultSets=true;Encrypt=False;"
  },
  "DbProvider": "mssql"
}
```

You can also see this within Visual Studio Code opening in Dev Container.

Otherwise, for local development:

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Your favorite IDE, or [Visual Studio Code](https://code.visualstudio.com/)
* (_Optional_) [Docker](https://www.docker.com/products/docker-desktop)

```bash
cd src/
docker build -t maintenancelog:local .
```

## Local Development

### Secrets Management

```bash
cd src/MaintenanceLog
# dotnet tool install --global dotnet-ef
dotnet ef database update

dotnet user-secrets set EmailConfig:SmtpUser myuser
dotnet user-secrets set EmailConfig:SmtpPass mypass
dotnet user-secrets set EmailConfig:SmtpHost my-smtp.host.com
dotnet user-secrets set EmailConfig:SmtpPort 587
```

## License

See [LICENSE](./LICENSE)
