using System.Text.Json;

namespace Mastermind.BusinessLogic
{
    public class CheatCodeJsonRepository : ICheatCodeRepository
    {
        private readonly string rutaArchivo;

        public CheatCodeJsonRepository(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
        }

        private List<CheatCode> Cargar()
        {
            if (!File.Exists(rutaArchivo))
                throw new FileNotFoundException(
                    "No se encontró el archivo de cheat codes.",
                    rutaArchivo);

            string contenido = File.ReadAllText(rutaArchivo);

            return JsonSerializer.Deserialize<List<CheatCode>>(
                contenido) ?? new List<CheatCode>();
        }

        public CheatCode? Buscar(string codigo)
        {
            return Cargar().FirstOrDefault(
                c => string.Equals(
                    c.Hash,
                    codigo,
                    StringComparison.OrdinalIgnoreCase));
        }

        public void ConsumirUso(string codigo)
        {
            List<CheatCode> codigos = Cargar();

            CheatCode? encontrado = codigos.FirstOrDefault(
                c => string.Equals(
                    c.Hash,
                    codigo,
                    StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
                throw new ArgumentException(
                    "El cheat code no existe.");

            if (encontrado.UsosDisponibles <= 0)
                throw new InvalidOperationException(
                    "El cheat code no tiene usos disponibles.");

            encontrado.UsosDisponibles--;

            string contenido = JsonSerializer.Serialize(
                codigos,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(rutaArchivo, contenido);
        }
    }
}
