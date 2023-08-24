using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;

namespace Api_Tienda_Online.Validation
{
    public interface IProductValidation
    {
        ResponseApiDto ValidationProductTrue(CreateProductDto product);
        ResponseApiDto ValidationProductTrue(Product product);

    }
}
