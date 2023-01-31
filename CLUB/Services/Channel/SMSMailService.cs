using CLUB.Services.Channel.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CLUB.Services.Channel
{
    public class SMSMailService : ISMSMailService
    {
        private readonly SmtpClient smtpClient;
		private readonly IConfiguration _configuration;
		public SMSMailService(IConfiguration _configuration)
        {
			this._configuration = _configuration;
			smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("opustech2k23@gmail.com", "OpusTech111"),
                EnableSsl = true
            };
        }

        public async Task SendEmailViaAppPass(string mailTo, string name, string subject, string message)
        {
            try
            {
                string userName = _configuration["Email:Email"];
                string password = _configuration["Email:Password"];
                string host = _configuration["Email:Host"];
                int port = int.Parse(_configuration["Email:Port"]);
                string mailFrom = _configuration["Email:Email"];
                string appPassword = _configuration["Email:AppPassword"];
                var emailMessage = new MailMessage();
                emailMessage.To.Add(new MailAddress(mailTo));
                emailMessage.From = new MailAddress(mailFrom, name);
                emailMessage.Subject = subject;
                emailMessage.Body = message;
                emailMessage.IsBodyHtml = true;
                var credential = new NetworkCredential
                {
                    UserName = userName,
                    Password = password
                };

                SmtpClient SmtpServer = new SmtpClient(host, 587);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Timeout = 5000;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(mailFrom, appPassword);
                SmtpServer.Send(emailMessage);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        public async Task SendEmailWithFromMail(string mailTo, string name, string subject, string message)
        {
            bool active = Convert.ToBoolean(_configuration["Email:Enabled"]);
            if (active)
            {

                string userName = _configuration["Email:Email"];
                string password = _configuration["Email:Password"];
                string host = _configuration["Email:Host"];
                int port = int.Parse(_configuration["Email:Port"]);
                string mailFrom = _configuration["Email:Email"];
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = userName,
                        Password = password
                    };

                    client.Credentials = credential;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(mailTo));
                        emailMessage.From = new MailAddress(mailFrom, name);
                        emailMessage.Subject = subject;
                        emailMessage.Body = message;
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
            }



            await Task.CompletedTask;
        }


        public async Task SendEmailWithAttatchments(string to, string subject/*, string path*/, string pdfUrl, string message, string name)
        {
            string userName = _configuration["Email:Email"];
            string password = _configuration["Email:Password"];
            string host = _configuration["Email:Host"];
            int port = int.Parse(_configuration["Email:Port"]);
            string mailFrom = _configuration["Email:Email"];
            string appPassword = _configuration["Email:AppPassword"];
            var credential = new NetworkCredential
            {
                UserName = userName,
                Password = password
            };
            List<Attachment> attach = new List<Attachment>();

            //foreach (var item in path)
            //{
                //attach.Add(new Attachment(path));
            //}
            attach.Add(new Attachment(pdfUrl));
            MailMessage msg = new MailMessage();



            msg.From = new MailAddress(userName, name);

            msg.To.Add(to);

            msg.Subject = subject;

            //  msg.Body = body;
            msg.Body = message;
            msg.IsBodyHtml = true;

            foreach (var item in attach)
            {
                msg.Attachments.Add(item);
            }



            SmtpClient SmtpServer = new SmtpClient(host, 587);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Timeout = 500000;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(mailFrom, appPassword);


            try
            {
                SmtpServer.Send(msg);

            }
            catch (System.Exception ex)
            {

                throw;
            }

            await Task.CompletedTask;
        }

        public string SendEMAIL(string mail, string subject, string message)
        {
            try
            {
                smtpClient.Send("opustech2k23@gmail.com", mail, subject, message);
                return "Sent";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
		public string SendEMAIL(string password, string toMail, string ccMail, string bccMail, string subject, string body, string path, bool isHtml)
        {
            try
            {
				string fromMail = "opustech2k23@gmail.com";
				MailMessage msg = new MailMessage();

				msg.From = new MailAddress(fromMail);
				msg.To.Add(toMail);
				if (ccMail.Length > 0)
				{
					msg.CC.Add(ccMail);
				}
				if (bccMail.Length > 0)
				{
					msg.Bcc.Add(bccMail);
				}
				msg.BodyEncoding = Encoding.UTF8; ;
				msg.SubjectEncoding = Encoding.UTF8;
				msg.Subject = subject;
				msg.Body = body;
				if (isHtml == true)
				{
					msg.IsBodyHtml = true;
				}
				if (Directory.Exists(path))
				{
					msg.Attachments.Add(new Attachment(path));
				}


				SmtpClient smtp = new SmtpClient();
				smtp.Host = "smtp.gmail.com";
				NetworkCredential credential = new NetworkCredential();
				credential.UserName = fromMail;
				credential.Password = password;
				smtp.Credentials = credential;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Send(msg);
				return "Sent";
			}
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string SendEMAILWithAttatchment(string toMail, string subject, string body, string path, bool isHtml)
        {
            try
            {
                string fromMail = "opustestmail@gmail.com";
                string password = "Opus1234";


                


                Attachment attach = new Attachment(path);
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(fromMail);
                msg.To.Add(toMail);
                msg.BodyEncoding = Encoding.UTF8; ;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Subject = subject;
                msg.Body = body;
                msg.Attachments.Add(attach);

                if (isHtml == true)
                {
                    msg.IsBodyHtml = true;
                }


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                NetworkCredential credential = new NetworkCredential();
                credential.UserName = fromMail;
                credential.Password = password;
                smtp.Credentials = credential;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(msg);
                return "Sent";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> SendSMSAsync(string mobile, string message)
        {
           // return "Skip";
            try
            {
                if (mobile.Length < 11)
                {
                    int numOfZeros = 11 - mobile.Length;
                    mobile = new string('0', numOfZeros) + mobile;
                }
                string url = String.Format("http://api.boom-cast.com/boomcast/WebFramework/boomCastWebService/externalApiSendTextMessage.php?masking=NOMASK&userName=OpusTech&password=c3eb7e87b84e252777057a07d984e98e&MsgType=TEXT&receiver={0}&message={1}", mobile, message);
                //string url = String.Format("http://apibd.rmlconnect.net/bulksms/personalizedbulksms?username=OpusBDENT&password=hxIi6jyZ&source=PHQ%20BD&destination={0}&message={1}", mobile, message);
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                dynamic data = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                if (data[0].success == 1) return "success";
                return "fail";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public async Task<string> SendWhatsappAsync(string mobile, string message)
        {
           // return "Skip";
            try
            {
                if (mobile.Length < 11)
                {
                    int numOfZeros = 11 - mobile.Length;
                    mobile = new string('0', numOfZeros) + mobile;
                }
                const string accountSid = "AC012fd256a1aa1e71c85674a4be38c678";
                const string authToken = "6b90feb8abfa6c40a467597407b85650";
                //+17248358523
                TwilioClient.Init(accountSid, authToken);

                var number = "+88" + mobile;

                var send = MessageResource.Create(
                    to: new Twilio.Types.PhoneNumber(number),
                    from: new Twilio.Types.PhoneNumber("+17248358523"),
                    body: message);
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

		public string SendEmailWithFrom(string mailTo, string name, string subject, string message)
		{
			string userName = _configuration["Email:Email"];
			string password = _configuration["Email:Password"];
			string host = _configuration["Email:Host"];
			int port = int.Parse(_configuration["Email:Port"]);
			string mailFrom = _configuration["Email:Email"];
			using (var client = new SmtpClient())
			{
				var credential = new NetworkCredential
				{
					UserName = userName,
					Password = password
				};

				client.Credentials = credential;
				client.Host = host;
				client.Port = port;
				client.EnableSsl = true;

				using (var emailMessage = new MailMessage())
				{
					emailMessage.To.Add(new MailAddress(mailTo));
					emailMessage.CC.Add(new MailAddress("engineersclubuttara@gmail.com"));
					emailMessage.From = new MailAddress(mailFrom, name);
					emailMessage.Subject = subject;
					emailMessage.Body = message;
					emailMessage.IsBodyHtml = true;
					client.Send(emailMessage);
				}
			}
			//await Task.CompletedTask;
			return "Sent";
		}
	}
}
