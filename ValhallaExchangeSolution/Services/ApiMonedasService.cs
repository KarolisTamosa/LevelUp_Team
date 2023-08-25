using Domain.IServices;
using Domain.Models;
using DTO;
using Newtonsoft.Json;

namespace Services
{
    public class ApiMonedasService : IApiMonedasService
    {
        private static string MONEDAS_API_URL = "https://v6.exchangerate-api.com/v6/a2add358f81f3345c05f21c9/latest/USD";
        private static string MONEDAS_API_KEY = "a2add358f81f3345c05f21c9";
        private static Dictionary<string, string> NOMBRES_MONEDAS = new Dictionary<string, string>() {
            {"USD", "Dólar estadounidense"},
            {"AED", "Dirham de los Emiratos Árabes Unidos"},
            {"AFN", "Afghani afgano"},
            {"ALL", "Lek albanés"},
            {"AMD", "Dram armenio"},
            {"ANG", "Florín neerlandés antillano"},
            {"AOA", "Kwanza angoleño"},
            {"ARS", "Peso argentino"},
            {"AUD", "Dólar australiano"},
            {"AWG", "Florín arubeño"},
            {"AZN", "Manat azerbaiyano"},
            {"BAM", "Marco convertible de Bosnia y Herzegovina"},
            {"BBD", "Dólar de Barbados"},
            {"BDT", "Taka bangladesí"},
            {"BGN", "Lev búlgaro"},
            {"BHD", "Dinar bahreiní"},
            {"BIF", "Franco burundiano"},
            {"BMD", "Dólar de Bermudas"},
            {"BND", "Dólar de Brunéi"},
            {"BOB", "Boliviano boliviano"},
            {"BRL", "Real brasileño"},
            {"BSD", "Dólar bahameño"},
            {"BTN", "Ngultrum butanés"},
            {"BWP", "Pula botsuano"},
            {"BYN", "Rublo bielorruso"},
            {"BZD", "Dólar beliceño"},
            {"CAD", "Dólar canadiense"},
            {"CDF", "Franco congoleño"},
            {"CHF", "Franco suizo"},
            {"CLP", "Peso chileno"},
            {"CNY", "Yuan chino"},
            {"COP", "Peso colombiano"},
            {"CRC", "Colón costarricense"},
            {"CUP", "Peso cubano"},
            {"CVE", "Escudo caboverdiano"},
            {"CZK", "Corona checa"},
            {"DJF", "Franco yibutiano"},
            {"DKK", "Corona danesa"},
            {"DOP", "Peso dominicano"},
            {"DZD", "Dinar argelino"},
            {"EGP", "Libra egipcia"},
            {"ERN", "Nakfa eritreo"},
            {"ETB", "Birr etíope"},
            {"EUR", "Euro"},
            {"FJD", "Dólar fiyiano"},
            {"FKP", "Libra malvinense"},
            {"FOK", "Corona feroesa"},
            {"GBP", "Libra esterlina"},
            {"GEL", "Lari georgiano"},
            {"GGP", "Libra de Guernsey"},
            {"GHS", "Cedi ghanés"},
            {"GIP", "Libra de Gibraltar"},
            {"GMD", "Dalasi gambiano"},
            {"GNF", "Franco guineano"},
            {"GTQ", "Quetzal guatemalteco"},
            {"GYD", "Dólar guyanés"},
            {"HKD", "Dólar de Hong Kong"},
            {"HNL", "Lempira hondureño"},
            {"HRK", "Kuna croata"},
            {"HTG", "Gourde haitiano"},
            {"HUF", "Forint húngaro"},
            {"IDR", "Rupia indonesia"},
            {"ILS", "Nuevo séquel israelí"},
            {"IMP", "Libra de la Isla de Man"},
            {"INR", "Rupia india"},
            {"IQD", "Dinar iraquí"},
            {"IRR", "Rial iraní"},
            {"ISK", "Corona islandesa"},
            {"JEP", "Libra de Jersey"},
            {"JMD", "Dólar jamaicano"},
            {"JOD", "Dinar jordano"},
            {"JPY", "Yen japonés"},
            {"KES", "Chelín keniano"},
            {"KGS", "Som kirguís"},
            {"KHR", "Riel camboyano"},
            {"KID", "Dólar de Kiribati"},
            {"KMF", "Franco comorense"},
            {"KRW", "Won surcoreano"},
            {"KWD", "Dinar kuwaití"},
            {"KYD", "Dólar de las Islas Caimán"},
            {"KZT", "Tenge kazajo"},
            {"LAK", "Kip laosiano"},
            {"LBP", "Libra libanesa"},
            {"LKR", "Rupia de Sri Lanka"},
            {"LRD", "Dólar liberiano"},
            {"LSL", "Loti lesothiano"},
            {"LYD", "Dinar libio"},
            {"MAD", "Dírham marroquí"},
            {"MDL", "Leu moldavo"},
            {"MGA", "Ariary malgache"},
            {"MKD", "Dinar macedonio"},
            {"MMK", "Kyat birmano"},
            {"MNT", "Tugrik mongol"},
            {"MOP", "Pataca de Macao"},
            {"MRU", "Ouguiya mauritana"},
            {"MUR", "Rupia mauriciana"},
            {"MVR", "Rufiyaa de Maldivas"},
            {"MWK", "Kwacha malauí"},
            {"MXN", "Peso mexicano"},
            {"MYR", "Ringgit malayo"},
            {"MZN", "Metical mozambiqueño"},
            {"NAD", "Dólar namibio"},
            {"NGN", "Naira nigeriano"},
            {"NIO", "Córdoba nicaragüense"},
            {"NOK", "Corona noruega"},
            {"NPR", "Rupia nepalesa"},
            {"NZD", "Dólar neozelandés"},
            {"OMR", "Rial omaní"},
            {"PAB", "Balboa panameño"},
            {"PEN", "Sol peruano"},
            {"PGK", "Kina de Papúa Nueva Guinea"},
            {"PHP", "Peso filipino"},
            {"PKR", "Rupia pakistaní"},
            {"PLN", "Zloty polaco"},
            {"PYG", "Guaraní paraguayo"},
            {"QAR", "Riyal catarí"},
            {"RON", "Leu rumano"},
            {"RSD", "Dinar serbio"},
            {"RUB", "Rublo ruso"},
            {"RWF", "Franco ruandés"},
            {"SAR", "Riyal saudí"},
            {"SBD", "Dólar de las Islas Salomón"},
            {"SCR", "Rupia de Seychelles"},
            {"SDG", "Dinar sudanés"},
            {"SEK", "Corona sueca"},
            {"SGD", "Dólar de Singapur"},
            {"SHP", "Libra de Santa Elena"},
            {"SLE", "Leone sierraleonés"},
            {"SLL", "Leone sierraleonés"},
            {"SOS", "Chelín somalí"},
            {"SRD", "Dólar surinamés"},
            {"SSP", "Libra sursudanesa"},
            {"STN", "Dobra de Santo Tomé y Príncipe"},
            {"SYP", "Libra siria"},
            {"SZL", "Lilangeni suazi"},
            {"THB", "Baht tailandés"},
            {"TJS", "Somoni tayiko"},
            {"TMT", "Manat turcomano"},
            {"TND", "Dinar tunecino"},
            {"TOP", "Paʻanga tongano"},
            {"TRY", "Lira turca"},
            {"TTD", "Dólar de Trinidad y Tobago"},
            {"TVD", "Dólar de Tuvalu"},
            {"TWD", "Nuevo dólar taiwanés"},
            {"TZS", "Chelín tanzano"},
            {"UAH", "Grivna ucraniana"},
            {"UGX", "Chelín ugandés"},
            {"UYU", "Peso uruguayo"},
            {"UZS", "Som uzbeko"},
            {"VES", "Bolívar soberano venezolano"},
            {"VND", "Dong vietnamita"},
            {"VUV", "Vatu vanuatuense"},
            {"WST", "Tala samoano"},
            {"XAF", "Franco CFA de África Central"},
            {"XCD", "Dólar del Caribe Oriental"},
            {"XDR", "Derechos especiales de giro"},
            {"XOF", "Franco CFA de África Occidental"},
            {"XPF", "Franco CFP"},
            {"YER", "Rial yemení"},
            {"ZAR", "Rand sudafricano"},
            {"ZMW", "Kwacha zambiano"},
            {"ZWL", "Dólar zimbabuense"}};


