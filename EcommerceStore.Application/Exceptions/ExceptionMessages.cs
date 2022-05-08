namespace EcommerceStore.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public static string BrandNotFound { get; private set; } = "Brand not found for provided brandId={0}";
        public static string BrandAlreadyExists { get; private set; } = "Brand with id={0} already exists";
        public static string ProductNotFound { get; private set; } = "Product not found for provided productId={0}";
        public static string ProductAlreadyExists { get; private set; } = "Product with id={0} already exists";
    }
}