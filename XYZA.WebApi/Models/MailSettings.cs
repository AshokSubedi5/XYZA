using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using XYZA.BLDA;
using XYZA.Models;

namespace XYZA.WebApi.Models
{
    public class MailSettings
    {
        public class MailResponse
        {
            public int successMailCount { get; set; }
            public IList<string> failedSendMailList { get; set; }
            public string status { get; set; }
            public IList<string> logs { get; set; }
            public int totalCustomer { get; set; }
        }
       
        IList<string> failedSendMailList;
        bool isCompleted;
        int i = 0;

        public SmtpClient MailSetup()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.googlemail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("noreplms@gmail.com", "Annapurna@#321");
            
            return client;

        }

        public void SendMailAsync(IEnumerable<CustomerModels> customers, EmailTemplate template)
        {
            i = 0;

            // Task.Factory.StartNew(() => SendMail(customers, template), TaskCreationOptions.LongRunning);

           


            Task.Factory.StartNew(() => backgroundtest(), TaskCreationOptions.LongRunning);

        }


        public void backgroundtest() {
            MailResponse mailResponse = new MailResponse();
            mailResponse.successMailCount = 0;
            mailResponse.status = "In Progress";
            mailResponse.logs = new List<string>();
            mailResponse.logs.Add("Started at : " + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));
            for (int i=0; i<10; i++)
            {
                mailResponse.logs.Add("Send at : " + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));
                SaveToJson(mailResponse);
                Thread.Sleep(5000);
            }
           
            mailResponse.logs.Add("end at : " + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));
            //frist save
            SaveToJson(mailResponse);
        }

        public void SendMail(IEnumerable<CustomerModels> customers, EmailTemplate template)
        {
            MailResponse mailResponse = new MailResponse();
            mailResponse.successMailCount = 0;
            mailResponse.totalCustomer = customers.Count();
            mailResponse.status = "In Progress";
            mailResponse.logs = new List<string>();
            mailResponse.logs.Add("Started at : " + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));

            //frist save
            SaveToJson(mailResponse);
            foreach (var customer in customers)
            {
                try
                {
                    SmtpClient smtpClient = MailSetup();
                    MailMessage mail = new MailMessage();

                    //Setting From , To and CC
                    //  mail.From = new MailAddress("noreply@yarshatech.com", "YarshaTech LMS");
                    mail.From = new MailAddress("noreply@gmail.com", "XYZA");
                    mail.To.Add(new MailAddress(customer.Email));
                    // mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));
                    mail.IsBodyHtml = true;

                    mail.Body = template.Message;
                    mail.Subject = template.Subject;
                    smtpClient.Send(mail);

                    //save at each mail send
                    mailResponse.successMailCount++;
                    mailResponse.logs.Add("Send mail to : " + customer.Email + " at :" + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));
                    SaveToJson(mailResponse);
                    smtpClient.Dispose();
                    Thread.Sleep(10000);
                   
                }
                catch (Exception ex)
                {
                    failedSendMailList.Add(customer.Email);
                    Console.Write(ex.Message);
                }
            }
            //save at last
            mailResponse.status = "Completed";
            mailResponse.logs.Add("Completed at :" + DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"));
            mailResponse.logs.Add("Total mail send " + mailResponse.successMailCount + " out of " + mailResponse.totalCustomer);
            SaveToJson(mailResponse);
        }

        public void SaveToJson(object data)
        {
            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Log/mail_log.json");
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);            
            // Write that JSON to txt file,  
            System.IO.File.WriteAllText(mappedPath, json);
        }
    }
}
