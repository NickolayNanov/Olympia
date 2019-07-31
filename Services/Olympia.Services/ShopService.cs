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
        private readonly IUsersService usersService;
        private readonly OlympiaDbContext context;

        public ShopService(
            IMapper mapper,
            IUsersService usersService,
            OlympiaDbContext context)
        {
            this.mapper = mapper;
            this.usersService = usersService;
            this.context = context;
        }

        public async Task<bool> AddItemToUserCartAsync(int itemId, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            var cart = await this.GetShoppingCartByUserNameAsync(username);

            if (cart == null)
            {
                return false;
            }

            if (cart.ShoppingCartItems.Select(x => x.ItemId).Contains(itemId))
            {
                return false;
            }

            var item = await this.GetItemByIdAsync(itemId);

            if (item == null)
            {
                return false;
            }


            item.TimesBought = 1;

            cart.ShoppingCartItems.Add(new ShoppingCartItem() { ItemId = itemId, ShoppingCartId = cart.Id });

            this.context.Update(cart);
            this.context.Update(item);
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

        public async Task<IEnumerable<ItemViewModel>> GetAllItemsByCategoryAsync(string categoryName)
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
                .FirstOrDefaultAsync(x => x.Name == categoryName))?
                .ItemCategories
                .Select(x => x.Item)
                .OrderByDescending(x => x.CreatedOn)
                .AsQueryable())
                .AsEnumerable();
            });

            if (ShopViewModels == null)
            {
                return null;
            }

            return ShopViewModels;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return this.context.Suppliers.Include(x => x.Items).AsEnumerable();
        }

        public async Task<ItemViewModel> GetItemDtoByIdAsync(int itemId)
        {
            var itemDto = this.mapper.Map<ItemViewModel>(
                await this.context
                            .Items
                            .Include(item => item.Supplier)
                            .SingleOrDefaultAsync(item => item.Id == itemId));

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
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var shoppingCart = (await this.context
                .Users
                .Include(user => user.ShoppingCart)
                .ThenInclude(shc => shc.ShoppingCartItems)
                .ThenInclude(sh => sh.Item)
                .SingleOrDefaultAsync(u => u.UserName == name)).ShoppingCart;

            if (shoppingCart == null)
            {
                return null;
            }

            var cartViewModel = this.mapper.Map<ShoppingCartViewModel>(shoppingCart);

            var items = this.mapper.ProjectTo<ItemViewModel>
                (shoppingCart.ShoppingCartItems.Select(x => x.Item).AsQueryable()).ToList();

            cartViewModel.Items = items;

            return cartViewModel;
        }

        public async Task<ShoppingCart> GetShoppingCartByUserNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var userFromDb = (await this.context
                .Users
                .Include(user => user.ShoppingCart)
                .ThenInclude(shc => shc.ShoppingCartItems)
                .ThenInclude(sh => sh.Item)
                .ThenInclude(sh => sh.ShoppingCartItems)
                .ThenInclude(sh => sh.ShoppingCart)
                .SingleOrDefaultAsync(u => u.UserName == name));

            if(userFromDb == null)
            {
                return null;
            }

            return userFromDb.ShoppingCart;
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
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            var cart = await this.GetShoppingCartByUserNameAsync(username);

            if(cart == null)
            {
                return false;
            }

            var cartItemsIds = cart.ShoppingCartItems.Select(x => x.ItemId);

            if (!cartItemsIds.Contains(itemId))
            {
                return false;               
            }
            else
            {
                var cartItem = cart.ShoppingCartItems
                    .SingleOrDefault(shc =>
                                    shc.ShoppingCartId == cart.Id &&
                                    shc.ItemId == itemId);

                cart.ShoppingCartItems.Remove(cartItem);
            }

            this.context.Update(cart);
            await this.context.SaveChangesAsync();

            return !(await this.GetShoppingCartByUserNameAsync(username))
                .ShoppingCartItems.Select(x => x.ItemId).Contains(itemId);
        }

        public async Task<bool> DeleteItemAsync(int itemId)
        {
            var item = await this.context.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            if(item == null)
            {
                return false;
            }

            this.context.ShoppingCartItems.RemoveRange(this.context.ShoppingCartItems.Where(x => x.ItemId == item.Id));
            this.context.ItemCategories.RemoveRange(this.context.ItemCategories.Where(x => x.ItemId == item.Id));
            this.context.Items.Remove(item);

            await this.context.SaveChangesAsync();

            return !this.context.Items.Contains(item);
        }

        public async Task<bool> IncreaseTimesItemIsBoughtAsync(int itemId)
        {
            var item = await this.context.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            if(item == null)
            {
                return false;
            }

            int initialValue = item.TimesBought;

            item.TimesBought++;

            this.context.Update(item);
            await this.context.SaveChangesAsync();

            return initialValue != item.TimesBought;
        }

        public async Task<bool> DecreaseTimesItemIsBoughtAsync(int itemId)
        {
            var item = await this.context.Items.SingleOrDefaultAsync(x => x.Id == itemId);

            if (item == null)
            {
                return false;
            }

            int initialValue = item.TimesBought;

            if (initialValue <= 1)
            {
                return true;
            }

            item.TimesBought--;

            this.context.Update(item);
            await this.context.SaveChangesAsync();

            return initialValue != item.TimesBought;
        }

        public async Task<bool> FinishOrderAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            var cart = await this.GetShoppingCartByUserNameAsync(name);

            if(cart == null)
            {
                return false;
            }

            if (cart.ShoppingCartItems.Count == 0)
            {
                return false;
            }

            var user = await this.usersService.GetUserByUsernameAsync(name);
            var order = new Order
            {
                UserId = user.Id
            };

            order.EndPrice = cart.EndPrice;
            order.DeliveryAddress = user.Address.Location;

            foreach (var item in cart.ShoppingCartItems.Select(x => x.Item))
            {
                order.OrderItems.Add(new OrderItem()
                {
                    ItemId = item.Id,
                    OrderId = order.Id
                });

                item.TimesEverBought += item.TimesBought;
                item.TimesBought = 0;
            }

            cart.ShoppingCartItems = new HashSet<ShoppingCartItem>();

            this.context.Orders.Add(order);
            this.context.Update(cart);
            await this.context.SaveChangesAsync();

            return order.UserId == user.Id;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUsernameAsync(string name)
        {
            var user = await this.usersService.GetUserByUsernameAsync(name);

            var orders = await this.context
                .Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Item)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Order)
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            return orders;
        }

        public async Task<bool> CompleteOrderAsync(int orderId)
        {
            var orderFromDb = await this.context.Orders.Include(ord => ord.OrderItems).SingleOrDefaultAsync(order => order.Id == orderId);
            
            if(orderFromDb == null)
            {
                return false;
            }

            this.context.OrderItems.RemoveRange(orderFromDb.OrderItems);
            this.context.Orders.Remove(orderFromDb);

            await this.context.SaveChangesAsync();

            return this.context.Orders.Contains(orderFromDb);
        }
    }
}
