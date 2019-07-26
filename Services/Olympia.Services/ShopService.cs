namespace Olympia.Services
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data;
    using Olympia.Data.Domain;
    using Olympia.Data.Models.BindingModels.Shop;
    using Olympia.Data.Models.ViewModels.Shop;
    using Olympia.Services.Contracts;
    using Olympia.Services.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShopService : IShopService
    {
        private readonly IMapper mapper;
        private readonly OlympiaDbContext context;

        public ShopService(
            IMapper mapper,
            OlympiaDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> AddItemToUserCart(int itemId, string username)
        {
            var item = await this.GetItemByIdAsync(itemId);
            var cart = await this.GetShoppingCartByUserNameAsync(username);


            if (cart.Items.Contains(item))
            {
                return false;
            }

            cart.Items.Add(item);
            item.ShoppingCardId = cart.Id;

            this.context.Update(item);
            this.context.Update(cart);
            await this.context.SaveChangesAsync();

            return cart.Items.Contains(item);
        }

        public async Task<bool> CreateItemAsync(ItemBindingModel model)
        {
            if (model.Price <= 0 ||
                string.IsNullOrEmpty(model.Name) ||
                string.IsNullOrEmpty(model.Description) ||
                model.ImgUrl == null)
            {
                return false;
            }

            var item = this.mapper.Map<Item>(model);

            var url = MyCloudinary.UploadImage(model.ImgUrl, model.Name);

            item.ImgUrl = url ?? Constants.CloudinaryInvalidUrl;

            var category = this.context.ChildCategories.FirstOrDefault(cat => cat.Name == model.CategoryName.ToString());
            var supplier = this.context.Suppliers.FirstOrDefault(sup => sup.Name == model.SupplierName);

            item.ItemCategories.Add(new ItemCategory { ChildCategoryId = category.Id });
            item.Supplier = supplier;

            await this.context.Items.AddAsync(item);
            await this.context.SaveChangesAsync();

            return this.context.Items.Contains(item);
        }

        public IEnumerable<ItemViewModel> GetAllItems()
        {
            var itemsViewModels = this.mapper.ProjectTo<ItemViewModel>(this.context.Items).AsEnumerable();

            return itemsViewModels;
        }

        public async Task<IEnumerable<ItemViewModel>> GetAllItemsByCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return new List<ItemViewModel>();
            }

            IEnumerable<ItemViewModel> ShopViewModels = new List<ItemViewModel>();

            await Task.Run(async () =>
            {
                ShopViewModels = this.mapper.ProjectTo<ItemViewModel>((await this.context.ChildCategories
                .Include(x => x.ItemCategories)
                .ThenInclude(ic => ic.Item)
                .ThenInclude(item => item.Supplier)
                .FirstOrDefaultAsync(x => x.Name == categoryName))
                .ItemCategories
                .Select(x => x.Item)
                .AsQueryable())
                .AsEnumerable();
            });

            return ShopViewModels;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return this.context.Suppliers.AsEnumerable();
        }

        public async Task<ItemViewModel> GetItemDtoByIdAsync(int itemId)
        {
            var itemDto = this.mapper.Map<ItemViewModel>(await this.context.Items.Include(item => item.Supplier).SingleOrDefaultAsync(item => item.Id == itemId));

            return itemDto;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            var itemFromDb = await this.context.Items.SingleOrDefaultAsync(item => item.Id == itemId);

            return itemFromDb;
        }

        public async Task<IEnumerable<ItemViewModel>> GetTopFiveItemsAsync()
        {
            IEnumerable<ItemViewModel> items = new List<ItemViewModel>();

            await Task.Run(() =>
            {
                items = this.mapper
                            .ProjectTo<ItemViewModel>(this.context
                                .Items
                                .Include(item => item.Supplier)
                                .OrderByDescending(x => x.TimesBought)
                                .Take(5))
                            .AsEnumerable();
            });

            return items;
        }

        public async Task<ShoppingCart> GetShoppingCartByUserNameAsync(string name)
        {
            var shoppingCart = (await this.context
                .Users
                .Include(user => user.ShoppingCart)
                .SingleOrDefaultAsync(u => u.UserName == name)).ShoppingCart;

            shoppingCart.Items = this.context.Items.Where(i => i.ShoppingCardId == shoppingCart.Id).ToList();

            return shoppingCart;
        }

        public async Task<ShoppingCart> GetShoppingCartByCartIdAsync(int cartId)
        {
            var cart = await
                this.context
                .ShoppingCarts
                .Include(x => x.Items)
                .Include(shc => shc.User)
                .SingleOrDefaultAsync(shc => shc.Id == cartId);

            return cart;
        }

        public async Task<bool> RemoveFromCartAsync(int cartId, int itemId)
        {
            var cart = await this.GetShoppingCartByCartIdAsync(cartId);
            var item = await this.GetItemByIdAsync(itemId);

            cart.Items.Remove(item);
            item.ShoppingCardId = 0;

            this.context.Update(cart);
            this.context.Update(item);

            await this.context.SaveChangesAsync();

            return (await this.GetShoppingCartByCartIdAsync(cartId)).Items.Contains(item);
        }
    }
}
