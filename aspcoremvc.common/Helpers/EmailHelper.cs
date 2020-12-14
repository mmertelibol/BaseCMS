using Common.Dto;
using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace Common.Helpers
{
    public static class EmailHelper
    {
        public static async Task<SendEmailResponse> Send(string to, string cc, string subject, string body, EmailSettingDto setting, string attachmentPath = null, string bcc = null)
        {
            #region SMTP EMail
            try
            {
                var conf = HttpHelper.GetConfig<string>("AppConfig:ActiveConnection");
                var fromName = HttpHelper.GetConfig<string>("EmailTemplateSettings:FromName") ?? "";

                if (string.IsNullOrEmpty(to)) return new SendEmailResponse() { Status = false, Message = "Email recipient not found." };
                if (setting == null) return new SendEmailResponse() { Status = false, Message = "Email settings not found." };

                using (MailMessage email = new MailMessage())
                {

                    email.From = new MailAddress(setting.EmailAddress, fromName);
                    email.SubjectEncoding = Encoding.UTF8;
                    email.BodyEncoding = Encoding.UTF8;
                    email.IsBodyHtml = true;
                    email.Priority = MailPriority.Normal;
                    email.Subject = (conf.ToLower() == "test" ? "[Test] " + subject : subject);

                    if (bcc != null)
                    {
                        if (bcc.Contains(";"))
                        {
                            foreach (string bccAddress in bcc.Split(';'))
                                if (bccAddress != "")
                                    email.Bcc.Add(bccAddress);
                        }
                        else
                        {
                            email.Bcc.Add(bcc);
                        }

                    }

                    foreach (string mailAddress in to.Split(';'))
                        if (mailAddress != "")
                            email.To.Add(mailAddress);

                    if (!string.IsNullOrWhiteSpace(cc))
                        foreach (string mailAddress in cc.Split(';'))
                            if (mailAddress != "")
                                email.CC.Add(mailAddress);

                    if (attachmentPath != null)
                    {
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
                        email.Attachments.Add(attachment);
                    }

                    email.Body = body;


                    using (var smtp = new SmtpClient
                    {
                        Host = setting.Host,
                        Port = setting.Port,
                        EnableSsl = setting.EnableSsl,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = setting.UseDefaultCredentials,
                        Credentials = new NetworkCredential(setting.EmailAddress, setting.Password)
                    })


                        await smtp.SendMailAsync(email);

                    return new SendEmailResponse() { Status = true, Message = "success" };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new SendEmailResponse() { Status = false, Message = "error! exceptionMessage: " + e.Message };
            }
            #endregion
        }

        public static async Task<SendEmailResponse> SendGridEmailSender(SendGridEmail mail)
        {
            #region SendGrid Email
            var conf = HttpHelper.GetConfig<string>("AppConfig:ActiveConnection");
            var fromName = HttpHelper.GetConfig<string>("EmailTemplateSettings:FromName") ?? "OtoFlex";
            var fromEmail = HttpHelper.GetConfig<string>("EmailSettings:EmailAddress") ?? "noreplay@otoflex.com";

            if (string.IsNullOrEmpty(mail.To)) return new SendEmailResponse() { Status = false, Message = "Email recipient not found." };

            //Test ortamında ise tüm mailler test mailine gönderiliyor
            if (conf.ToLower() == "test")
            {
                mail.To = "test@otoflex.com";
                mail.Cc = null;
            }

            var msg = new SendGridMessage();
            var apiKey = HttpHelper.GetConfig<string>("AppConfig:SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var subject = string.Empty;

            if (!string.IsNullOrWhiteSpace(mail.Subject))
            {
                subject = (conf.ToLower() == "test" ? "[Test] " + mail.Subject : mail.Subject);

            }

            //Dynamic template mail ise
            if (!string.IsNullOrWhiteSpace(mail.TemplateId))
            {
                var dynamicTemplateData = new Dictionary<string, string>();

                foreach (var item in mail.Parameters)
                {
                    dynamicTemplateData.Add(item.Parameter.Replace("~#~", ""), item.Value);
                }

                if ((conf.ToLower() == "test"))
                {
                    dynamicTemplateData.Add("TestServer", "[Test]");
                }

                if (!string.IsNullOrWhiteSpace(mail.Subject))
                {
                    dynamicTemplateData.Add("Subject", mail.Subject);
                }

                //Dynamic Template mailler birden fazla gönderici içeriyorsa
                if (mail.To.Contains(";"))
                {
                    var tos = new List<EmailAddress>();

                    foreach (string mailAddress in mail.To.Split(';'))
                    {
                        if (mailAddress != "")
                            tos.Add(new EmailAddress(mailAddress));
                    }
                    msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(new EmailAddress(fromEmail, fromName), tos, mail.TemplateId, dynamicTemplateData);
                }
                else
                {
                    msg = MailHelper.CreateSingleTemplateEmail(new EmailAddress(fromEmail, fromName), new EmailAddress(mail.To), mail.TemplateId, dynamicTemplateData);
                }

            }
            else //Dynamic değil ise
            {
                var plainText = string.Empty;
                if (!string.IsNullOrWhiteSpace(mail.Body))
                {
                    plainText = HtmlToPlainText(mail.Body);
                }

                //Birden fazla gönderi içeriyorsa
                if (mail.To.Contains(";"))
                {
                    var tos = new List<EmailAddress>();

                    foreach (string mailAddress in mail.To.Split(';'))
                    {
                        if (mailAddress != "")
                            tos.Add(new EmailAddress(mailAddress));
                    }
                    msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(fromEmail, fromName), tos, subject, plainText, mail.Body);
                }
                else
                {
                    msg = MailHelper.CreateSingleEmail(new EmailAddress(fromEmail, fromName), new EmailAddress(mail.To), subject, plainText, mail.Body);
                }
                //
            }


            //Add Bcc
            if (mail.Bcc != null)
            {
                if (mail.Bcc.Contains(";"))
                {
                    foreach (string bccAddress in mail.Bcc.Split(';'))
                    {
                        if (bccAddress != "" && bccAddress != mail.To)
                            msg.AddBcc(new EmailAddress(bccAddress));
                    }
                }
                else
                {
                    msg.AddBcc(new EmailAddress(mail.Bcc));
                }
            }
            //  

            //Add Cc
            if (mail.Cc != null)
            {
                if (mail.Cc.Contains(";"))
                {
                    foreach (string ccAddress in mail.Cc.Split(';'))
                    {
                        if (ccAddress != "" && ccAddress != mail.To)
                            msg.AddCc(new EmailAddress(ccAddress));
                    }
                }
                else
                {
                    msg.AddCc(new EmailAddress(mail.Cc));
                }
            }
            //    

            //Add Attachment
            if (mail.AttachmentPath != null)
            {
                Byte[] bytes = File.ReadAllBytes(mail.AttachmentPath);
                String file = Convert.ToBase64String(bytes);
                string fileName = System.IO.Path.GetFileName(mail.AttachmentPath);
                msg.AddAttachment(fileName, file);
            }

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return new SendEmailResponse() { Status = true, Message = "success" };
            }
            else
            {
                return new SendEmailResponse() { Status = false, Message = "error" };
            }
            #endregion
        }

        private static string HtmlToPlainText(string html)
        {
            string buf;
            string block = "address|article|aside|blockquote|canvas|dd|div|dl|dt|" +
              "fieldset|figcaption|figure|footer|form|h\\d|header|hr|li|main|nav|" +
              "noscript|ol|output|p|pre|section|table|tfoot|ul|video";

            string patNestedBlock = $"(\\s*?</?({block})[^>]*?>)+\\s*";
            buf = Regex.Replace(html, patNestedBlock, "\n", RegexOptions.IgnoreCase);

            // Replace br tag to newline.
            buf = Regex.Replace(buf, @"<(br)[^>]*>", "\n", RegexOptions.IgnoreCase);

            // (Optional) remove styles and scripts.
            buf = Regex.Replace(buf, @"<(script|style)[^>]*?>.*?</\1>", "", RegexOptions.Singleline);

            // Remove all tags.
            buf = Regex.Replace(buf, @"<[^>]*(>|$)", "", RegexOptions.Multiline);

            // Replace HTML entities.
            buf = WebUtility.HtmlDecode(buf);
            return buf;
        }
    }

    public class SendEmailResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class EmailTemplateParameter
    {
        public string Parameter { get; set; }
        public string Value { get; set; }
    }

    public class SendGridEmail
    {
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public string AttachmentPath { get; set; }
        public string Subject { get; set; }
        public string TemplateId { get; set; }
        public List<EmailTemplateParameter> Parameters { get; set; }
    }
}
