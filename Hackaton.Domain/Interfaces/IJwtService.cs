namespace Hackaton.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid medicoId, string email, string role);
    }

}
