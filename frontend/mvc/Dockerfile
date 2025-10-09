# Dockerfile para proyecto MVC .NET Core
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solución y restaurar paquetes
COPY ["frontend/mvc/mvc.csproj", "mvc/"]
COPY ["frontend/core/core.csproj", "core/"]

# Restaurar paquetes
RUN dotnet restore "mvc/mvc.csproj"

# Copiar todo el código
COPY frontend/mvc/ mvc/
COPY frontend/core/ core/

# Compilar y publicar
RUN dotnet build "mvc/mvc.csproj" -c Release -o /app/build
RUN dotnet publish "mvc/mvc.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "mvc.dll"]
