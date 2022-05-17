namespace EcommerceStore.Application.Exceptions
{
    public static class ExceptionMessages
    {
        public static string BrandNotFound { get; private set; } = "Brand with the specified data was not found";
        public static string BrandAlreadyExists { get; private set; } = "Brand with such name already exists";
    }
}