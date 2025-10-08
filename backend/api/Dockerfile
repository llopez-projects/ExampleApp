# Imagen base oficial de .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar solución y restaurar paquetes
COPY ["api/api.csproj", "api/"]
COPY ["application/application.csproj", "application/"]
COPY ["infrastructure/infrastructure.csproj", "infrastructure/"]
COPY ["domain/domain.csproj", "domain/"]
RUN dotnet restore "api/api.csproj"

# Compilar y publicar
COPY . .
WORKDIR "/src/api"
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
