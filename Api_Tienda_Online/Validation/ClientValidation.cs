using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Api_Tienda_Online.Validation
{
    public class ClientValidation : IValidationClient
    {
        private TiendaOnlineContext _context;
        public ClientValidation(TiendaOnlineContext context) 
        {
            _context = context;
        }

        public async Task<ResponseApiDto> clientValidationLogin(ClientLoginDto client)
        {
            if(string.IsNullOrEmpty(client.Gmail) || string.IsNullOrEmpty(client.Password))
            {
                return new ResponseApiDto { Success = false, StatusCode = 400};
            }

            var seachClient = await _context.Clients.Where(c =>
                   c.Email == client.Gmail 
                   && c.Password == client.Password)
                   .FirstOrDefaultAsync();

            if(seachClient != null)
            {
                return new ResponseApiDto
                {
                    Success = true,
                    Data = seachClient,
                    StatusCode = 200
                };
            }

            return new ResponseApiDto
            {
                Success = false,
                StatusCode = 404
            };

        }

        public async Task<ResponseApiDto> clientRegister(ClientRegisterDto client)
        {
          
            await _context.Clients.AddAsync(new Client
            {
                NamesAndSurnames= client.NamesAndSurnames,
                Email= client.Gmail,    
                Password= client.Password,
                Phone= client.Phone,
            });

            var confirSave = await _context.SaveChangesAsync();

            if(confirSave > 0)
            {
                return new ResponseApiDto
                {
                    Success = true,
                    StatusCode = 200
                };
            }

            return new ResponseApiDto
            {
                Success = false,
            };
        }
    }
}
