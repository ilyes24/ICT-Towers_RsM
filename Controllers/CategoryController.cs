using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private ICategoryService _categoryService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private RelShip_ManContext db = new RelShip_ManContext();

        public CategoryController(
          ICategoryService categoryService,
          IOptions<AppSettings> appSettings,
          IMapper mapper
          )
        {
          _categoryService = categoryService;
          _appSettings = appSettings.Value;
          _mapper = mapper;
        }

        [Produces("application/json")]
        [HttpGet("")]
        public async Task<IActionResult> FindAll()
        {
            ProductService productService = new ProductService();
            try
            {
                var Categorys = db.Category.ToList();
              foreach (var category in Categorys)
              {
                category.Product = (ICollection<Product>)productService.GetAll(category);
              }
                return Ok(Categorys);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var Categorys = db.Category.Find(id);
                return Ok(Categorys);
            }
            catch
            {
                return BadRequest();
            }

        }

      [AllowAnonymous]
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
    }
}
