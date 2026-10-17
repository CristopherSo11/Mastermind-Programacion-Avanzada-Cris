namespace Mastermind.BusinessLogic
{
    public class SesionPartida
    {
        public bool CheatUtilizado { get; private set; }

        public void MarcarCheatUtilizado()
        {
            if (CheatUtilizado)
                throw new InvalidOperationException(
                    "Ya utilizaste un cheat code en esta partida.");

            CheatUtilizado = true;
        }
    }
}
