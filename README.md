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
docker run --rm -it -p 8080:8080 ghcr.io/mitch-b/maintenancelog:latest
```

Then open your browser to `http://localhost:8080`.

To run with a sidecar SQL database container **(recommended)**, use the provided [`docker-compose.yml`](./docker-compose.yml) file.

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
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpUser $EMAILCONFIG_SMTPUSER
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpFrom $EMAILCONFIG_SMTPUSER
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpPass $EMAILCONFIG_SMTPPASS
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpHost $EMAILCONFIG_SMTPHOST
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpPort $EMAILCONFIG_SMTPPORT
dotnet user-secrets set MaintenanceLogSettings:Database:Host db
```

To use `sqlite`, use the following appsettings:

```json
{
  "MaintenanceLogSettings": {
    "Database": {
      "DbProvider": "sqlite",
      "Name": "/data/maintenancelogdb.db;Cache=Shared",
      "Host": "",
      "User": "",
      "Password": ""
    },
    // ...
  },
  // ...
}
```

To use `mssql` in a sidecar container in the Codespace, use the following appsettings:

```json
{
  "MaintenanceLogSettings": {
    "Database": {
      "DbProvider": "mssql",
      "Name": "MaintenanceLogDb",
      "Host": "localhost",
      "User": "sa",
      "Password": "This is publ1c, so whatever works!"
    },
    // ...
  },
  // ...
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

### Entity Framework Migration

The initial migration was added with the following command:

```bash
# cd src/MaintenanceLog.Data
dotnet ef migrations add Initial --startup-project ../MaintenanceLog/
```

And the database can be upgraded with the following command:

```bash
# cd src/MaintenanceLog.Data
dotnet ef database update --startup-project ../MaintenanceLog/
```

... or with the following command:

```bash
# ./.scripts/add-migration.ps1
./.scripts/add-migration.sh
```

### Icon Selection

Browse Bootstrap Icons at [icons.getbootstrap.com](https://icons.getbootstrap.com/).

With icon SVG content (e.g. from the "Copy SVG" button), you can use the following to convert to data URL and put into NavMenu.razor.css:

https://www.svgbackgrounds.com/tools/svg-to-css/ (use the 'Legacy' setting on left, and 'Url wrapper' on right)

For example, `bi-house-gear`, I appended `-nav-menu` to follow convention, and change the fill color from `currentColor` to `white`:

```css
.bi-house-gear-nav-menu {
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='white' class='bi bi-house-gear-fill' viewBox='0 0 16 16'%3E%3Cpath d='M7.293 1.5a1 1 0 0 1 1.414 0L11 3.793V2.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v3.293l2.354 2.353a.5.5 0 0 1-.708.708L8 2.207 1.354 8.854a.5.5 0 1 1-.708-.708z'/%3E%3Cpath d='M11.07 9.047a1.5 1.5 0 0 0-1.742.26l-.02.021a1.5 1.5 0 0 0-.261 1.742 1.5 1.5 0 0 0 0 2.86 1.5 1.5 0 0 0-.12 1.07H3.5A1.5 1.5 0 0 1 2 13.5V9.293l6-6 4.724 4.724a1.5 1.5 0 0 0-1.654 1.03'/%3E%3Cpath d='m13.158 9.608-.043-.148c-.181-.613-1.049-.613-1.23 0l-.043.148a.64.64 0 0 1-.921.382l-.136-.074c-.561-.306-1.175.308-.87.869l.075.136a.64.64 0 0 1-.382.92l-.148.045c-.613.18-.613 1.048 0 1.229l.148.043a.64.64 0 0 1 .382.921l-.074.136c-.306.561.308 1.175.869.87l.136-.075a.64.64 0 0 1 .92.382l.045.149c.18.612 1.048.612 1.229 0l.043-.15a.64.64 0 0 1 .921-.38l.136.074c.561.305 1.175-.309.87-.87l-.075-.136a.64.64 0 0 1 .382-.92l.149-.044c.612-.181.612-1.049 0-1.23l-.15-.043a.64.64 0 0 1-.38-.921l.074-.136c.305-.561-.309-1.175-.87-.87l-.136.075a.64.64 0 0 1-.92-.382ZM12.5 14a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3'/%3E%3C/svg%3E");
}
```

### Secrets Management

```bash
cd src/MaintenanceLog

dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpUser myuser
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpFrom myuser@host.com
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpPass mypass
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpHost my-smtp.host.com
dotnet user-secrets set MaintenanceLogSettings:EmailConfig:SmtpPort 587
```

## License

See [LICENSE](./LICENSE)
