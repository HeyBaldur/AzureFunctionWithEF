using System.Threading.Tasks;

namespace AzureFunctionWithEF.Services.Interfaces
{
    /// <summary>
    /// Validate and get cnx strings
    /// </summary>
    public interface IAccessConnectionString
    {
        Task<string> GetConnectionString();
        void ValidateConnectionString();
    }
}
