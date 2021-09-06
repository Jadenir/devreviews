using System.Threading.Tasks;
using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevReviews.API.Controllers
{
    [ApiController]
    [Route("api/products/{productId}/produtctreviews")]
    public class ProductReviewsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductReviewsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/products/1/productreviews/5
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int productId, int id)
        {
            //Consulta ProductReviews no dbContext
            var productReviews = await _repository.GetReviewByIdAsync(id);
            //Se não existir com o id especificado, retorna NotFound()
            if (productReviews == null)
            {
                return NotFound();
            }
            //Mapeia o Details da consulta realizada
            var productDetails = _mapper.Map<ProductReviewDetailsViewModel>(productReviews);
            //Retorna para a View o resultado
            return Ok(productDetails);
        }

        //POST api/products/1/productreviews/5
        [HttpPost]
        public async Task<IActionResult> Post(int productId, AddProductReviewInputModel model)
        {
              var productReview = new ProductReview(model.Author, model.Rating,model.Comments, productId);

            await _repository.AddReviewAsync(productReview);
            
            return CreatedAtAction(nameof(GetById), new { id = productReview.Id, productId = productId }, model);
        }
    }
}