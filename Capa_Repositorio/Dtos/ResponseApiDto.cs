using System.Security.Claims;

namespace Capa_Repositorio.Dtos
{
    public class ResponseApiDto
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public bool Success  { get; set; }
        public ClaimsIdentity Identity { get; set; }
    }
}
