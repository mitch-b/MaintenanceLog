Write-Host "Forcibly closing processes holding TCP ports open we need"

# Array of ports to check
$ports = @(5217, 7019)

# Loop over the ports
foreach ($port in $ports)
{
  # Use netstat to find processes using the port and findstr to get the PID
  Write-Host "Checking port $port"
  $processId = netstat -ano | findstr ":$port" | %{ $_.Split()[-1] }

  # If a process is using the port, kill it
  if ($processId)
  {
    Write-Host "Killing process $processId on port $port"
    Stop-Process -Id $processId -Force
  }
  else
  {
    Write-Host "No process using port $port"
  }
}