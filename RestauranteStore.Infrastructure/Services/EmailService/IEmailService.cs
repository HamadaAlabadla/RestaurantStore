using RestauranteStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.EmailService
{
	public interface IEmailService
	{
		Task SendDistinctiveStyleEmail(string customerEmail, Order order);
	}
}
