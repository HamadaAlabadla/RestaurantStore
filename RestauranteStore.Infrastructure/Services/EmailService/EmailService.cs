using RestauranteStore.EF.Models;
using System.Net;
using System.Net.Mail;

namespace RestaurantStore.Infrastructure.Services.EmailService
{
	public class EmailService : IEmailService
	{
		//public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		//{
		//    var smtpClient = new SmtpClient("smtp.gmail.com")
		//    {
		//        Port = 587,
		//        Credentials = new NetworkCredential("hamadh.h9719@gmail.com" , "mosojupnghtemmqm"),
		//        EnableSsl = true,
		//    };
		//    await smtpClient.SendMailAsync("hamadh.h9719@gmail.com", email, subject, htmlMessage);

		//}

		public async Task SendDistinctiveStyleEmail(string customerEmail, Order order)
		{
			try
			{
				// Create a new MailMessage
				MailMessage message = new MailMessage();
				message.From = new MailAddress("storerestaurantsha@gmail.com", "Restaurant Store");
				message.To.Add(customerEmail);
				message.Subject = "Receipt of an order - Restaurant Store";
				var trs = "";
				foreach (var item in order.OrderItems)
				{
					trs += $@"<tr>
                                <td style=""border-bottom: 1px solid #ddd; padding: 10px;"">{item.Product.Name}</td>
                                <td style=""border-bottom: 1px solid #ddd; padding: 10px;"">{item.QTY}</td>
                            </tr>";
				}
				// Create the HTML email body with inline CSS for styling
				string emailBody = $@"<!DOCTYPE html>
                                        <html>
                                        <head>
                                            <meta charset=""UTF-8"">
                                            <title>Customer Purchase Order</title>
                                        </head>
                                        <body style=""font-family: Arial, sans-serif; background-color: #f2f2f2; padding: 20px;"">
                                            <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 5px; padding: 20px;"">
                                                <h2 style=""color: #007bff;"">Customer Purchase Order #{order.Id}</h2>
                                                <p>Dear Seller,</p>
                                                <p>A customer has placed a purchase order for the following products:</p>

                                                <table style=""width: 100%; border-collapse: collapse; margin-bottom: 20px;"">
                                                    <thead>
                                                        <tr style=""background-color: #007bff; color: #ffffff;"">
                                                            <th style=""padding: 10px;"">Product Name</th>
                                                            <th style=""padding: 10px;"">Quantity</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        {trs}
                                                        <!-- Add more rows for each product in the purchase order -->
                                                    </tbody>
                                                </table>

                                                <p>Thank you for your attention to this order.</p>
                                                <p>Sincerely,</p>
                                                <p>Restaurant Store</p>
                                            </div>
                                        </body>
                                        </html>
                                        ";

				message.IsBodyHtml = true;
				message.Body = emailBody;

				// Set up your SMTP server credentials
				SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential("storerestaurantsha@gmail.com", "qtdezpumicvqtzmg");
				smtpClient.EnableSsl = true;

				// Send the email
				await smtpClient.SendMailAsync(message);

				Console.WriteLine("Distinctive style email sent successfully!");
			}
			catch (Exception ex)
			{
				// Handle any errors that may occur during the email sending process
				Console.WriteLine("Error sending distinctive style email: " + ex.Message);
			}
		}
	}
}
