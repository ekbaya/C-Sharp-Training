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

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
           Item item = new()
           {
               Id = Guid.NewGuid(),
               Name = itemDto.Name,
               Price = itemDto.Price,
               CreatedDate = DateTimeOffset.UtcNow
           };

           repository.CreateItem(item);

           return CreatedAtAction(nameof(GetItem), new{id = item.Id}, item.AsDto());
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            /*
            We are taking the existing Item , Creating a copy of it with 
            ("With-Expression") With the Above two properties modified for new values
            */

            repository.UpdateItem(updatedItem);
            return NoContent();
        }
        
        //DELETE /items/{id}
        [HttpDelete("{id}")]
        
        public ActionResult DeleteItem(Guid id)
        {
           var existingItem = repository.GetItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);

            return NoContent();
        }
    }
}