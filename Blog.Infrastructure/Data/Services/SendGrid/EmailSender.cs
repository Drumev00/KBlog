using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Blog.Infrastructure.Data.Services.SendGrid
{
	public class EmailSender
	{
		public EmailSender(IOptions<AuthMessageSenderOptions> options)
		{
			Options = options.Value;
		}

		public AuthMessageSenderOptions Options { get; set; }

		public async Task Execute(string email, string html)
		{
			var apiKey = Environment.GetEnvironmentVariable("SendGridKey", EnvironmentVariableTarget.User);
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress("kblogreply4@gmail.com", "KBlog Account Confirmation");
			var subject = "Account Confirmation";
			var to = new EmailAddress(email);
			var plainTextContent = string.Empty;
			var htmlContent = html;
			var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
			await client.SendEmailAsync(msg);
		}
	}
}
