# Use the official .NET SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution and restore any dependencies
COPY ../../wcf_chat.sln ./
COPY ../ChatHostWPF.csproj ChatHostWPF/
RUN dotnet restore

# Copy the rest of the application and build it
COPY . .
RUN dotnet publish ChatHostWPF/ChatHostWPF.csproj -c Release -o out

# Use the official ASP.NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the build output from the build stage
COPY --from=build /app/out ./

# Expose the port the app runs on
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "ChatHostWPF.dll"]
