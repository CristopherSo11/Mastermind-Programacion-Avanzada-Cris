using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class EvaluadorMastermindTests
    {
        [Theory]
        [InlineData("RGBY", "RGBY", 4, 0)]
        [InlineData("RGBY", "RBYG", 1, 3)]
        [InlineData("RGBY", "PPPP", 0, 0)]
        [InlineData("RRGB", "RGRR", 1, 2)]
        public void EvaluarIntento_DebeContarCorrectamente(
            string patronSecreto,
            string intentoJugador,
            int bienEsperados,
            int colorEsperados)
        {
            // Arrange: preparar el evaluador
            EvaluadorMastermind evaluador =
                new EvaluadorMastermind();

            // Act: ejecutar el algoritmo
            var resultado = evaluador.EvaluarIntento(
                patronSecreto, intentoJugador);

            // Assert: comprobar los resultados
            Assert.Equal(bienEsperados,
                resultado.bienPosicionados);

            Assert.Equal(colorEsperados,
                resultado.colorCorrecto);
        }
    }
}
