using BKGestionTareas.DTO;

namespace BKGestionTareas.Repository
{
    public interface ILoginRepository
    {
        bool VerifyUserExist(DtoUsuario dtoUsuario);
        string CreateToken(DtoUsuario dtoUsuario, string secretKey);
    }
}

