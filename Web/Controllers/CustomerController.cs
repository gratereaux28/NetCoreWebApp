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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetCustomers();
            var map = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            Customer customer = new Customer();
            JsonConvert.PopulateObject(values, customer);

            customer.Id= Guid.NewGuid();
            customer.CreateAt = DateTime.Now;

            if (!TryValidateModel(customer))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _customerService.InsertCustomer(customer);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(string values, Guid key)
        {
            Customer customer = await _customerService.GetCustomer(key);
            JsonConvert.PopulateObject(values, customer);

            customer.ModifiedAt = DateTime.Now;

            if (!TryValidateModel(customer))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _customerService.UpdateCustomer(customer);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            await _customerService.DeleteCustomer(key);
            return Ok();
        }

    }
}
