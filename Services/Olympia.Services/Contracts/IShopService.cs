namespace Olympia.Services.Contracts
{
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Shop;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShopService 
    {
        Task<Item> GetItemByIdAsync(int itemId);

        IEnumerable<ItemViewModel> GetAllItems();

        Task<bool> CreateItemAsync(ItemBindingModel model);

        Task<IEnumerable<ItemViewModel>> GetAllItemsByCategory(string categoryName);

        Task<ItemViewModel> GetItemDtoByIdAsync(int itemId);

        IEnumerable<Supplier> GetAllSuppliers();

        Task<bool> AddItemToUserCart(int itemId, string username);

        Task<IEnumerable<ItemViewModel>> GetTopFiveItemsAsync();

        Task<ShoppingCart> GetShoppingCartByUserNameAsync(string name);

        Task<ShoppingCart> GetShoppingCartByCartIdAsync(int cartId);

        Task<bool> RemoveFromCartAsync(int cartId, int itemId);
    }
}
