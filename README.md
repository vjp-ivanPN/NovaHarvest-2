NovaHarvest


1. Arranque
Requisitos
Para ejecutar el proyecto con Docker es necesario tener instalado
Docker Desktop (o Docker Engine) con Docker Compose.
Para ejecutarlo directamente, sin Docker, es necesario tener instalado
el SDK de .NET 10.

Opción A: arrancar con Docker

Abre una terminal en la carpeta raíz del repositorio, donde se
encuentra `compose.yaml`.

Construye las imágenes y arranca los servicios:

``` bash
docker compose up -d --build
```
Abre el navegador en:
Inicio: http://localhost:8080/
Acerca de: http://localhost:8080/about
Estado del servidor: http://localhost:8080/status
Para comprobar que los contenedores están funcionando:
``` bash
docker compose ps
```
Para consultar los registros de los servicios:
``` bash
docker compose logs -f
```
Para detener y eliminar los contenedores creados por Compose:
``` bash
docker compose down
```
Este comando no elimina las imágenes construidas.
Opción B: ejecutar directamente con `dotnet run
`
Esta opción ejecuta el backend directamente en el ordenador, sin pasar
por nginx ni por Docker.

Abre una terminal en la carpeta raíz del repositorio.

Entra en la carpeta del proyecto .NET:

``` bash
cd backend/NovaHarvest.Web
```
Ejecuta la aplicación:
``` bash
dotnet run
```
El perfil HTTP del proyecto está configurado en
`Properties/launchSettings.json` y utiliza el puerto 5226. Abre
estas direcciones en el navegador:
Inicio: http://localhost:5226/
Acerca de: http://localhost:5226/about
Estado del servidor: http://localhost:5226/status
Para detener la aplicación, vuelve a la terminal donde se está
ejecutando y pulsa `Ctrl + C`.
> **Importante:** no ejecutes `dotnet run` desde la raíz del
> repositorio. Primero debes entrar en `backend/NovaHarvest.Web`, que es
> donde está el archivo `NovaHarvest.Web.csproj`.
2. Arquitectura
NovaHarvest está separada en dos servicios. El navegador se comunica con
nginx (`web`), y nginx reenvía las peticiones al backend ASP.NET
Core (`backend`), donde Kestrel ejecuta el código C#.
Diagrama del recorrido de una petición con Docker
``` text
┌──────────────────────────┐
│        Navegador         │
│   http://localhost:8080  │
└────────────┬─────────────┘
             │
             │ Petición HTTP al puerto 8080
             ▼
┌──────────────────────────┐
│     Equipo anfitrión     │
│       Puerto 8080        │
└────────────┬─────────────┘
             │
             │ Mapeo 8080:80
             ▼
┌──────────────────────────┐
│       Contenedor web     │
│       nginx :80          │
│      Reverse proxy       │
└────────────┬─────────────┘
             │
             │ Red interna de Docker
             │ backend:8080
             ▼
┌──────────────────────────┐
│     Contenedor backend  │
│   ASP.NET Core / Kestrel │
│         Puerto 8080      │
│       Program.cs         │
└──────────────────────────┘

      La respuesta vuelve por el mismo recorrido
      hasta llegar al navegador.
