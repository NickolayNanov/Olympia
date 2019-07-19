﻿namespace Olympia.Services.Contracts
{
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Shop;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShopService 
    {
        IEnumerable<ItemViewModel> GetAllItems();

        Task<bool> CreateItemAsync(ItemBindingModel model);

        Task<IEnumerable<ItemViewModel>> GetAllItemsByCategory(string categoryName);
        Task<ItemViewModel> GetItemByIdAsync(int itemId);
    }
}