using Mastermind.BusinessLogic;
using Xunit;

namespace Mastermind.Tests
{
    public class SesionPartidaTests
    {
        [Fact]
        public void NoDebePermitirDosCheatsEnUnaPartida()
        {
            // Arrange
            SesionPartida sesion = new SesionPartida();

            // Act: utilizar el primer cheat
            sesion.MarcarCheatUtilizado();

            // Assert: comprobar que no permite otro
            Assert.True(sesion.CheatUtilizado);

            Assert.Throws<InvalidOperationException>(() =>
                sesion.MarcarCheatUtilizado());
        }
    }
}
