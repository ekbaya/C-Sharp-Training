using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
               Id = item.Id,
               Name = item.Name,
               Price = item.Price,
               CreatedDate = item.CreatedDate
           };
        }
    }
}


/*
Extension methods enable you to "add" methods to existing types without creating a new 
derived type, recompiling, or otherwise modifying the original type. Extension methods
are static methods, but they're called as if they were instance methods on the extended 
type. 
*/