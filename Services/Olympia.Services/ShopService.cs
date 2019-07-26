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
            var cart = await this.GetShoppingCartByUserNameAsync(username);
            
            if(cart.ShoppingCartItems.Select(x => x.ItemId).Contains(itemId))
            {
                return false;
            }

            cart.ShoppingCartItems.Add(new ShoppingCartItem() { ItemId = itemId, ShoppingCartId = cart.Id });

            this.context.Update(cart);
            await this.context.SaveChangesAsync();
            
            return cart.ShoppingCartItems.Select(x => x.ItemId).Contains(itemId);
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
            return this.context.Suppliers.Include(x => x.Items).AsEnumerable();
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

        public async Task<ShoppingCartViewModel> GetShoppingCartDtoByUserNameAsync(string name)
        {
            var shoppingCart = (await this.context
                .Users
                .Include(user => user.ShoppingCart)
                .ThenInclude(shc => shc.ShoppingCartItems)
                .ThenInclude(sh => sh.Item)
                .SingleOrDefaultAsync(u => u.UserName == name)).ShoppingCart;

            var cartViewModel = this.mapper.Map<ShoppingCartViewModel>(shoppingCart);

            cartViewModel.Items = this.mapper.ProjectTo<ItemViewModel>
                (shoppingCart.ShoppingCartItems.Select(x => x.Item).AsQueryable()).ToList();

            return cartViewModel;
        }

        public async Task<ShoppingCart> GetShoppingCartByUserNameAsync(string name)
        {
            var shoppingCart = (await this.context
                .Users
                .Include(user => user.ShoppingCart)
                .ThenInclude(shc => shc.ShoppingCartItems)
                .ThenInclude(sh => sh.Item)
                .ThenInclude(sh => sh.ShoppingCartItems)
                .ThenInclude(sh => sh.ShoppingCart)
                .SingleOrDefaultAsync(u => u.UserName == name)).ShoppingCart;

            return shoppingCart;
        }

        public async Task<ShoppingCart> GetShoppingCartByCartIdAsync(int cartId)
        {
            var cart = await
                this.context
                .ShoppingCarts
                .Include(shc => shc.ShoppingCartItems)
                .ThenInclude(shci => shci.Item)
                .Include(shc => shc.ShoppingCartItems)
                .ThenInclude(shci => shci.ShoppingCart)
                .SingleOrDefaultAsync(shc => shc.Id == cartId);

            return cart;
        }

        public async Task<bool> RemoveFromCartAsync(string username, int itemId)
        {
            var cart = await this.GetShoppingCartByUserNameAsync(username);

            var cartItemsIds = cart.ShoppingCartItems.Select(x => x.ItemId);

            if (cartItemsIds.Contains(itemId))
            {
                var cartItem = cart.ShoppingCartItems
                    .SingleOrDefault(shc => 
                                    shc.ShoppingCartId == cart.Id && 
                                    shc.ItemId == itemId);

                cart.ShoppingCartItems.Remove(cartItem);
            }

            this.context.Update(cart);
            await this.context.SaveChangesAsync();

            return (await this.GetShoppingCartByUserNameAsync(username))
                .ShoppingCartItems.Select(x => x.ItemId).Contains(itemId);
        }

        public async Task<bool> DeleteItemAsync(int itemId)
        {
            var item = await this.context.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            this.context.ShoppingCartItems.RemoveRange(this.context.ShoppingCartItems.Where(x => x.ItemId == item.Id));
            this.context.ItemCategories.RemoveRange(this.context.ItemCategories.Where(x => x.ItemId == item.Id));
            this.context.Items.Remove(item);

            await this.context.SaveChangesAsync();

            return this.context.Items.Contains(item);
        }
    }
}
