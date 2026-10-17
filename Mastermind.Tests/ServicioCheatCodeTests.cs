using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class ServicioCheatCodeTests
    {
        private class RepositorioFalso : ICheatCodeRepository
        {
            public int UsosConsumidos { get; private set; }

            public CheatCode? Buscar(string codigo)
            {
                if (codigo == "TEST123")
                {
                    return new CheatCode
                    {
                        Hash = codigo,
                        UsosDisponibles = 1 - UsosConsumidos
                    };
                }

                return null;
            }

            public void ConsumirUso(string codigo)
            {
                if (Buscar(codigo)?.UsosDisponibles <= 0)
                    throw new InvalidOperationException(
                        "Sin usos disponibles.");

                UsosConsumidos++;
            }
        }

        [Fact]
        public void CheatDebeConsumirseAunqueNoAyude()
        {
            // Arrange
            RepositorioFalso repositorio = new RepositorioFalso();

            ServicioCheatCode servicio =
                new ServicioCheatCode(
                    repositorio,
                    new Random(10));

            SesionPartida sesion = new SesionPartida();

            ResultadoEvaluacion resultado =
                new ResultadoEvaluacion
                {
                    BienPosicionados = 4,
                    ColorCorrecto = 0
                };

            // Act: todas las posiciones ya son correctas
            servicio.AplicarCheat(
                "TEST123",
                "RNB1",
                "RNB1",
                sesion,
                resultado);

            // Assert
            Assert.Equal("Sin suerte", resultado.EstadoCheat);
            Assert.Equal(1, repositorio.UsosConsumidos);
            Assert.True(sesion.CheatUtilizado);
            Assert.Equal(4, resultado.BienPosicionados);
        }
        
[Fact]
public void NoDebePermitirSegundoCheatEnMismaPartida()
{
    // Arrange
    RepositorioFalso repositorio = new RepositorioFalso();

    ServicioCheatCode servicio =
        new ServicioCheatCode(
            repositorio,
            new Random(10));

    SesionPartida sesion = new SesionPartida();

    ResultadoEvaluacion resultado =
        new ResultadoEvaluacion
        {
            BienPosicionados = 1,
            ColorCorrecto = 3
        };

    // Act: utilizar el primer cheat
    servicio.AplicarCheat(
        "TEST123",
        "RNB1",
        "RB1N",
        sesion,
        resultado);

    // Assert: intentar utilizarlo nuevamente
    Assert.Throws<InvalidOperationException>(() =>
        servicio.AplicarCheat(
            "TEST123",
            "RNB1",
            "RB1N",
            sesion,
            resultado));

    // Solo se consumió un uso.
    Assert.Equal(1, repositorio.UsosConsumidos);

    Assert.True(sesion.CheatUtilizado);
        }
    }
}
