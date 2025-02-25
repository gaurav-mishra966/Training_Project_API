using CreditCard_Backend_API.Models.Domain;

namespace CreditCard_Backend_API.Repositories.Interface
{
    public interface IServicesRepository
    {
        Task<IEnumerable<Service>> GetAllServiceAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task AddServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
        Task DeleteServiceAsync(int id);
    }
}
