using Mastermind.BusinessLogic;

EvaluadorMastermind evaluador = new EvaluadorMastermind();

string patronSecreto = "RGBY";
string intentoJugador = "RBYG";

var resultado = evaluador.EvaluarIntento(
    patronSecreto, intentoJugador);

Console.WriteLine("=== JUEGO MASTERMIND ===");
Console.WriteLine($"Patrón secreto: {patronSecreto}");
Console.WriteLine($"Intento del jugador: {intentoJugador}");
Console.WriteLine($"Bien posicionados: {resultado.bienPosicionados}");
Console.WriteLine($"Colores correctos en otra posición: {resultado.colorCorrecto}");

Console.WriteLine();
Console.WriteLine("=== COLORES CONFIGURADOS ===");

string rutaColores = Path.Combine(
    AppContext.BaseDirectory,
    "Configuracion",
    "colores.json");

ConfiguracionColores configuracion =
    ConfiguracionColores.CargarDesdeJson(rutaColores);

foreach (var color in configuracion.ColoresPermitidos)
{
    Console.WriteLine($"{color.Key} - {color.Value}");
}

Console.WriteLine();
Console.WriteLine("=== PRUEBA DE VALIDACIONES ===");

ValidadorMastermind validador =
    new ValidadorMastermind(configuracion);

try
{
    validador.Validar("RNB1", "RB1N");
    Console.WriteLine("Intento válido.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

try
{
    validador.Validar("RNB1", "ZZZZ");
    Console.WriteLine("Intento válido.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== REGISTRO DE EVALUACIONES ===");

string rutaLog = Path.Combine(
    AppContext.BaseDirectory,
    "Logs",
    "evaluaciones.txt");

ILogEvaluacion logArchivo = new LogArchivo(rutaLog);

ServicioRegistroEvaluacion servicioRegistro =
    new ServicioRegistroEvaluacion(logArchivo);

var resultadoRegistro = evaluador.EvaluarIntento(
    "RNB1", "RB1N");

servicioRegistro.RegistrarEvaluacion(
    "RNB1",
    "RB1N",
    resultadoRegistro.bienPosicionados,
    resultadoRegistro.colorCorrecto);

Console.WriteLine("Evaluación registrada correctamente.");
Console.WriteLine($"Archivo: {rutaLog}");

Console.WriteLine();
Console.WriteLine("=== PRUEBA DE CHEAT CODES ===");


string carpetaDatos = Path.Combine(
    Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData),
    "MastermindCris");

Directory.CreateDirectory(carpetaDatos);

string rutaCheats = Path.Combine(
    carpetaDatos,
    "cheatcodes.json");

string rutaCheatsInicial = Path.Combine(
    AppContext.BaseDirectory,
    "Configuracion",
    "cheatcodes.json");

// Copiar los códigos iniciales solamente si
// todavía no existe el archivo persistente.
if (!File.Exists(rutaCheats))
{
    File.Copy(rutaCheatsInicial, rutaCheats);
}


ICheatCodeRepository repositorioCheats =
    new CheatCodeJsonRepository(rutaCheats);

string codigoPrueba = "A1B2C3D4E5F6G7H8I9J0ABCDE";

CheatCode? cheat = repositorioCheats.Buscar(codigoPrueba);

if (cheat != null)
{
    Console.WriteLine("Cheat code encontrado.");
    Console.WriteLine(
        $"Usos disponibles: {cheat.UsosDisponibles}");
}
else
{
    Console.WriteLine("Cheat code no encontrado.");
}

Console.WriteLine();
// Console.WriteLine("=== PRUEBA DE CHEAT CODE ===");

SesionPartida sesion = new SesionPartida();

ServicioCheatCode servicioCheat =
    new ServicioCheatCode(
        repositorioCheats,
        new Random(10));

var evaluacionCheat = evaluador.EvaluarIntento(
    "RNB1", "RB1N");

ResultadoEvaluacion resultadoCheat =
    new ResultadoEvaluacion
    {
        BienPosicionados = evaluacionCheat.bienPosicionados,
        ColorCorrecto = evaluacionCheat.colorCorrecto
    };

try
{
    servicioCheat.AplicarCheat(
        codigoPrueba,
        "RNB1",
        "RB1N",
        sesion,
        resultadoCheat);

    Console.WriteLine(
        $"Estado: {resultadoCheat.EstadoCheat}");

    Console.WriteLine(
        $"Posición revelada: {resultadoCheat.PosicionRevelada}");

    Console.WriteLine(
        $"Color revelado: {resultadoCheat.ColorRevelado}");

    Console.WriteLine(
        $"Bien posicionados: {resultadoCheat.BienPosicionados}");

    Console.WriteLine(
        $"Cheat utilizado: {sesion.CheatUtilizado}");
}
catch (Exception ex) when (
    ex is ArgumentException ||
    ex is InvalidOperationException)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== SERVICIO MASTERMIND INTEGRADO ===");

ServicioMastermind servicioMastermind =
    new ServicioMastermind(
        evaluador,
        validador,
        servicioRegistro);

try
{
    ResultadoEvaluacion resultadoFinal =
        servicioMastermind.Evaluar("RNB1", "RB1N");

    Console.WriteLine(
        $"Bien posicionados: {resultadoFinal.BienPosicionados}");

    Console.WriteLine(
        $"Colores correctos en otra posición: {resultadoFinal.ColorCorrecto}");

    Console.WriteLine("Evaluación registrada en el log.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Error de validación: {ex.Message}");
}

Console.WriteLine();
// Console.WriteLine("=== MASTERMIND CON CHEAT CODE ===");
Console.WriteLine();
Console.WriteLine("=== RUTA DEL ARCHIVO DE CHEATS ===");
Console.WriteLine(rutaCheats);
// ServicioMastermind servicioCompleto =
//     new ServicioMastermind(
//         evaluador,
//         validador,
//         servicioRegistro,
//         servicioCheat);

// SesionPartida nuevaPartida = new SesionPartida();

// string segundoCodigo = "Z9Y8X7W6V5U4T3S2R1Q0LMNOP";

// try
// {
//     ResultadoEvaluacion resultadoConCheat =
//         servicioCompleto.Evaluar(
//             "RNB1",
//             "RB1N",
//             nuevaPartida,
//             segundoCodigo);

//     Console.WriteLine(
//         $"Bien posicionados: {resultadoConCheat.BienPosicionados}");

//     Console.WriteLine(
//         $"Colores correctos: {resultadoConCheat.ColorCorrecto}");

//     Console.WriteLine(
//         $"Estado del cheat: {resultadoConCheat.EstadoCheat}");

//     Console.WriteLine(
//         $"Posición revelada: {resultadoConCheat.PosicionRevelada}");

//     Console.WriteLine(
//         $"Color revelado: {resultadoConCheat.ColorRevelado}");

//     Console.WriteLine(
//         $"Cheat utilizado: {nuevaPartida.CheatUtilizado}");
// }
// catch (Exception ex) when (
//     ex is ArgumentException ||
//     ex is InvalidOperationException)
// {
//     Console.WriteLine($"Error: {ex.Message}");
// }
