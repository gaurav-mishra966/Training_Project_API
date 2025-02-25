using CreditCard_Backend_API.Models.Domain;
using CreditCard_Backend_API.Models.DTO;
using CreditCard_Backend_API.Repositories.Implementation;
using CreditCard_Backend_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreditCard_Backend_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;

        public ServiceController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllService()
        {
            var services = await _servicesRepository.GetAllServiceAsync();
            return Ok(services);
        }

        //Create
        [HttpPost]
        [Route("/Service/addService")]
        public async Task<ActionResult> AddService([FromBody] ServicesRequestDTO serviceDTO)
        {
            if (serviceDTO == null)
            {
                return BadRequest("Invalid data.");
            }
            var service = new Service
            {
                ServiceName = serviceDTO.ServiceName,
                ServiceDescription = serviceDTO.ServiceDescription,
                ServiceCharges = serviceDTO.ServiceCharges,
                
            };

            await _servicesRepository.AddServiceAsync(service);
            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service);

        }

        //this is referenced in post method once the addition is done to return response
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var product = await _servicesRepository.GetServiceByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            await _servicesRepository.UpdateServiceAsync(service);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            var product = await _servicesRepository.GetServiceByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _servicesRepository.DeleteServiceAsync(id);
            return Ok(new { deletedProduct = new[] { product } });
        }
    }
}
