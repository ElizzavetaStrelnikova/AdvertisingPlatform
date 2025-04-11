# Advertising Platform Service

Веб-сервис для управления рекламными площадками.

## Prerequisites

- .NET 9.0+

## Technologies

### Backend

- ASP.NET Core - Web API framework

- In-Memory Database - Lightweight data storage

- RESTful API - JSON-based endpoints

- Logging to file

- Caching data 

## API Endpoints

- GET /api/platforms - List all advertising platforms

- GET /api/platforms/search?query={location} - Search advertising platforms by location

## 🚀 Build and deploy

### Local installation

1. Clone the repository.

git clone git@github.com:ElizzavetaStrelnikova/AdvertisingPlatform.git
cd advertising-platform

2. Install dependencies.

dotnet restore

3. Run the server.

dotnet run --project src/AdvertisingPlatform.Service