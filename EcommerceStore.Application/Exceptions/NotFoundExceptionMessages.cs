namespace EcommerceStore.Application.Exceptions
{
    public static class NotFoundExceptionMessages
    {
        public static string BrandNotFound { get; private set; } = "Brand not found for provided brandId={0}";
        public static string ProductNotFound { get; private set; } = "Product not found for provided productId={0}";
        public static string RoleNotFound { get; private set; } = "Role not found for provided roleId={0}";
        public static string UserNotFound { get; private set; } = "User not found for provided userId={0}";
        public static string AddressNotFound { get; private set; } = "Address not found for provided addressId={0}";
        public static string OrderNotFound { get; private set; } = "Order not found for provided orderId={0}";
        public static string ProductCategoryNotFound { get; private set; } = "Product category not found for provided productCategoryId={0}";
        public static string SectionNotFound { get; private set; } = "Section not found for provided sectionId={0}";
        public static string ReviewNotFound { get; private set; } = "Review not found for provided reviewId={0}";
    }
}
