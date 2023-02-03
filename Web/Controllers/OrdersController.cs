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
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _ordersService.GetOrders();
            var map = _mapper.Map<IEnumerable<OrdersDTO>>(customers);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            Orders order = new Orders();
            OrdersDTO orderDTO = new OrdersDTO();
            JsonConvert.PopulateObject(values, order);
            JsonConvert.PopulateObject(values, orderDTO);

            order.Id= Guid.NewGuid();
            order.Date = DateTime.Now;

            if (orderDTO.stringDetalle != null)
            {
                var detalles = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDTO.stringDetalle);
                order.OrderDetails = detalles;
                orderDTO.OrderDetails = _mapper.Map<List<OrderDetailsDTO>>(detalles);
            }
            else
                order.OrderDetails = new List<OrderDetails>();

            if (!TryValidateModel(order))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _ordersService.InsertOrders(order);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(string values, Guid key)
        {
            Orders order = await _ordersService.GetOrder(key);
            OrdersDTO orderDTO = _mapper.Map<OrdersDTO>(order);
            JsonConvert.PopulateObject(values, order);
            JsonConvert.PopulateObject(values, orderDTO);

            if (orderDTO.stringDetalle != null)
            {
                var detalles = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDTO.stringDetalle);
                foreach (var item in detalles)
                {
                    if(item.Id == Guid.Empty)
                    {
                        item.Id = Guid.NewGuid();
                    }
                }
                order.OrderDetails = detalles;
                orderDTO.OrderDetails = _mapper.Map<List<OrderDetailsDTO>>(detalles);

            }
            else
                order.OrderDetails = new List<OrderDetails>();

            if (!TryValidateModel(order))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _ordersService.UpdateOrders(order);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            await _ordersService.DeleteOrders(key);
            return Ok();
        }

    }
}
