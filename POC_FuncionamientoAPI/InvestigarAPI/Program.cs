using Newtonsoft.Json;

namespace InvestigarAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string apiUrl = "https://v6.exchangerate-api.com/v6/a2add358f81f3345c05f21c9/latest/USD";
            string apiKey = "a2add358f81f3345c05f21c9";

            using (var httpClient = new HttpClient())
            {
                // Agregar la clave de acceso en los headers de la solicitud
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                try
                {
                    // Realizar la solicitud GET a la API y obtener la respuesta
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verificar si la solicitud fue exitosa
                    response.EnsureSuccessStatusCode();

                    // Leer el contenido de la respuesta como una cadena JSON u otro formato según la API
                    string jsonResponse = await response.Content.ReadAsStringAsync();


                    // Convertir archivo json a ResultadoJson
                    ResultadoJson resulatadoAPI = JsonConvert.DeserializeObject<ResultadoJson>(jsonResponse);


                    // Mostrar los datos en la consola para verificar que se haya deserializado correctamente
                    Console.WriteLine("Result: " + resulatadoAPI.Result);
                    Console.WriteLine("Documentation: " + resulatadoAPI.Documentation);
                    Console.WriteLine("Terms_Of_Use: " + resulatadoAPI.Terms_Of_Use);
                    Console.WriteLine("Time_Last_Update_Unix: " + resulatadoAPI.Time_Last_Update_Unix);
                    Console.WriteLine("Time_Last_Update_Utc: " + resulatadoAPI.Time_Last_Update_Utc);
                    Console.WriteLine("Time_Next_Update_Unix: " + resulatadoAPI.Time_Next_Update_Unix);
                    Console.WriteLine("Time_Next_Update_Utc: " + resulatadoAPI.Time_Next_Update_Utc);
                    Console.WriteLine("Base_Code: " + resulatadoAPI.Base_Code);

                    // Mostrar las tasas de conversión
                    foreach (var conversionRate in resulatadoAPI.Conversion_Rates)
                    {
                        Console.WriteLine($"{conversionRate.Key}: {conversionRate.Value}");
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

    public class ResultadoJson
    {
        public string Result { get; set; }
        public string Documentation { get; set; }
        public string Terms_Of_Use { get; set; }
        public long Time_Last_Update_Unix { get; set; }
        public string Time_Last_Update_Utc { get; set; }
        public long Time_Next_Update_Unix { get; set; }
        public string Time_Next_Update_Utc { get; set; }
        public string Base_Code { get; set; }
        public Dictionary<string, decimal> Conversion_Rates { get; set; }
    }
}
