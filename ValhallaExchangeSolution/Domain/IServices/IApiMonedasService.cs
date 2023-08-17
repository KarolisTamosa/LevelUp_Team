using Domain.Models;
namespace Domain.IServices
{
    public interface IApiMonedasService
    {
        List<Moneda> ObtenerListaMonedasDeApi();
    }
}
