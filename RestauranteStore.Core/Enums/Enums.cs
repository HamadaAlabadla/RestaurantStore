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
			Cancelled,          //ألغيت
			Completed,          //اكتملت
			Denied,             // رفض
			Expired,            //منتهي الصلاحية
			Failed,             //فشل
			Pending,            //قيد الإنتظار
			Processing,         //قيد المعالجة
			Refunded,           //مرجع
			Delivered,          //تم التوصيل
			Delivering,         //قيد التوصيل
			Draft,              //مسودة
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
