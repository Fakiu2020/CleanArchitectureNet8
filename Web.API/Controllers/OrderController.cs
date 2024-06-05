using Application.Command.Order.Create;
using Application.Command.Order.Delete;
using Application.Command.Order.Update;
using Application.Queries.Order.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Common;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : BaseController
    {


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var result=  (await Mediator.Send(new GetAllOrderQuery()));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var result = (await Mediator.Send(new GetByIdOrderQuery() { OrderId = orderId }));
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }


   

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateOrderCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteOrderCommand() { OrderId=id});
            if (result.Succeeded)
                return Ok();
            return BadRequest(result.Errors);
        }
    }
}
