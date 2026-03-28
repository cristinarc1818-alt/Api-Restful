# Api-Restful 
PI RESTful para gestión de catálogo de libros construida con ASP.NET Core 8, Swagger y frontend integrado.
Características
API RESTful con ASP.NET Core Web API y [ApiController]
Entity Framework Core con In-Memory Database
3 libros pre-cargados como datos semilla
DTOs para separation de capas
JSON en camelCase con campo protegido [JsonIgnore]
Middleware global de manejo de errores
Swagger UI documentado en raíz con comentarios XML
Frontend con fetch/async-await y manejo de errores visible

URLs de Acceso
Swagger: https://localhost:7068/
API: https://localhost:7068/api/libros
Frontend: https://localhost:7068/index.html

Configuraciones
JSON camelCase configurado
Referencias circulares ignoradas
CORS habilitado para desarrollo
Errores retornan JSON consistente
Swagger en raíz con RoutePrefix vacío
