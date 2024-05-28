# Use the official .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy application and build it
COPY ./wcf_chat/ .

# Restore dependencies and build the application
RUN dotnet restore
RUN dotnet publish ChatHostConsole/ChatHostConsole.csproj -c Release -o out

# Use the official .NET Runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out ./

# Expose the port the app runs on
EXPOSE 48400
EXPOSE 11000/udp

# Run the application
ENTRYPOINT ["dotnet", "ChatHostConsole.dll"]
