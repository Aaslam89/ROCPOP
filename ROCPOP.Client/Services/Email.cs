using ROCPOP.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using ROCPOP.Client.Models.ViewModels;

namespace ROCPOP.Client.Services
{
    public class Email
    {

        //        //Smtp Server 
        //        //string SmtpServer = "smtp.gmail.com";
        //        //Smtp Port Number 
        //        //int SmtpPortNumber = 587;

        //        //Smtp Server
        //        string SmtpServer = "smtp.mail.yahoo.com";
        //        //Smtp Port Number
        //        int SmtpPortNumber = 465;

        public void Send(ContactForm model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(model.Name, model.Email));
            message.To.Add(new MailboxAddress("PopCorn", "AmirAslam003@yahoo.com"));
            message.Subject = "ROC CITY POPCORN Contact Form";
            message.Body = new TextPart("plain")
            {
                Text = String.Format("From: {0} \n Email: {1} \n Subject: {2} \n Message: {3}", model.Name, model.Email, model.Subject, model.Message)
            };
            
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                //client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("AmirAslam89", "fONZI008");
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
