using api_core_mvc.Models;

namespace api_core_mvc.Servicios
{
    public interface IServicio_API
    {
        Task<List<Producto>> Lista();

        Task<Producto> Obtener(int id);

        Task<bool> Guardar(Producto obj);

        Task<bool> Editar(Producto obj);

        Task<bool> Eliminar(int id);
    }
}
