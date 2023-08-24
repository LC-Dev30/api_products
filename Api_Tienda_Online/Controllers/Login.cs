using Api_Tienda_Online.Services.Security;
using Api_Tienda_Online.Validation;
using Capa_Repositorio.dbContext;
using Capa_Repositorio.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api_Tienda_Online.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private IValidationClient _validationClient;
        private IToken _token;


        public Login(IValidationClient validationClient,IToken token)
        {
            _validationClient = validationClient;
            _token = token;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginClient(ClientLoginDto client)
         {
            var clientValidation = await _validationClient.clientValidationLogin(client);

            if(clientValidation.Success && clientValidation.StatusCode == 200 && clientValidation.Data != null)
            {
                var serializarClient = JsonSerializer.Serialize(clientValidation.Data);
                var clientConvert = JsonSerializer.Deserialize<Client>(serializarClient);
                var id = clientConvert.IdClient;

                var token = await _token.CreateToken(id);

                if( token != null)
                {
                    return Ok(new {Token = token, clientValidation});
                }
            }

            if (clientValidation.StatusCode == 404) return NotFound();

            if (clientValidation.StatusCode == 400) return BadRequest();

            return Problem();
        }

        [HttpPost("register")]
        public async Task<IActionResult> ClientRegister(ClientRegisterDto client)
        {
            var responseService = await _validationClient.clientRegister(client);

            if(responseService.Success) return Ok(responseService);

            return BadRequest();
        }
    }
}
