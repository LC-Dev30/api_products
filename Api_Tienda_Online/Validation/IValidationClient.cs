using Capa_Repositorio.Dtos;

namespace Api_Tienda_Online.Validation
{
    public interface IValidationClient
    {
        Task<ResponseApiDto> clientValidationLogin(ClientLoginDto client);
        Task<ResponseApiDto> clientRegister(ClientRegisterDto client);
    }
}
