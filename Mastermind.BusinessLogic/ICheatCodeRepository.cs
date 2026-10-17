namespace Mastermind.BusinessLogic
{
    public interface ICheatCodeRepository
    {
        CheatCode? Buscar(string codigo);

        void ConsumirUso(string codigo);
    }
}
