using Actividadenclase.Data.Models;
using Actividadenclase.Data.Repositories;
using Actividadenclase.Data.Repositories.Interfaces;
using Actividadenclase.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Actividadenclase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        //// GET: api/<ProductsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _productRepository.GetAllProductsAsync().Result;
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
           var response = _productRepository.GetProductByIdAsync(id).Result;
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var data = _productRepository.GetProductByIdAsync(id).Result;

            if (data == null)
            {
                return BadRequest("El producto no existe");
            }

            var response = _productRepository.DeleteProductAsync(id).Result;

            return Ok(response);
        }

        //POST api/<ProductsController>

        [HttpPost]
        public IActionResult Post([FromBody] CreateProductDTO request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Category = request.Category,

            };

            _productRepository.CreateProductAsync(newProduct).Wait();


            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UpdateProductDTO request)
        {
            var data = _productRepository.GetProductByIdAsync(id).Result;

            if (data == null)
            {
                return BadRequest("El producto no existe");
            }

            data.Name = request.Name;
            data.Description = request.Description;
            data.Price = request.Price;
            data.Stock = request.Stock;
            data.Category = request.Category;
            


           var response = _productRepository.UpdateProductAsync(id,data).Result;


            return Ok(response);
        }

        //// PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}
    }
}
