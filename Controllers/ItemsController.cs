using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections;
using Catalog.Entities;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository  repository)//Dependency Injection
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
           var items = repository.GetItems().Select(item=> new ItemDto
           {
               Id = item.Id,
               Name = item.Name,
               Price = item.Price,
               CreatedDate = item.CreatedDate
           });/*This will ensure that u do not break the clients when you are changing 
           the Item as the database Evolve*/
           return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if(item is null){
                return NotFound();
            }
            return item.AsDto();
        }
    }
}