using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Services.Channel.Interfaces
{
    public interface ISMSMailService
    {
        Task SendEmailViaAppPass(string mailTo, string name, string subject, string message);
        Task SendEmailWithFromMail(string mailTo, string name, string subject, string message);
        Task SendEmailWithAttatchments(string to, string subject, /*IEnumerable<string> path,*/ string pdfUrl, string message, string name);
        Task<string> SendSMSAsync(string mobile, string message);
        string SendEMAIL(string mail, string subject, string message);
		string SendEmailWithFrom(string mailTo, string name, string subject, string message);
        string SendEMAILWithAttatchment(string toMail, string subject, string body, string path, bool isHtml);
        Task<string> SendWhatsappAsync(string mobile, string message);

    }
}
