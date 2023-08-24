namespace Api_Tienda_Online.Services.Security
{
    public interface IToken
    {
        Task<string> CreateToken(int id);
    }
}
