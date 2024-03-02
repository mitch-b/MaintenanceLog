#!/bin/bash

# Save the original directory
originalDirectory=$(pwd)

# Ask for the migration name
read -p "Enter the name of the migration: " migrationName

# Get the current script root and go up a folder
scriptRoot=$(dirname "$0")
parentFolder=$(dirname "$scriptRoot")

# Change directory to "src/MaintenanceLog.Data"
cd "$parentFolder/src/MaintenanceLog.Data"

# Run the migration add
dotnet ef migrations add $migrationName --startup-project ../MaintenanceLog

# Ask if the user wants to apply the database update
read -p "Do you want to apply the database update? (yes/[no]) " response

# Check for multiple truthy values
case $response in
    [Yy]* | [Yy][Ee][Ss]* | 1 ) applyUpdate=true ;;
    * ) applyUpdate=false ;;
esac

if $applyUpdate ; then
    # Run the database update
    dotnet ef database update --startup-project ../MaintenanceLog
fi

# Change back to the original directory
cd "$originalDirectory"