using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;
using RelationShipManager.Services;
using System.Collections.Generic;
using System.Linq;

namespace RelationShipManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        private IProductService _productService;
        private RelShip_ManContext _db = new RelShip_ManContext();

        public ProductController(
            IProductService productService,
            IOptions<AppSettings> appSettings,
            IMapper mapper
        )
        {
            _productService = productService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var productService = new ProductService();
            try
            {
                var Products = _db.Product.ToList();
                return Ok(Products);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
