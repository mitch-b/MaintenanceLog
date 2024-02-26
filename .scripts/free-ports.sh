#!/bin/bash

echo "Forcibly closing processes holding TCP ports open we need"

# Array of ports to check
ports=(5217 7019)

# Loop over the ports
for port in "${ports[@]}"
do
  # Use lsof to find processes using the port and awk to get the PID
  echo "Checking port $port"
  pid=$(lsof -t -i:$port)

  # If a process is using the port, kill it
  if [ -n "$pid" ]; then
    echo "Killing process $pid on port $port"
    kill -9 $pid
  else
    echo "No process using port $port"
  fi
done