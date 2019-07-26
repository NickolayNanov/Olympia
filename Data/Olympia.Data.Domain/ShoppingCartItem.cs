namespace Olympia.Data.Domain
{
    public class ShoppingCartItem
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
