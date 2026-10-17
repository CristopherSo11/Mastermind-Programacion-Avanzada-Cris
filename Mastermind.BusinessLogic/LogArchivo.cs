namespace Mastermind.BusinessLogic
{
    public class LogArchivo : ILogEvaluacion
    {
        private readonly string rutaArchivo;

        public LogArchivo(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
        }

        public void Registrar(string mensaje)
        {
            string? carpeta = Path.GetDirectoryName(rutaArchivo);

            if (!string.IsNullOrEmpty(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            File.AppendAllText(
                rutaArchivo,
                mensaje + Environment.NewLine);
        }
    }
}
