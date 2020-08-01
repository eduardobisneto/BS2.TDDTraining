namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class ItemBuilder
    {
        private readonly WellKnownProduct wellKnownProduct;

        private ItemBuilder(WellKnownProduct wellKnownProduct)
        {
            this.wellKnownProduct = wellKnownProduct;
        }

        public Item Build()
        {
            return new Item(wellKnownProduct.ProductId, wellKnownProduct.Name, wellKnownProduct.Price);
        }

        public static ItemBuilder For<T>() where T: WellKnownProduct, new()
        {
            return new ItemBuilder(new T());
        }
    }
}