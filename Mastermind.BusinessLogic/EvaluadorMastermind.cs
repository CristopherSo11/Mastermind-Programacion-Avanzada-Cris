namespace Mastermind.BusinessLogic
{
    public class EvaluadorMastermind
    {
        public (int bienPosicionados, int colorCorrecto) EvaluarIntento(
            string patronSecreto, string intentoJugador)
        {
            int bienPosicionados = 0;
            int colorCorrecto = 0;

            Dictionary<char, int> coloresSecretos = new();
            Dictionary<char, int> coloresIntento = new();

            for (int i = 0; i < patronSecreto.Length; i++)
            {
                if (patronSecreto[i] == intentoJugador[i])
                {
                    bienPosicionados++;
                }
                else
                {
                    char secreto = patronSecreto[i];
                    char intento = intentoJugador[i];

                    if (!coloresSecretos.ContainsKey(secreto))
                        coloresSecretos[secreto] = 0;

                    if (!coloresIntento.ContainsKey(intento))
                        coloresIntento[intento] = 0;

                    coloresSecretos[secreto]++;
                    coloresIntento[intento]++;
                }
            }

            foreach (var color in coloresIntento)
            {
                if (coloresSecretos.ContainsKey(color.Key))
                {
                    colorCorrecto += Math.Min(
                        color.Value,
                        coloresSecretos[color.Key]);
                }
            }

            return (bienPosicionados, colorCorrecto);
        }
    }
}
