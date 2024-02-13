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
