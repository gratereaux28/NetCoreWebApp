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
    /// <summary>
    /// This controller make all configuration and contains all the method to work with the orders
    /// </summary>
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService, IMapper mapper) : base()
        {
            _mapper = mapper;
            _ordersService = ordersService;
        }

        /// <summary>
        /// This method get the initial view
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method get all orders in database
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _ordersService.GetOrders();

            //Here we are mapping the original model into a dto to hidden the original mode
            var map = _mapper.Map<IEnumerable<OrdersDTO>>(customers);
            string result = JsonConvert.SerializeObject(map, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(result);
        }

        /// <summary>
        /// This method insert a new oder
        /// </summary>
        /// <param name="values">Contains all change in json format</param>
        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            Orders order = new Orders();
            OrdersDTO orderDTO = new OrdersDTO();
            JsonConvert.PopulateObject(values, order);
            JsonConvert.PopulateObject(values, orderDTO);

            order.Id= Guid.NewGuid();
            order.Date = DateTime.Now;

            //Here we transform all details because we send all details in json format
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

        /// <summary>
        /// This method update a existing oder
        /// </summary>
        /// <param name="values">Contains all change in json format</param>
        /// <param name="key">Contains the key of the order</param>
        [HttpPut]
        public async Task<IActionResult> Put(string values, Guid key)
        {
            Orders order = await _ordersService.GetOrder(key);
            OrdersDTO orderDTO = _mapper.Map<OrdersDTO>(order);
            JsonConvert.PopulateObject(values, order);
            JsonConvert.PopulateObject(values, orderDTO);

            //Here we transform all details because we send all details in json format
            if (orderDTO.stringDetalle != null)
            {
                var detalles = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDTO.stringDetalle);
                foreach (var item in detalles)
                {
                    //Here we add a nre guid because front end send to server an empty id
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

        /// <summary>
        /// This method delete a existing oder with his all dependency
        /// </summary>
        /// <param name="key">Contains the key of the order</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            await _ordersService.DeleteOrders(key);
            return Ok();
        }

    }
}
