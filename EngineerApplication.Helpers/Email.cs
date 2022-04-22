using System.Net.Mail;

namespace EngineerApplication.Helpers
{
  public class Email
  {
    private const string HtmlEmailHeader = "<html><head><title></title></head><body style='font-family:arial; font-size:14px;'>";
    private const string HtmlEmailFooter = "</body></html>";

    public List<string> To { get; set; }
    public List<string> CC { get; set; }
    public List<string> BCC { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
    public Email()
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
    {
      To = new List<string>();
      CC = new List<string>();
      BCC = new List<string>();
    }

    public void Send()
    {
      MailMessage message = new MailMessage();

      foreach (var x in To)
      {
        message.To.Add(x);
      }
      foreach (var x in CC)
      {
        message.CC.Add(x);
      }
      foreach (var x in BCC)
      {
        message.Bcc.Add(x);
      }

      message.Subject = Subject;
      message.Body = string.Concat(HtmlEmailHeader, Body, HtmlEmailFooter);
      message.BodyEncoding = System.Text.Encoding.UTF8;
      message.From = new MailAddress(From);
      message.SubjectEncoding = System.Text.Encoding.UTF8;
      message.IsBodyHtml = true;

      SmtpClient client = new SmtpClient("relay.mail.server");

      new Thread(() => { client.Send(message); }).Start();
    }
  }
}