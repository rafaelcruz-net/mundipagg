using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using System.Net;
using System.Configuration;
using System.IO;

namespace MundiPagg.Infra.Utils
{
    public class EmailUtils
    {
        private static readonly string username;
        private static readonly string password;
        private static readonly string from;
        private static readonly NetworkCredential networkCredential;

        public struct EmaiSubject
        {
            public static String NEW_TRANSACTION_UPDATE = "MundiPagg - Atualização de Pedido";
        }

        static EmailUtils()
        {
            username = ConfigurationManager.AppSettings["SendGridUserName"].ToString();
            password = ConfigurationManager.AppSettings["SendGridPassword"].ToString();
            from = ConfigurationManager.AppSettings["SendGridFrom"].ToString();

            networkCredential = new NetworkCredential(username, password);
 
        }

        public static async void SendEmail(string to, string body, string subject)
        {
            SendGridMessage message = new SendGridMessage();

            message.AddTo(to);
            message.Html = body;
            message.Subject = subject;
            message.From = new MailAddress(from, "Desafio - MundiPagg");

            var transportWeb = new Web(networkCredential);
            
            await transportWeb.DeliverAsync(message);
        }

        public static String TransformTemplate(string pathTemplate, dynamic model)
        {
            string html = System.IO.File.ReadAllText(pathTemplate);
            string emailHtml = "";

            if (model != null)
                emailHtml = RazorEngine.Razor.Parse(html, model);   
            else
                emailHtml = RazorEngine.Razor.Parse(html);   

            return emailHtml;
        }

        public static String TransformTemplate(Stream streamTemplate, dynamic model)
        {

            StreamReader reader = new StreamReader(streamTemplate);

            try
            {
                var html = reader.ReadToEnd();

                string emailHtml = "";

                if (model != null)
                    emailHtml = RazorEngine.Razor.Parse(html, model);
                else
                    emailHtml = RazorEngine.Razor.Parse(html);

                return emailHtml;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }


        




    }
}
