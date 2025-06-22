using Microsoft.AspNetCore.Mvc;
using SalesFlow.Api.Dto;
using SalesFlow.Application.Feature.Products.Commands;
using SalesFlow.Application.Feature.Products.Commands.CreateProduct;
using SalesFlow.Application.Feature.Products.Queries;
using SalesFlow.Application.Interfaces.Services;

namespace SalesFlow.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : BaseApiController
    {

        private readonly IProductServices _services;

        public ProductController(IProductServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductCommand command)
        {
            var dataId = await Mediator.Send(command);
            return Ok(dataId);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllProductQuery()));
        }


        [HttpGet]
        [Route("GetProductCompose")]
        public async Task<IActionResult> GetProductCompose()
        {
            return Ok(await Mediator.Send(new GetAllProductQuery()));
        }   
        
        
        [HttpGet]
        [Route("GetProductSimple")]
        public async Task<IActionResult> GetProductSimple()
        {
            return Ok(await Mediator.Send(new GetAllIProductSimple()));
        }

        [HttpGet]
        [Route("GetIngredients")]
        public async Task<IActionResult> GetIngridients()
        {
            return Ok(await Mediator.Send(new GetAllIngredientsQuery()));
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var response = await _services.GetProductsByCategoryAsync(categoryId);
            return Ok(response);
        }



        [HttpPost("upload-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No se recibió ninguna imagen.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var imageUrl = $"/images/products/{fileName}";

            return Ok(new { imageUrl });
        }



    }
}
