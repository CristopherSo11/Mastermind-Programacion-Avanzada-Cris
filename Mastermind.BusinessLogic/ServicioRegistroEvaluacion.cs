namespace Mastermind.BusinessLogic
{
    public class ServicioRegistroEvaluacion
    {
        private readonly ILogEvaluacion log;
        private readonly Func<DateTime> obtenerFecha;

        public ServicioRegistroEvaluacion(
            ILogEvaluacion log,
            Func<DateTime>? obtenerFecha = null)
        {
            this.log = log;
            this.obtenerFecha = obtenerFecha ?? (() => DateTime.Now);
        }

        public void RegistrarEvaluacion(
            string patronSecreto,
            string intentoJugador,
            int bienPosicionados,
            int colorCorrecto)
        {
            string fecha = obtenerFecha()
                .ToString("dd-MM-yyyy HH:mm:ss");

            string mensaje =
                $"En la fecha {fecha} se evaluó el intento " +
                $"{intentoJugador} contra el patrón {patronSecreto}. " +
                $"Resultado: {bienPosicionados} bien posicionados – " +
                $"{colorCorrecto} con color correcto.";

            log.Registrar(mensaje);
        }
    }
}
