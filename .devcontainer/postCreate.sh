dotnet dev-certs https --trust
#chmod +x ./.scripts/*

set +e
dotnet tool install --global dotnet-ef
set -e

echo "Setting .dotnet tool path"
export PATH="$PATH:/$USER/.dotnet/tools"