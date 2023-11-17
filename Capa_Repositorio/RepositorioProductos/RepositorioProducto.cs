using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capa_Repositorio.RepositorioProductos
{
    public class RepositorioProducto : IProductosRepositorio
    {
        //context
        private TiendaOnlineContext _context;
     

        public RepositorioProducto(TiendaOnlineContext context)
        {
            _context = context; 
        }

        public async Task<ResponseApiDto> ListProducts(int idClient)
        {
            try
            {         
                var listaProductos = await _context.Products
                       .Include(e => e.CategoryNavigation)
                       .Where(p => p.IdClient == idClient)
                       .Select(p => new ProductsDto
                       {
                           IdProduct = p.IdProduct,
                           Name = p.Name,
                           Description = p.Description,
                           Price = p.Price,
                          Category = p.CategoryNavigation.NameCategory
                       }).ToListAsync();

                return new ResponseApiDto
                {
                    StatusCode= 200,
                    Data = listaProductos,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDto
                {
                    StatusCode = 500,
                    Data = ex.Message,
                    Success = false
                };
            }
        }

        public Task<ResponseApiDto> Products(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseApiDto> CreateProduct(CreateProductDto product,int idClient)
        {
            var convertPrice = int.Parse(product.Price).ToString("N2");
           
            await _context.AddAsync(new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = decimal.Parse(convertPrice),
                Category = product.Category,
                IdClient = idClient
            });

             await _context.SaveChangesAsync();
            
            return new ResponseApiDto
            {
                StatusCode = 201,
                Data = product,
                Success = true
            };
        }

        public async Task<ResponseApiDto> DeleteProduct(int idProduct)
        {
            var entity = await _context.Products.FindAsync(idProduct);
            
            if(entity != null)
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();

                return new ResponseApiDto
                {
                    StatusCode = 200,
                    Success = true,
                    Data = "Product deleted!"
                };
            }

            return new ResponseApiDto
            {
              Success = false,
              Data = "Product not found",
              StatusCode = 200
            };
        }

        public async Task<ResponseApiDto> UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();

            return new ResponseApiDto
            {
                StatusCode = 200,
                Data = "updated product!",
                Success = true
            };
        }
    }
}
