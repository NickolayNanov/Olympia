﻿namespace Olympia.Services.Contracts
{
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Shop;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShopService
    {
        Task<ShoppingCartViewModel> GetShoppingCartDtoByUserNameAsync(string username);

        Task<Item> GetItemByIdAsync(int itemId);

        IEnumerable<ItemViewModel> GetAllItems();

        Task<bool> CreateItemAsync(ItemBindingModel model);

        Task<IEnumerable<ItemViewModel>> GetAllItemsByCategoryAsync(string categoryName);

        Task<ItemViewModel> GetItemDtoByIdAsync(int itemId);

        IEnumerable<Supplier> GetAllSuppliers();

        Task<bool> AddItemToUserCartAsync(int itemId, string username);

        Task<IEnumerable<ItemViewModel>> GetTopFiveItemsAsync();

        Task<ShoppingCart> GetShoppingCartByUserNameAsync(string name);

        Task<ShoppingCart> GetShoppingCartByCartIdAsync(int cartId);

        Task<bool> RemoveFromCartAsync(string username, int itemId);

        Task<bool> DeleteItemAsync(int itemId);

        Task<bool> IncreaseTimesItemIsBoughtAsync(int itemId);

        Task<bool> DecreaseTimesItemIsBoughtAsync(int itemId);

        Task<bool> FinishOrderAsync(string name);

        Task<IEnumerable<Order>> GetAllOrdersByUsernameAsync(string name);

        Task<bool> CompleteOrderAsync(int orderId);
    }
}
