using Newtonsoft.Json;

namespace InvestigarAPI
{
    public class Persona
    {
        public string Base_code { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            string apiUrl = "https://v6.exchangerate-api.com/v6/a2add358f81f3345c05f21c9/latest/USD";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Realizar la solicitud GET a la API y obtener la respuesta
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa
                    response.EnsureSuccessStatusCode();

                    // Leer el contenido de la respuesta como una cadena JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(jsonResponse);

                    //Deserializar la cadena JSON en una lista de objetos Persona
                    var personas = JsonConvert.DeserializeObject<List<Persona>>(jsonResponse);

                    // Mostrar los resultados en la consola
                    foreach (var persona in personas)
                    {
                        Console.WriteLine($"Nombre: {persona.Base_code}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error al deserializar la respuesta JSON: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }
        }
    }

}