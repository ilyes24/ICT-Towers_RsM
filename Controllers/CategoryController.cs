using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelationShipManager.Dtos;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;
using RelationShipManager.Services;

namespace RelationShipManager.Controllers
{
    //[Authorize]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly RelShip_ManContext _db = new RelShip_ManContext();

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper
        )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [Produces("application/json")]
        [HttpGet("")]
#pragma warning disable 1998
        public async Task<IActionResult> FindAll()
#pragma warning restore 1998
        {
            var productService = new ProductService();
            try
            {
                var Categorys = _db.Category.ToList();
                foreach (var category in Categorys)
                    category.Product = (ICollection<Product>) productService.GetAll(category);
                return Ok(Categorys);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
#pragma warning disable 1998
        public async Task<IActionResult> Find(int id)
#pragma warning restore 1998
        {
            try
            {
                var Categorys = _db.Category.Find(id);
                return Ok(Categorys);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpPost("")]
        public IActionResult Add([FromBody] CategoryDto categoryDtro)
        {
            var category = _mapper.Map<Category>(categoryDtro);
            try
            {
                var c = _categoryService.Create(category);
                return Ok(c);
            }
            catch (AppException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.IdCategory = id;
            try
            {
                _categoryService.Update(category);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }
        }

        [Produces("application/json")]
        [HttpDelete]
        public Task<IActionResult> Delete(int id)
        {
            _categoryService.Delete(id);
            return FindAll();
        }
    }
}
