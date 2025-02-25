using CreditCard_Backend_API.Models.Domain;
using CreditCard_Backend_API.Repositories.Interface;

namespace CreditCard_Backend_API.Repositories.Implementation
{
    public class ServiceRepository : IServicesRepository
    {
        private readonly List<Service> _services;

        public ServiceRepository()
        {
            _services = new List<Service>
            {
                new Service { Id = 1, ServiceName = "Visa Platinum Service", ServiceDescription = "Enjoy exclusive benefits and rewards.", ServiceCharges = 0 },
                new Service { Id = 2, ServiceName = "MasterCard Gold Service", ServiceDescription = "Get the best cash back rewards.", ServiceCharges = 95 },
                new Service { Id = 3, ServiceName = "American Express Green Service", ServiceDescription = "Earn points on every purchase.", ServiceCharges = 150},
                new Service { Id = 4, ServiceName = "Platinum Service", ServiceDescription = "Enjoy exclusive benefits and rewards.", ServiceCharges = 0 },
                new Service { Id = 5, ServiceName = "Gold Service", ServiceDescription = "Get the best cash back rewards.", ServiceCharges = 95 },
                new Service { Id = 6, ServiceName = "Express Green Service", ServiceDescription = "Earn points on every purchase.", ServiceCharges = 150},
                new Service { Id = 7, ServiceName = "Visa Service", ServiceDescription = "Enjoy exclusive benefits and rewards.", ServiceCharges = 0 },
                new Service { Id = 8, ServiceName = "Master Card Service", ServiceDescription = "Get the best cash back rewards.", ServiceCharges = 95 },
                new Service { Id = 9, ServiceName = "American Green Service", ServiceDescription = "Earn points on every purchase.", ServiceCharges = 150}
            };
        }

        public async Task<IEnumerable<Service>> GetAllServiceAsync()
        {
            return await Task.FromResult(_services);
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            var service = _services.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(service);
        }

        public async Task AddServiceAsync(Service services)
        {
            _services.Add(services);
            await Task.CompletedTask;
        }

        public async Task UpdateServiceAsync(Service service)
        {
            var existingProduct = _services.FirstOrDefault(p => p.Id == service.Id);
            if (existingProduct != null)
            {
                existingProduct.ServiceName = service.ServiceName;
                existingProduct.ServiceDescription = service.ServiceDescription;
                existingProduct.ServiceCharges = service.ServiceCharges;
                
            }
            await Task.CompletedTask;
        }

        public async Task DeleteServiceAsync(int id)
        {
            var product = _services.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _services.Remove(product);
            }
            await Task.CompletedTask;
        }
    }
}
