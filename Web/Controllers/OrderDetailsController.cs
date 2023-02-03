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
    public class OrderDetailsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _orderDetailsService = orderDetailsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid orderID)
        {
            var details = await _orderDetailsService.GetDetailsByOrderId(orderID);
            var map = _mapper.Map<IEnumerable<OrderDetailsDTO>>(details);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

    }
}
