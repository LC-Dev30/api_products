using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;
using System.Threading.Tasks;

namespace Capa_Repositorio.RepositorioProductos
{
    public interface IProductosRepositorio
    {
        Task<ResponseApiDto> ListProducts(int idClient);
        Task<ResponseApiDto> CreateProduct(CreateProductDto product, int idClient);
        Task<ResponseApiDto> Products(int id);
        Task<ResponseApiDto> UpdateProduct(Product product);
        Task<ResponseApiDto> DeleteProduct(int idProduct);
    }
}