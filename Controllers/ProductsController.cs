using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET para api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();

            var productsViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return Ok(productsViewModel);
        }
        //GET para api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repository.GetDetailsByIdAsync(id);
            //Se for nulo retorna NotFound()
            if (product == null)
            {
                return NotFound();
            }
            //Grava um log
            Log.Information("GET por ID chamado com ID: " + id);
            
            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);
            return Ok(productDetails);
        }
        //Post para api/products
        /// <summary>Cadastro de Produto</summary>
        /// <remarks>Requisição:
        /// {
        /// "title": "Um chinelo top"  ,
        /// "description": "Um chienelo de marca", 
        /// "price": 100
        /// }
        ///</remarks>
        /// <param name="model">Obejgo com dados de cadastro de produto</param>
        /// <returns>Objeto recém criado</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AddProductInputModel model)
        {
            var product = new Product(model.Title, model.Desiption, model.Price);

            await _repository.AddAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }
        //Put para api/products/{id}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductInputModel model)
        {
            var product = await _repository.GetByIdAsync(id);
            //Se for nulo retorna NotFound()
            if (product == null)
            {
                return NotFound();
            }
            //Atualiza dados
            product.Update(model.Desiption, model.Price);

            await _repository.UpdateAsync(product);

            return NoContent();
        }
    }
};