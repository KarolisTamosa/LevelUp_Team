using Newtonsoft.Json;

namespace Datos
{ 
    public static class ApiDivisas
    {
        private static string MONEDAS_API_URL = "https://v6.exchangerate-api.com/v6/a2add358f81f3345c05f21c9/latest/USD";
        private static string MONEDAS_API_KEY = "a2add358f81f3345c05f21c9";
        private static async Task<ResultadoApiDivisas> ImportarMonedasDesdeApiAsync()
        {
            ResultadoApiDivisas resultadoApi = new();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MONEDAS_API_KEY}");

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(MONEDAS_API_URL);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    resultadoApi = JsonConvert.DeserializeObject<ResultadoApiDivisas>(jsonResponse);
                }
                catch (HttpRequestException ex)
                {//TODO Quitar el Console.WriteLine
                    Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
                }
                catch (JsonException ex)
                {//TODO Quitar el Console.WriteLine
                    Console.WriteLine($"Error al deserializar la respuesta JSON: {ex.Message}");
                }
                catch (Exception ex)
                {//TODO Quitar el Console.WriteLine
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }

            }
            return resultadoApi;
        }
        public static ResultadoApiDivisas ImportarMonedasDesdeApi()
        {
            return ImportarMonedasDesdeApiAsync().GetAwaiter().GetResult();
        }

    }
}
