using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class ValidadorMastermindTests
    {
        private readonly ValidadorMastermind validador;

        public ValidadorMastermindTests()
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

            validador = new ValidadorMastermind(configuracion);
        }

        [Fact]
        public void DebeAceptarPatronesValidos()
        {
            validador.Validar("RNB1", "RB1N");
        }

        [Fact]
        public void DebeRechazarColoresInvalidos()
        {
            Assert.Throws<ArgumentException>(() =>
                validador.Validar("RNB1", "ZZZZ"));
        }

        [Fact]
        public void DebeRechazarLongitudInvalida()
        {
            Assert.Throws<ArgumentException>(() =>
                validador.Validar("RNB", "RNB"));
        }

        [Fact]
        public void DebeRechazarLongitudesDiferentes()
        {
            Assert.Throws<ArgumentException>(() =>
                validador.Validar("RNB1", "RNB1RN"));
        }

        [Fact]
        public void DebeRechazarPatronVacio()
        {
            Assert.Throws<ArgumentException>(() =>
                validador.Validar("", "RNB1"));
        }
    }
}
