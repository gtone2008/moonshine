using System;
using System.Configuration;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace MoonshineSendMail
{
    class Program
    {
        private readonly static string MYSQLCONN = ConfigurationManager.AppSettings["MySQLconn"].ToString();
        private readonly static string MYSQLSTR = "SELECT basic_id,description, spec,LowLimit,UpperLimit,current_qty FROM basic_data where LowLimit > current_qty";

        private readonly static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private readonly static string smtpPort = ConfigurationManager.AppSettings["smtpPort"];
        private readonly static string address = ConfigurationManager.AppSettings["address"];
        private readonly static string displayname = ConfigurationManager.AppSettings["displayname"];

        private readonly static string To = ConfigurationManager.AppSettings["To"].ToString();
        private readonly static string Cc = ConfigurationManager.AppSettings["Cc"].ToString();

        private readonly static string subject = "Below listed materials need to buy now";

        private static readonly string EMAILBOTTOM = "<p><i><font color=\"red\">Please do not reply this mail.</font></i></p><p>Moonshine Management System</p>";

        private static string body = "This is an automated message generated from Moonshine Management System.<br/>This is to inform you that below listed materials need to buy now.</br></br><table align='center' border='1' cellpadding='0' cellspacing='0' style='border-collapse: collapse;width:100%; '><tr 'background-color:#4f81bd'><th>P/N</th><th>Description</th><th>Spec</th><th>LowLimit</th><th>UpperLimit</th><th>Current_qty</th></tr>";

        public static void ClientSendMail(string To, string Cc, string subject, string body)
        {
            if (string.IsNullOrEmpty(To)) return;

            SmtpClient client = new SmtpClient(smtpClient, int.Parse(smtpPort));

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(address, displayname);
            mail.To.Add(To);
            if (!string.IsNullOrEmpty(Cc))
            {
                mail.CC.Add(Cc);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            client.Send(mail);

            mail.Dispose();
        }

        static void Main(string[] args)
        {
            MySqlDataReader myReader = MySqlHelper.ExecuteReader(MYSQLCONN, MYSQLSTR);
            bool hasData = false; 
            while (myReader.Read())
            {
                hasData = true;
                body += "<tr><td>" + myReader[0].ToString() + "</td>";
                body += "<td>" + myReader[1].ToString() + "</td>";
                body += "<td>" + myReader[2].ToString() + "</td>";
                body += "<td>" + myReader[3].ToString() + "</td>";
                body += "<td>" + myReader[4].ToString() + "</td>";
                body += "<td style='color: red'>" + myReader[5].ToString() + "</td></tr>";
                //Console.WriteLine(myReader[0].ToString());
            }
            body += "</table>";
            body += EMAILBOTTOM;

            if(hasData)
            {
                ClientSendMail(To, Cc, subject, body);
            }

            //Console.WriteLine(Cc);
            //Console.WriteLine(To);
            //Console.Read();

        }
    }
}
