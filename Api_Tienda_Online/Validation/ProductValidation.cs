using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;

namespace Api_Tienda_Online.Validation
{
    public class ProductValidation : IProductValidation
    {
        public ResponseApiDto ValidationProductTrue(CreateProductDto product)
        {
            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Description) || product.Price == "")

                return new ResponseApiDto
                {
                    Success = false,
                    StatusCode = 400,
                    Data = new {MessageErr = "Campos vacios o datos Erroneos!"}
                };
             
          
            return new ResponseApiDto
            {
                Success = true
            };
        }

        public ResponseApiDto ValidationProductTrue(Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Description) ||
                 product.Price == 0)

                return new ResponseApiDto
                {
                    Success = false,
                    StatusCode = 400,
                    Data = new { MessageErr = "Campos vacios o datos Erroneos!" }
                };


            return new ResponseApiDto
            {
                Success = true
            };
        }
    }
}
