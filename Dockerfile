# Use .NET 8 runtime as base
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Use .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PrayTimeBot/PrayTimeBot.csproj", "PrayTimeBot/"]
COPY ["PrayTimeBot.Domain/PrayTimeBot.Domain.csproj", "PrayTimeBot.Domain/"]
COPY ["PrayTimeBot.Infrastructure/PrayTimeBot.Infrastructure.csproj", "PrayTimeBot.Infrastructure/"]

RUN dotnet restore "PrayTimeBot/PrayTimeBot.csproj"

COPY . .
WORKDIR "/src/PrayTimeBot"
RUN dotnet build "PrayTimeBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PrayTimeBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 80 for Railway
EXPOSE 80

ENTRYPOINT ["dotnet", "PrayTimeBot.dll"]
