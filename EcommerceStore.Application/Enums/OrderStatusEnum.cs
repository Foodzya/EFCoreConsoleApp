namespace EcommerceStore.Application.Enums
{
    public static class OrderStatusEnum
    {
        public enum StatusesEnum
        {
            Created,
            InReview, 
            InDelivery,
            Completed,
            Failed,
            Canceled
        }
    }
}