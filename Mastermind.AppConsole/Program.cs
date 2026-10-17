using Mastermind.BusinessLogic;

Console.WriteLine("=================================");
Console.WriteLine("       BIENVENIDO A MASTERMIND");
Console.WriteLine("=================================");

// Cargar colores desde el archivo JSON.
string rutaColores = Path.Combine(
    AppContext.BaseDirectory,
    "Configuracion",
    "colores.json");

ConfiguracionColores configuracion =
    ConfiguracionColores.CargarDesdeJson(rutaColores);

// Preparar las clases del juego.
EvaluadorMastermind evaluador = new EvaluadorMastermind();

ValidadorMastermind validador =
    new ValidadorMastermind(configuracion);

// Preparar el archivo de logging.
string rutaLog = Path.Combine(
    AppContext.BaseDirectory,
    "Logs",
    "evaluaciones.txt");

ILogEvaluacion log = new LogArchivo(rutaLog);

ServicioRegistroEvaluacion registro =
    new ServicioRegistroEvaluacion(log);

// Preparar el archivo persistente de cheat codes.
string carpetaDatos = Path.Combine(
    Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData),
    "MastermindCris");

Directory.CreateDirectory(carpetaDatos);

// TEMPORAL: archivo exclusivo para pruebas.
string rutaCheats = Path.Combine(
    carpetaDatos,
    "cheatcodes.json");
string rutaCheatsInicial = Path.Combine(
    AppContext.BaseDirectory,
    "Configuracion",
    "cheatcodes.json");

if (!File.Exists(rutaCheats))
{
    File.Copy(rutaCheatsInicial, rutaCheats);
}

ICheatCodeRepository repositorioCheats =
    new CheatCodeJsonRepository(rutaCheats);

ServicioCheatCode servicioCheat =
    new ServicioCheatCode(
        repositorioCheats,
        new Random());

// Servicio principal que conecta todo.
ServicioMastermind servicio =
    new ServicioMastermind(
        evaluador,
        validador,
        registro,
        servicioCheat);

bool seguirJugando = true;

while (seguirJugando)
{
    Console.WriteLine();
    Console.WriteLine("=== NUEVA PARTIDA ===");

    int longitud;

    while (true)
    {
        Console.Write("Elige longitud (4 o 6): ");
        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out longitud) &&
            (longitud == 4 || longitud == 6))
        {
            break;
        }

        Console.WriteLine(
            "Debes seleccionar 4 o 6 posiciones.");
    }

    // Obtener los códigos permitidos.
    string[] colores =
        configuracion.ColoresPermitidos.Keys.ToArray();

    Random aleatorio = Random.Shared;

    // Generar el patrón secreto.
    string patronSecreto = string.Concat(
        Enumerable.Range(0, longitud)
            .Select(_ => colores[
                aleatorio.Next(colores.Length)]));
    SesionPartida sesion = new SesionPartida();
Console.WriteLine(
    $"[PRUEBA] Patrón secreto: {patronSecreto}");
    string? ultimoIntento = null;
    int intentos = 0;
    bool victoria = false;

    Console.WriteLine();
    Console.WriteLine("Colores disponibles:");

    foreach (var color in configuracion.ColoresPermitidos)
    {
        Console.WriteLine(
            $"{color.Key} - {color.Value}");
    }

    Console.WriteLine();
    Console.WriteLine(
        $"Descubre el patrón secreto de {longitud} posiciones.");

    Console.WriteLine(
        "Puedes repetir colores en tus intentos.");

    Console.WriteLine(
        "Para utilizar una pista escribe CHEAT.");

    while (!victoria)
    {
        Console.WriteLine();
        Console.Write(
            $"Intento #{intentos + 1}: ");

        string intento = (
            Console.ReadLine() ?? "")
            .Trim()
            .ToUpperInvariant();

        // Solicitar una pista sin consumir un intento.
        if (intento == "CHEAT")
        {
            if (intentos == 0)
            {
                Console.WriteLine(
                    "Primero realiza un intento.");
                continue;
            }

            Console.Write("Introduce tu cheat code: ");
            string codigo = (
                Console.ReadLine() ?? "").Trim();

            // Utilizamos el último intento válido.
            // Su valor se guarda más abajo.
            if (ultimoIntento == null)
            {
                Console.WriteLine(
                    "Todavía no hay un intento válido.");
                continue;
            }

            try
            {
                ResultadoEvaluacion pista =
                    servicio.Evaluar(
                        patronSecreto,
                        ultimoIntento,
                        sesion,
                        codigo);

                Console.WriteLine(
                    $"Estado: {pista.EstadoCheat}");

                Console.WriteLine(
                    $"Posición revelada: {pista.PosicionRevelada}");

                Console.WriteLine(
                    $"Color revelado: {pista.ColorRevelado}");
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is InvalidOperationException)
            {
                Console.WriteLine(
                    $"No se pudo utilizar la pista: {ex.Message}");
            }

            continue;
        }

        try
        {
            ResultadoEvaluacion resultado =
                servicio.Evaluar(
                    patronSecreto,
                    intento);

            intentos++;
            ultimoIntento = intento;

            Console.WriteLine(
                $"Bien posicionados: {resultado.BienPosicionados}");

            Console.WriteLine(
                $"Colores correctos en otra posición: {resultado.ColorCorrecto}");

            if (resultado.BienPosicionados == longitud)
            {
                victoria = true;

                Console.WriteLine();
                Console.WriteLine(
                    "¡FELICIDADES! Descubriste el patrón.");

                Console.WriteLine(
                    $"Patrón secreto: {patronSecreto}");

                Console.WriteLine(
                    $"Intentos utilizados: {intentos}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(
                $"Intento inválido: {ex.Message}");
        }
    }

    Console.WriteLine();
    Console.Write("¿Quieres jugar otra vez? (S/N): ");

    string respuesta = (
        Console.ReadLine() ?? "")
        .Trim()
        .ToUpperInvariant();

    seguirJugando = respuesta == "S";
}

Console.WriteLine();
Console.WriteLine("¡Gracias por jugar Mastermind!");
