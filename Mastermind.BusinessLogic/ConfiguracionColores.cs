using System.Text.Json;

namespace Mastermind.BusinessLogic
{
    public class ConfiguracionColores
    {
        public Dictionary<string, string> ColoresPermitidos
            { get; set; } = new();

        public static ConfiguracionColores CargarDesdeJson(
            string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException(
                    "No se encontró el archivo de colores.",
                    rutaArchivo);
            }

            string contenido = File.ReadAllText(rutaArchivo);

            ConfiguracionColores? configuracion =
                JsonSerializer.Deserialize<ConfiguracionColores>(
                    contenido,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (configuracion == null ||
                configuracion.ColoresPermitidos.Count == 0)
            {
                throw new InvalidOperationException(
                    "La configuración de colores está vacía.");
            }

            return configuracion;
        }
    }
}
