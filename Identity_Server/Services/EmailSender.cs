namespace Identity_Server.Services
{
    public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Send Email to {email}: {subject} - {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
