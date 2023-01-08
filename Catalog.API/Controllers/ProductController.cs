using Catalog.Api.Entities;
using Catalog.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        public ProductController(Logger<ProductController> logger, IProductRepository productRepository)
        {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult>Get()
        {
            var product = await _productRepository.GetProducts();
            return Ok(product);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product ==null)
            {
                _logger.LogError($"Product with the given id {id} is not found");

                return NotFound($"Product with the given id {id} is not found");
            }
            return Ok(product);

        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
             await _productRepository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
