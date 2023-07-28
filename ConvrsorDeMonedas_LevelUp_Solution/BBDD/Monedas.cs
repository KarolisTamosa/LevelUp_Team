﻿using Datos.Entidades;
using Newtonsoft.Json;

namespace Datos
{
    
    internal class Monedas
    {
        private string MONEDAS_API_URL = "https://v6.exchangerate-api.com/v6/a2add358f81f3345c05f21c9/latest/USD";
        private static string MONEDAS_API_KEY = "a2add358f81f3345c05f21c9";
        public async Task<ResultadoApiMonedas?> ImportarMonedasDesdeApiAsync(out bool errores)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MONEDAS_API_KEY}");
                ResultadoApiMonedas resultadoApi = null;
                
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(MONEDAS_API_URL);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    resultadoApi = JsonConvert.DeserializeObject<ResultadoApiMonedas>(jsonResponse);
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

                return resultadoApi;
            }
    }
}