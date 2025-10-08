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

### 3. Acceder a los servicios
- Web: 
- API: http://localhost:5026/swagger
- Elastic: http://localhost:9200/_cat/indices?v
- Kibana: http://localhost:5601
- DB: 
    - Usuario: sa
    - Password: Passw0rd