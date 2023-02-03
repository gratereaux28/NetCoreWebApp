using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Extensions;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _productsService = productsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productsService.GetProduct(id);
            var map = _mapper.Map<ProductsDTO>(product);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productsService.GetProducts();
            var map = _mapper.Map<IEnumerable<ProductsDTO>>(products);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            Products product = new Products();
            JsonConvert.PopulateObject(values, product);

            product.Id= Guid.NewGuid();
            product.CreateAt = DateTime.Now;

            if (!TryValidateModel(product))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _productsService.InsertProduct(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(string values, Guid key)
        {
            Products product = await _productsService.GetProduct(key);
            JsonConvert.PopulateObject(values, product);

            product.ModifiedAt = DateTime.Now;

            if (!TryValidateModel(product))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _productsService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            await _productsService.DeleteProduct(key);
            return Ok();
        }

    }
}
