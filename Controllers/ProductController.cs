using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;
using RelationShipManager.Services;

namespace RelationShipManager.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
      private IProductService _productService;
      private IMapper _mapper;
      private readonly AppSettings _appSettings;
      private RelShip_ManContext db = new RelShip_ManContext();

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
  }
}
