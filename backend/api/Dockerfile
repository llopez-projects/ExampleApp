# Imagen base oficial de .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y restaurar paquetes
COPY ["backend/api/api.csproj", "api/"]
COPY ["backend/application/application.csproj", "application/"]
COPY ["backend/infrastructure/infrastructure.csproj", "infrastructure/"]
COPY ["backend/domain/domain.csproj", "domain/"]

# Restaurar paquetes
RUN dotnet restore "api/api.csproj"

# Copiar todo el código
COPY backend/api/ api/
COPY backend/application/ application/
COPY backend/infrastructure/ infrastructure/
COPY backend/domain/ domain/

# Compilar y publicar
WORKDIR "/src/api"
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]
