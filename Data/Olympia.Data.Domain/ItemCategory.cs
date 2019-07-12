namespace Olympia.Data.Domain
{
    public class ItemCategory
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int ChildCategoryId { get; set; }
        public ChildCategory ChildCategory { get; set; }
    }
}
