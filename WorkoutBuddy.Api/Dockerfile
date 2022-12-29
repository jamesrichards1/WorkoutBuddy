#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkoutBuddy.Api/WorkoutBuddy.Api.csproj", "WorkoutBuddy.Api/"]
RUN dotnet restore "WorkoutBuddy.Api/WorkoutBuddy.Api.csproj"
COPY . .
WORKDIR "/src/WorkoutBuddy.Api"
RUN dotnet build "WorkoutBuddy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkoutBuddy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkoutBuddy.Api.dll"]