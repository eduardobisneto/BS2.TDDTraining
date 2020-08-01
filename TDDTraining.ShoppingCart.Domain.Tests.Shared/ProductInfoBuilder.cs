namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class ProductInfoBuilder
    {
        private readonly WellKnownProduct wellKnownProduct;

        public ProductInfoBuilder(WellKnownProduct wellKnownProduct)
        {
            this.wellKnownProduct = wellKnownProduct;
        }

        public Apis.ProductInfo Build()
        {
            return new Apis.ProductInfo(wellKnownProduct.ProductId, wellKnownProduct.Name, wellKnownProduct.Price);
        }

        public static ProductInfoBuilder For<T>() where T : WellKnownProduct, new()
        {
            return new ProductInfoBuilder(new T());
        }
    }
}