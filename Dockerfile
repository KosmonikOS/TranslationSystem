FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /App

COPY ./TranslationSystem/ ./TranslationSystem/
COPY ./TranslationSystem.Bot/ ./TranslationSystem.Bot/
COPY ./TranslationSystem.Data/ ./TranslationSystem.Data/
COPY ./TranslationSystem.Domain/ ./TranslationSystem.Domain/
COPY ./TranslationSystem.Services/ ./TranslationSystem.Services/

WORKDIR /App/TranslationSystem

RUN dotnet restore

RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "/App/TranslationSystem/out/TranslationSystem.dll"]