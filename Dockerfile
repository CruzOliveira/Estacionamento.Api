FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Estacionamento.API/Estacionamento.API.csproj", "Estacionamento.API/"]
COPY ["Estacionamento.Application/Estacionamento.Application.csproj", "Estacionamento.Application/"]
COPY ["Estacionamento.Domain/Estacionamento.Domain.csproj", "Estacionamento.Domain/"]
COPY ["Estacionamento.Infrastructure/Estacionamento.Infrastructure.csproj", "Estacionamento.Infrastructure/"]

RUN dotnet restore "Estacionamento.API/Estacionamento.API.csproj"

COPY . .
WORKDIR "/src/Estacionamento.API"

RUN dotnet publish "Estacionamento.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Estacionamento.API.dll"]
