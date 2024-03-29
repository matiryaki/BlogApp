﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Common.Helpers
{
   public class MailHelper
    {
         public static bool SendMail(string body,string to,string subject,bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }
         public static bool SendMail(string body,List<string> to,string subject,bool isHtml = true)
        {
            bool Result = false;
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress(ConfigHelper.GeT<string>("MailUser"));
                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                using(var smtp=new SmtpClient(ConfigHelper.GeT<string>("MailHost"), ConfigHelper.GeT<int>("MailPort")))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(ConfigHelper.GeT<string>("MailUser"),ConfigHelper.GeT<string>("MailPass"));
                    smtp.Send(message);
                    Result = true;
                }
            }
            catch (Exception)
            {

               
            }
            return Result;
        }

    }
}
