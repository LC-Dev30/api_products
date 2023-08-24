using Api_Tienda_Online.Validation;
using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;
using Capa_Repositorio.RepositorioProductos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace Api_Tienda_Online.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private IProductosRepositorio _repositorioProductos;
        private IProductValidation _validationProduct;
     

        public Products(IProductosRepositorio repositorioProductos, IProductValidation validationProduct)
        {
            _repositorioProductos = repositorioProductos;   
            _validationProduct = validationProduct;
        }

        [Authorize]
        [HttpGet("Products")]
        public async Task<IActionResult> ListaProductos() 
        {

            if(HttpContext.User.Claims.Count() > 0)
            {
              var idClient = int.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value);
              var lista = await _repositorioProductos.ListProducts(idClient);

              if (lista.Success) return Ok(lista);

              if (lista.StatusCode == 500) return Problem();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("Product")]
        public async Task<IActionResult> AddProduct(CreateProductDto product)
        {
            var valid = _validationProduct.ValidationProductTrue(product);

            if (!valid.Success || valid.StatusCode == 400)
            {
                return BadRequest();
            }

            if(valid.Success)
            {
              var idClient = int.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value);
              var response = await _repositorioProductos.CreateProduct(product,idClient);

                if(response.StatusCode == 201) return Ok(response);
                
                if(response.StatusCode == 500) return Problem();
            }

            return BadRequest(valid);
        }

        [Authorize]
        [HttpPut("Product")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var valid = _validationProduct.ValidationProductTrue(product);

            if (valid.Success)
            {
                var idClient = int.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "id").Value);
                product.IdClient = idClient;
                var response = await _repositorioProductos.UpdateProduct(product);

                if (response.Success) return Ok(response);

                if (response.StatusCode == 500) return Problem();
            }
            
            return BadRequest(valid);
        }

        [HttpDelete("Product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id != 0)
            {
               var response = await _repositorioProductos.DeleteProduct(id);
                if (response.Success) return Ok(response);
            }

            return NotFound();
        }
    }
}
