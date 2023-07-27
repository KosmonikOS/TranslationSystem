# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /App

# Copy necessary files for build
COPY ./TranslationSystem/ ./TranslationSystem/
COPY ./TranslationSystem.Bot/ ./TranslationSystem.Bot/
COPY ./TranslationSystem.Data/ ./TranslationSystem.Data/
COPY ./TranslationSystem.Domain/ ./TranslationSystem.Domain/
COPY ./TranslationSystem.Services/ ./TranslationSystem.Services/

WORKDIR /App/TranslationSystem

# Restore and publish
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /App/TranslationSystem

# Copy the published output from the build stage to the runtime stage
COPY --from=build-env /App/TranslationSystem/out .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "TranslationSystem.dll"]
