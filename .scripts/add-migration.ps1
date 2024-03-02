$originalDirectory = Get-Location

# Ask for the migration name
$migrationName = Read-Host -Prompt "Enter the name of the migration "

# Get the current script root and go up a folder
$scriptRoot = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
$parentFolder = Split-Path -Parent -Path $scriptRoot

# Change directory to "src/MaintenanceLog.Data"
Set-Location -Path "$parentFolder\src\MaintenanceLog.Data"

# Run the migration add
dotnet ef migrations add $migrationName --startup-project ../MaintenanceLog

# Ask if the user wants to apply the database update
$response = Read-Host -Prompt "Do you want to apply the database update? (yes/[no]) "

switch ($response.ToLower()) {
    "yes" { $applyUpdate = $true }
    "1" { $applyUpdate = $true }
    "y" { $applyUpdate = $true }
    default { $applyUpdate = $false }
}

if ($applyUpdate) {
    # Run the database update
    dotnet ef database update --startup-project ../MaintenanceLog
}

# Change back to the original directory
Set-Location -Path $originalDirectory