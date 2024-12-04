# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and project files
COPY Pawel.MakeBooking.Process.sln ./  
COPY Pawel.MakeBooking.Contracts/*.csproj ./Pawel.MakeBooking.Contracts/
COPY Pawel.MakeBooking.Controllers/*.csproj ./Pawel.MakeBooking.Controllers/
COPY Pawel.MakeBooking.DBConnect/*.csproj ./Pawel.MakeBooking.DBConnect/
COPY Pawel.MakeBooking.Entities/*.csproj ./Pawel.MakeBooking.Entities/
COPY Pawel.MakeBooking.Handlers/*.csproj ./Pawel.MakeBooking.Handlers/
COPY Pawel.MakeBooking.Process/*.csproj ./Pawel.MakeBooking.Process/

# Copy the .learningtransport directory
COPY .learningtransport /app/.learningtransport

# Copy appsettings.json file (ensure it exists in the root of your project)
COPY appsettings.json /app/appsettings.json

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . ./  

# Build the application
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory inside the runtime container
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build-env /out .

# Copy the appsettings.json file to the container
COPY appsettings.json /app/appsettings.json

# Expose the port if it's a web application (default for Kestrel is 5000)
EXPOSE 5010

# Set the entry point to the application
ENTRYPOINT ["dotnet", "Pawel.MakeBooking.Process.dll"]
