
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapStaticAssets();

string CreatePage(string title, string content)
{
    return $"""
    <!DOCTYPE html>
    <html lang="es">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>NovaHarvest - {title}</title>
        <link rel="stylesheet" href="/css/site.css">
    </head>
    <body>
        <header>
            <img src="/img/logo.png" alt="Logo NovaHarvest">
            <h2>NovaHarvest</h2>

            <nav>
                <a href="/">Inicio</a>
                <a href="/about">Acerca de</a>
                <a href="/status">Estado</a>
            </nav>
        </header>

        <main>
            {content}
        </main>

        <footer>
            NovaHarvest · IES Valle del Jerte · {DateTime.Now.Year}
        </footer>
    </body>
    </html>
    """;
}

app.MapGet("/", () => Results.Content(
    CreatePage("Inicio", """
        <h1>Inicio</h1>
        <p>NovaHarvest es un proyecto que busca reducir el desperdicio de alimentos en el Valle del Jerte.</p>
        <p>Conectamos a los productores agrícolas con compradores interesados en aprovechar los excedentes.</p>
        <p>Queremos facilitar la distribución de frutas y verduras que todavía pueden aprovecharse.</p>
        <p>Nuestro objetivo es fomentar un consumo responsable y reducir el desperdicio alimentario.</p>
    """),
    "text/html"));

app.MapGet("/about", () => Results.Content(
    CreatePage("Acerca de", """
        <h1>Acerca de</h1>
        <p><strong>Proyecto:</strong> NovaHarvest</p>
        <p><strong>Centro:</strong> IES Valle del Jerte</p>
        <p><strong>Autor:</strong> Ivan Prados Nuñez</p>
        <p><strong>Versión:</strong> 0.1.0</p>
    """),
    "text/html"));

app.MapGet("/status", () => Results.Content(
    CreatePage("Estado del servidor", $"""
        <h1>Estado del servidor</h1>
        <p>Fecha y hora del servidor: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
        <p>Página generada por ASP.NET Core (Kestrel)</p>
    """),
    "text/html"));

app.Run();
