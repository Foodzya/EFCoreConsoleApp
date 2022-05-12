namespace EcommerceStore.Application.Exceptions
{
    public static class AlreadyExistExceptionMessages
    {
        public static string BrandAlreadyExists { get; private set; } = "Brand with id={0} already exists";
        public static string ProductAlreadyExists { get; private set; } = "Product with id={0} already exists";
        public static string RoleAlreadyExists { get; private set; } = "Role with id={0} already exists";
        public static string UserAlreadyExists { get; private set; } = "User with id={0} already exists";
        public static string ProductCategoryAlreadyExists { get; private set; } = "Product category with id={0} already exists";
        public static string SectionAlreadyExists { get; private set; } = "Section with id={0} already exists";
        public static string ReviewAlreadyExists { get; private set; } = "Review with id={0} already exists";
    }
}