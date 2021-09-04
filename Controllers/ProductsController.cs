using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevReviews.API.Models;
using DevReviews.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DevReviewsDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductsController(DevReviewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //GET para api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _dbContext.Products;
            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);
            return Ok(productsViewModel);
        }
        //GET para api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Se não achar, retorna NotFound()
            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            //Se for nulo retorna NotFound()
            if (product == null)
            {
                return NotFound();
            }
            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);
            return Ok(productDetails);
        }
        //Post para api/products
        [HttpPost]
        public IActionResult Post(AddProductInputModel model)
        {
            //Set tiver erros de validação, retornar BadRequest()
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }
        //Put para api/products/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProductInputModel model)
        {
            //Set tiver erros de validação, retornar BadRequest()
            //Se não existir produto com id especificado, retornar NotFound()
            if (model.Desiption.Length > 50)
            {
                return BadRequest();
            }
            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            //Se for nulo retorna NotFound()
            if (product == null)
            {
                return NotFound();
            }
            //Atualiza dados
            product.Update(model.Desiption, model.Price);

            return NoContent();
        }
    }
}