namespace Mastermind.BusinessLogic
{
    public class ServicioCheatCode
    {
        private readonly ICheatCodeRepository repositorio;
        private readonly Random aleatorio;

        public ServicioCheatCode(
            ICheatCodeRepository repositorio,
            Random? aleatorio = null)
        {
            this.repositorio = repositorio;
            this.aleatorio = aleatorio ?? new Random();
        }

        public void AplicarCheat(
            string codigo,
            string patronSecreto,
            string intentoJugador,
            SesionPartida sesion,
            ResultadoEvaluacion resultado)
        {
            if (sesion.CheatUtilizado)
                throw new InvalidOperationException(
                    "Ya utilizaste un cheat code en esta partida.");

            CheatCode? cheat = repositorio.Buscar(codigo);

            if (cheat == null)
                throw new ArgumentException(
                    "El cheat code no existe.");

            if (cheat.UsosDisponibles <= 0)
                throw new InvalidOperationException(
                    "El cheat code no tiene usos disponibles.");

            int posicion = aleatorio.Next(patronSecreto.Length);

            // Consumir el código incluso si la pista no ayuda.
            repositorio.ConsumirUso(codigo);
            sesion.MarcarCheatUtilizado();

            resultado.PosicionRevelada = posicion + 1;
            resultado.ColorRevelado = patronSecreto[posicion];

            if (patronSecreto[posicion] == intentoJugador[posicion])
            {
                resultado.EstadoCheat = "Sin suerte";
                return;
            }

            resultado.EstadoCheat =
                $"Pista revelada en posición {posicion + 1}";

            
            resultado.PosicionRevelada = posicion + 1;
            resultado.ColorRevelado = patronSecreto[posicion];

            if (patronSecreto[posicion] == intentoJugador[posicion])
            {
                resultado.EstadoCheat = "Sin suerte";
                return;
            }

            resultado.EstadoCheat =
                $"Pista revelada en posición {posicion + 1}";
        }
    }
}
