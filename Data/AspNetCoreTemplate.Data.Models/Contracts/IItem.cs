namespace Olympia.Data.Domain.Enums
{
    using System.Collections.Generic;

    public interface IItem
    {
         string Name { get; }

         decimal Price { get; }

         ICollection<Review> Reviews { get; }
         
         ShoppingCart ShoppingCart { get; }
    }
}
