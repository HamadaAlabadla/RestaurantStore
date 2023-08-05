namespace RestauranteStore.Core.Enums
{
    public static class Enums
    {
        public enum UserType
        {

            supplier,
            restaurant,
            admin
        }
        public enum StatusProduct
        {
            Published,
            Scheduled,
            Inactive,
            Draft
        }

        public enum StatusOrder
        {
            Draft,              //مسودة
            Pending,            //قيد الإنتظار
            Processing,         //قيد المعالجة
            Delivering,         //قيد التوصيل
            Delivered,          //تم التوصيل
            Completed,          //اكتملت
            Denied,             // رفض
            Cancelled,          //ألغيت
            Failed,             //فشل
            Refunded,           //مرجع
            Expired            //منتهي الصلاحية
        }
        public enum PaymentMethod
        {
            CashOnDelivery,
            CreditCardVisa,
            CreditCardMastercard,
            Paypal

        }
    }
}
