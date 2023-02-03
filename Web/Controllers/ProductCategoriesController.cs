using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Extensions;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductCategoriesService _productCategoriesService;

        public ProductCategoriesController(IProductCategoriesService productCategoriesService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _productCategoriesService = productCategoriesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _productCategoriesService.GetCategories();
            var map = _mapper.Map<IEnumerable<ProductCategoriesDTO>>(category);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            ProductCategories category = new ProductCategories();
            JsonConvert.PopulateObject(values, category);

            category.Id= Guid.NewGuid();
            category.CreateAt = DateTime.Now;

            if (!TryValidateModel(category))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _productCategoriesService.InsertCategory(category);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(string values, Guid key)
        {
            ProductCategories category = await _productCategoriesService.GetCategory(key);
            JsonConvert.PopulateObject(values, category);

            category.ModifiedAt = DateTime.Now;

            if (!TryValidateModel(category))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _productCategoriesService.UpdateCategory(category);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            await _productCategoriesService.DeleteCategory(key);
            return Ok();
        }

    }
}
