namespace TDDTraining.ShoppingCart.Domain.Tests.Shared
{
    public class ProductInfoBuilder
    {
        private readonly WellKnowProduct wellKnowProduct;

        public ProductInfoBuilder(WellKnowProduct wellKnowProduct)
        {
            this.wellKnowProduct = wellKnowProduct;
        }

        public Apis.ProductInfo Build()
        {
            return new Apis.ProductInfo(wellKnowProduct.ProductId, wellKnowProduct.Name, wellKnowProduct.Price);
        }

        public static ProductInfoBuilder For<T>() where T : WellKnowProduct, new()
        {
            return new ProductInfoBuilder(new T());
        }
    }
}