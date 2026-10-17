
namespace Mastermind.BusinessLogic
{
    public class ServicioMastermind
    {
        private readonly EvaluadorMastermind evaluador;
        private readonly ValidadorMastermind validador;
        private readonly ServicioRegistroEvaluacion registro;
        private readonly ServicioCheatCode? servicioCheat;

        public ServicioMastermind(
            EvaluadorMastermind evaluador,
            ValidadorMastermind validador,
            ServicioRegistroEvaluacion registro,
            ServicioCheatCode? servicioCheat = null)
        {
            this.evaluador = evaluador;
            this.validador = validador;
            this.registro = registro;
            this.servicioCheat = servicioCheat;
        }

        public ResultadoEvaluacion Evaluar(
            string patronSecreto,
            string intentoJugador,
            SesionPartida? sesion = null,
            string? codigoCheat = null)
        {
            // Validar las entradas.
            validador.Validar(patronSecreto, intentoJugador);

            // Evaluar utilizando el algoritmo original.
            var evaluacion = evaluador.EvaluarIntento(
                patronSecreto, intentoJugador);

            ResultadoEvaluacion resultado =
                new ResultadoEvaluacion
                {
                    BienPosicionados = evaluacion.bienPosicionados,
                    ColorCorrecto = evaluacion.colorCorrecto
                };

            // Aplicar la pista si se proporciona un cheat code.
            if (!string.IsNullOrWhiteSpace(codigoCheat))
            {
                if (servicioCheat == null || sesion == null)
                    throw new InvalidOperationException(
                        "No se configuró el servicio de cheat codes.");

                servicioCheat.AplicarCheat(
                    codigoCheat,
                    patronSecreto,
                    intentoJugador,
                    sesion,
                    resultado);
            }

            // Registrar la evaluación.
            registro.RegistrarEvaluacion(
                patronSecreto,
                intentoJugador,
                resultado.BienPosicionados,
                resultado.ColorCorrecto);

            return resultado;
        }
    }
}