        //Obtener Lista de Monedas de Api
        public List<Moneda> ObtenerListaMonedasDeApi()
        {
            var listaMonedas = new List<Moneda>();

            var apiApiMonedasDTO = ObtenerApiMonedasDTODeJson().GetAwaiter().GetResult();
            
            if (apiApiMonedasDTO == null)
            {
                return listaMonedas;
            }

            listaMonedas = ConvertirApiMonedasDTOAListMoneda(apiApiMonedasDTO);

            return listaMonedas;
        }

        private async Task<ApiMonedasDTO> ObtenerApiMonedasDTODeJson()
        {
            ApiMonedasDTO resultadoApi = new();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MONEDAS_API_KEY}");

                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(MONEDAS_API_URL);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    resultadoApi = JsonConvert.DeserializeObject<ApiMonedasDTO>(jsonResponse);
                }
                catch (Exception ex)
                {
                    return new ApiMonedasDTO();
                }

            }
            return resultadoApi;
        }

        private List<Moneda> ConvertirApiMonedasDTOAListMoneda(ApiMonedasDTO apiMonedasDTO)
        {
            return apiMonedasDTO.Conversion_Rates.Select(moneda => new Moneda()
            {
                Codigo = moneda.Key.ToUpper(),
                Nombre = NOMBRES_MONEDAS[moneda.Key],
                ValorEnDolares = moneda.Value,
                Eliminado = false
            }).ToList();
        }
    }
}
