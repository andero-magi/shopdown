namespace Shop.ApplicationServices.SpaceshipServices;

using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

public class EmailService : IEmailService
{

    private readonly string _fromEmail;
    private readonly string _password;

    public EmailService(string fromEmail, string pass)
    {
        _fromEmail = fromEmail;
        _password = pass;
    }

    public async Task SendEmail(EmailDto dto)
    {
        var from = new MailAddress(_fromEmail);
        var to = new MailAddress(dto.Recipient);

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_fromEmail, _password)
        };

        using (var message = new MailMessage(from, to))
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    var stream = file.OpenReadStream();
                    string mimeType = MimeKit.MimeTypes.GetMimeType(file.FileName);
                    ContentType type = new(mimeType);
                    Attachment attch = new(stream, type);

                    var disp = attch.ContentDisposition;
                    disp.ReadDate = DateTime.Now;
                    disp.CreationDate = DateTime.Now;
                    disp.ModificationDate = DateTime.Now;
                    disp.FileName = file.FileName;
                    disp.Size = file.Length;
                    disp.DispositionType = DispositionTypeNames.Attachment;

                    message.Attachments.Add(attch);
                }
            }

            message.Subject = dto.Subject;
            message.Body = dto.Body;
            message.Sender = from;
            smtp.Send(message);
        }
    }
}
