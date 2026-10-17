namespace Mastermind.BusinessLogic
{
    public class ResultadoEvaluacion
    {
        public int BienPosicionados { get; set; }

        public int ColorCorrecto { get; set; }

        public string EstadoCheat { get; set; } = "No usado";

        public int? PosicionRevelada { get; set; }

        public char? ColorRevelado { get; set; }
    }
}
