namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class ProductInfoBuilder
    {
        private readonly WellKnownProduct wellKnowProduct;

        public ProductInfoBuilder(WellKnownProduct wellKnowProduct)
        {
            this.wellKnowProduct = wellKnowProduct;
        }

        public static ProductInfoBuilder For<T>() where T : WellKnownProduct, new()
        {
            var wellKnowProduct = new T();
            return new ProductInfoBuilder(wellKnowProduct);
        }

        public ProductInfo Build()
        {
            return new ProductInfo(wellKnowProduct.ProductId, wellKnowProduct.Name, wellKnowProduct.Price);
        }
    }
}