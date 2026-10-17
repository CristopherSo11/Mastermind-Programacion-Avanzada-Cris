using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class ServicioRegistroEvaluacionTests
    {
        // Repositorio falso para guardar el mensaje
        // sin escribir archivos reales.
        private class LogFalso : ILogEvaluacion
        {
            public string MensajeRegistrado { get; private set; }
                = string.Empty;

            public void Registrar(string mensaje)
            {
                MensajeRegistrado = mensaje;
            }
        }

        [Fact]
        public void DebeRegistrarEvaluacionConFechaYResultado()
        {
            // Arrange
            LogFalso log = new LogFalso();

            DateTime fechaFija =
                new DateTime(2026, 10, 16, 15, 30, 0);

            ServicioRegistroEvaluacion servicio =
                new ServicioRegistroEvaluacion(
                    log,
                    () => fechaFija);

            // Act
            servicio.RegistrarEvaluacion(
                "RNB1",
                "RB1N",
                1,
                3);

            // Assert
            Assert.Contains(
                "16-10-2026 15:30:00",
                log.MensajeRegistrado);

            Assert.Contains(
                "RNB1",
                log.MensajeRegistrado);

            Assert.Contains(
                "RB1N",
                log.MensajeRegistrado);

            Assert.Contains(
                "1 bien posicionados",
                log.MensajeRegistrado);

            Assert.Contains(
                "3 con color correcto",
                log.MensajeRegistrado);
        }

        [Fact]
        public void DebeRegistrarUnaEvaluacionPerfecta()
        {
            // Arrange
            LogFalso log = new LogFalso();

            ServicioRegistroEvaluacion servicio =
                new ServicioRegistroEvaluacion(
                    log,
                    () => new DateTime(2026, 10, 16));

            // Act
            servicio.RegistrarEvaluacion(
                "RNB1",
                "RNB1",
                4,
                0);

            // Assert
            Assert.Contains(
                "4 bien posicionados",
                log.MensajeRegistrado);

            Assert.Contains(
                "0 con color correcto",
                log.MensajeRegistrado);
        }
    }
}
