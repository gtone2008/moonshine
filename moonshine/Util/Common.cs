using System;
using System.Configuration;
using System.DirectoryServices;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls;

namespace moonshine.Util
{
    public class Common
    {
        private readonly static string smtpClient = ConfigurationManager.AppSettings["smtpClient"];
        private readonly static string smtpPort = ConfigurationManager.AppSettings["smtpPort"];
        private readonly static string address = ConfigurationManager.AppSettings["address"];
        private readonly static string displayname = ConfigurationManager.AppSettings["displayname"];

        private readonly static string LDAP_PATH = ConfigurationManager.AppSettings["LDAP"];

        /// <summary>
        /// Get user id from HttpContext
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentNTID()
        {
            string identityName = HttpContext.Current.User.Identity.Name;
            int splitIndex = identityName.IndexOf('\\');
            return splitIndex > -1 ? identityName.Substring(splitIndex + 1) : identityName;
        }

        /// <summary>
        /// Get User information from AD
        /// </summary>
        /// <param name="ntid">AD用户名</param>
        /// <returns>用户实例</returns>
        public static moonshine.Model.AdUser GetADUserEntity(string ntid)
        {
            if (string.IsNullOrEmpty(ntid))
            {
                throw new Exception("Searched user id cannot be null.");
            }

            DirectoryEntry entry = new DirectoryEntry(LDAP_PATH);
            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + ntid + "))";

            SearchResult searchResult = searcher.FindOne();
            if (searchResult != null)
            {
                Model.AdUser user = new Model.AdUser();
                user.UID = ntid;
                user.Department = GetADProperty(searchResult, "department");
                user.DisplayName = GetADProperty(searchResult, "displayName");
                user.Email = GetADProperty(searchResult, "mail");

                return user;
            }
            return null;
        }

        /// Get User information from AD
        /// </summary>
        /// <param name="ntid">AD用户名</param>
        /// <returns>用户实例</returns>
        public static string GetNamerByNTID(string ntid, string propertyName)
        {
            if (string.IsNullOrEmpty(ntid))
            {
                throw new Exception("Searched user id cannot be null.");
            }

            DirectoryEntry entry = new DirectoryEntry(LDAP_PATH);
            DirectorySearcher searcher = new DirectorySearcher(entry);
            searcher.Filter = "(&(objectClass=user)(sAMAccountName=" + ntid + "))";

            SearchResult searchResult = searcher.FindOne();
            if (searchResult != null)
            {
                return GetADProperty(searchResult, propertyName);
            }
            return null;
        }

        /// <summary>
        /// 根据属性名，在搜索结果中查找属性值
        /// </summary>
        /// <param name="searchResult">DirectorySearcher返回的搜索结果</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        private static string GetADProperty(SearchResult searchResult, string propertyName)
        {
            if (searchResult.Properties.Contains(propertyName))
            {
                return searchResult.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetCurrentUrl()
        {
            string absoluteUri = HttpContext.Current.Request.Url.AbsoluteUri;
            return absoluteUri.Substring(0, absoluteUri.LastIndexOf("/") + 1);
        }

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

        /// <summary>
        /// 限制图片大小
        /// </summary>
        public static bool IsFileSize(FileUpload fupload1)
        {
            //从web.config读取判断文件大小的限制
            // double iFileSizeLimit = Convert.ToInt32(ConfigurationManager.AppSettings["FileSizeLimit"]);
            double iFileSizeLimit = 1024;
            //判断文件是否超出了限制
            if (iFileSizeLimit < fupload1.PostedFile.ContentLength)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 验证是否指定的图片格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isImage(FileUpload fupload)
        {
            string fileExtension = Path.GetExtension(fupload.FileName).ToLower();
            bool isimage = false;
            string thestr = fileExtension.ToLower();
            string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png" };
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (thestr == allowExtension[i])
                {
                    isimage = true;
                    break;
                }
            }
            return isimage;
        }

        //上传图片
        public static bool UploadFile(FileUpload fupload, string folderName)
        {
            //string folderPath = Path.Combine(fupload.Page.Server.MapPath("uploads"), folderName);
            string folderPath = fupload.Page.Server.MapPath("uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = fupload.FileName;
            string saveFile = Path.Combine(folderPath, fileName);
            fupload.SaveAs(saveFile);
            return true;
        }
    }
}