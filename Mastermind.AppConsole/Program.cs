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
