namespace Mastermind.BusinessLogic
{
    public class ValidadorMastermind
    {
        private readonly HashSet<char> coloresPermitidos;

        public ValidadorMastermind(
            ConfiguracionColores configuracion)
        {
            if (configuracion == null)
                throw new ArgumentNullException(
                    nameof(configuracion));

            coloresPermitidos = new HashSet<char>();

            foreach (string codigo in
                configuracion.ColoresPermitidos.Keys)
            {
                if (codigo.Length != 1)
                    throw new ArgumentException(
                        "Cada color debe tener un código de un carácter.");

                coloresPermitidos.Add(codigo[0]);
            }

            if (coloresPermitidos.Count == 0)
                throw new ArgumentException(
                    "Debe existir al menos un color permitido.");
        }

        public void Validar(
            string? patronSecreto,
            string? intentoJugador)
        {
            if (string.IsNullOrWhiteSpace(patronSecreto))
                throw new ArgumentException(
                    "El patrón secreto no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(intentoJugador))
                throw new ArgumentException(
                    "El intento del jugador no puede estar vacío.");

            if (patronSecreto.Length != 4 &&
                patronSecreto.Length != 6)
                throw new ArgumentException(
                    "El patrón secreto debe tener 4 o 6 colores.");

            if (intentoJugador.Length != 4 &&
                intentoJugador.Length != 6)
                throw new ArgumentException(
                    "El intento debe tener 4 o 6 colores.");

            if (patronSecreto.Length != intentoJugador.Length)
                throw new ArgumentException(
                    "El patrón y el intento deben tener la misma longitud.");

            foreach (char color in patronSecreto)
            {
                if (!coloresPermitidos.Contains(color))
                    throw new ArgumentException(
                        $"El patrón contiene un color no permitido: {color}");
            }

            foreach (char color in intentoJugador)
            {
                if (!coloresPermitidos.Contains(color))
                    throw new ArgumentException(
                        $"El intento contiene un color no permitido: {color}");
            }
        }
    }
}
