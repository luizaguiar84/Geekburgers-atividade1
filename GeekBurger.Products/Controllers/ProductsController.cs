using AutoMapper;
using GeekBurger.Products.Contract;
using GeekBurger.Products.Models;
using GeekBurger.Products.Repositories;
using GeekBurger.Products.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Products.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;
        private readonly IProductChangedService _productChangedService;

        public ProductController(
            IProductsRepository productsRepository, 
            IStoreRepository storeRepository, 
            IMapper mapper, 
            IProductChangedService productChangedService)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _productChangedService = productChangedService;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productsRepository.GetProductById(id);

            var productToGet = _mapper.Map<ProductToGet>(product);

            return Ok(productToGet);
        }

        [HttpPost()]
        public IActionResult AddProduct([FromBody] ProductToUpsert productToAdd)
        {
            if (productToAdd == null)
                return BadRequest();

            var product = _mapper.Map<Product>(productToAdd);

            if (product.StoreId == Guid.Empty)
                return new Helper.UnprocessableEntityResult(ModelState);

            _productsRepository.Add(product);
            _productsRepository.Save();

            _productChangedService.SendMessagesAsync();

            var productToGet = _mapper.Map<ProductToGet>(product);

            return CreatedAtRoute("GetProduct",
                new { id = productToGet.ProductId },
                productToGet);
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateProduct(Guid id, [FromBody] JsonPatchDocument<ProductToUpsert> productPatch)
        {
            Product product;

            if (productPatch == null)
                return BadRequest();

            product = _productsRepository.GetProductById(id);

            if (id == null || product == null)
            {
                return NotFound();
            }

            var productToUpdate = _mapper.Map<ProductToUpsert>(product);
            productPatch.ApplyTo(productToUpdate);

            product = _mapper.Map(productToUpdate, product);

            if (product.StoreId == Guid.Empty)
                return new Helper.UnprocessableEntityResult(ModelState);
            
            _productsRepository.Update(product);
            _productsRepository.Save();

            var productToGet = _mapper.Map<ProductToGet>(product);

            return CreatedAtRoute("GetProduct",
                new { id = productToGet.ProductId },
                productToGet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _productsRepository.GetProductById(id);

            _productsRepository.Delete(product);
            _productsRepository.Save();
            return NoContent();
        }
    }
}