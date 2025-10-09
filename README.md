# 🧰 ExampleApp 

Este proyecto consta de una solución que integra:

- Interfaz visual de usuario (frontend)
- Web API REST (backend)
- Base de datos SQL Server
- Elasticsearch + Kibana para logging y visualización.

## 📦 Estructura del repositorio

- backend → Web API REST/SOAP con seguridad y optimizaciones
- database → Script de creación de base de datos
- frontend → Interfaz visual (Web, Windows App o Blazor) 
- docs → Documentación general del proyecto


## 🚀 Requisitos

- [Docker + Docker Compose]
- [.NET SDK]
- [SQL Server]

---

# 🔗 Conexión entre servicios

```plaintext
        ┌────────────────────────────┐
        │        Navegador           │
        │  http://localhost:5000     │
        └────────────┬───────────────┘
                     │
                     ▼
        ┌────────────────────────────┐
        │           MVC              │
        │       (puerto 5000)        │
        └────────────┬───────────────┘
                     │
                     ▼
        ┌────────────────────────────┐
        │           API              │
        │       (puerto 8080)        │
        └──────┬────────────┬────────┘
               │            │
               ▼            ▼
    ┌──────────────┐ ┌────────────────────┐
    │   SQL Server │ │   Elasticsearch    │
    │ (puerto 1433)│ │   (puerto 9200)    │
    └──────────────┘ └────────────────────┘
```

---

# 🛠️ Cómo ejecutar el proyecto

## 🐳 Opción 1: Ejecutar con Docker Compose

### 1. Clonar el proyecto

```bash
git clone https://github.com/tu-usuario/exampleapp.git
cd exampleapp
```

### 2. Levantar los servicios

```bash
docker-compose up -d
```

Esto inicia:

- sqlserver: base de datos
- sql-init: crea el esquema inicial
- elasticsearch: motor de búsqueda
- kibana: interfaz de logs
- api: aplicación .NET
- kibana_setup: crea el index pattern en Kibana
- mvc: interfaz de usuario

### 3. Acceder a los servicios
- Web: http://localhost:5000/
- API: http://localhost:5026/swagger
- Elastic: http://localhost:9200/_cat/indices?v
- Kibana: http://localhost:5601
- DB: 
    - Usuario: sa
    - Password: Passw0rd

## ⚙️ Opción 2: Levantar manualmente los servicios

### 1. Clonar el proyecto

```bash
git clone https://github.com/tu-usuario/exampleapp.git
cd exampleapp
```

### 🧱 2. Levantar la base de datos (SQL Server)

- Ejecutar el script  `create-schema.sql` para la creación de la base de datos, tablas y carga de datos.

### 🚀 3. Compilar y ejecutar la API

```bash
cd ExampleApp
dotnet run --project backend/api/api.csproj
```

### 🌐 4. Compilar y ejecutar el proyecto MVC

```bash
cd ExampleApp
dotnet run --project frontend/mvc/mvc.csproj
```

### 5. Configurar Kibana

- Ejecutar un endpoint de la API como por ejemplo: `http://localhost:5026/api/employees`
- Ingresar al sitio y crear el index-pattern: `exampleapp-endpoints` y seleccionar el campo de tiempo `Timestamp`

### 6. Acceder a los servicios
- Web: http://localhost:5000/
- API: http://localhost:5026/swagger
- Elastic: http://localhost:9200/_cat/indices?v
- Kibana: http://localhost:5601
- DB: 
    - Usuario: sa
    - Password: Passw0rd