using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class ServicioMastermindTests
    {
        private class LogFalso : ILogEvaluacion
        {
            public int CantidadRegistros { get; private set; }

            public void Registrar(string mensaje)
            {
                CantidadRegistros++;
            }
        }

        private static ServicioMastermind CrearServicio(LogFalso log)
        {
            ConfiguracionColores configuracion =
                new ConfiguracionColores
                {
                    ColoresPermitidos = new Dictionary<string, string>
                    {
                        { "R", "Red" },
                        { "N", "Green" },
                        { "B", "Black" },
                        { "1", "Yellow" }
                    }
                };

            return new ServicioMastermind(
                new EvaluadorMastermind(),
                new ValidadorMastermind(configuracion),
                new ServicioRegistroEvaluacion(
                    log,
                    () => new DateTime(2026, 10, 16)));
        }

        [Fact]
        public void EvaluacionValidaDebeRegistrarResultado()
        {
            LogFalso log = new LogFalso();
            ServicioMastermind servicio = CrearServicio(log);

            ResultadoEvaluacion resultado =
                servicio.Evaluar("RNB1", "RB1N");

            Assert.Equal(1, resultado.BienPosicionados);
            Assert.Equal(3, resultado.ColorCorrecto);
            Assert.Equal(1, log.CantidadRegistros);
        }

        [Fact]
        public void EvaluacionInvalidaNoDebeRegistrarse()
        {
            LogFalso log = new LogFalso();
            ServicioMastermind servicio = CrearServicio(log);

            Assert.Throws<ArgumentException>(() =>
                servicio.Evaluar("RNB1", "ZZZZ"));

            Assert.Equal(0, log.CantidadRegistros);
        }
    }
}
