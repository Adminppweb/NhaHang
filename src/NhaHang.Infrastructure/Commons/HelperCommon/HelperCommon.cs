using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace NhaHang.Infrastructure.Commons
{
    public class HelperCommon
    {
        //public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        //{
        //    if (String.IsNullOrEmpty(passwordFormat))
        //        passwordFormat = "SHA1";
        //    string saltAndPassword = String.Concat(password, saltkey);
        //    string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
        //    return hashedPassword;
        //}

        public static string CreatePassEncryptText(string plainText, string encryptionPrivateKey = "MAKV2SPBNI9921221")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        public static string CreateEncryptText(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            string encryptionPrivateKey = "MAKV2SPBNI9921221";

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            string result = Convert.ToBase64String(encryptedBinary);
            result = Regex.Replace(result, "[^0-9a-zA-Z]+", "");
            return result;
        }

        public static string CreateSaltKey(int size = 5)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        #region thuat toan 
        public static System.Security.Cryptography.RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new System.Security.Cryptography.RijndaelManaged
            {
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        public static byte[] Encrypt(byte[] plainBytes, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        public static String EncryptAbc283(String plainText, String key = "aecrtgf12e456yhn")
        {
            var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }

        public static String DecryptAbc283(String encryptedText, String key = "aecrtgf12e456yhn")
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return System.Text.Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
        }
        #endregion

        #region thuat toan AES256 (256 bits = 32 bytes)
        public static string EncryptCodeAES256(string code)
        {
            string encryptkey = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            string iVector = "hochiminh1234";
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException("Code");
            }
            CryptLib _crypt = new CryptLib();
            string key = CryptLib.getHashSha256(encryptkey, 32);
            return _crypt.encrypt(code, key, iVector);
        }

        public static string DecryptCodeAES256(string code)
        {
            string encryptkey = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            string iVector = "hochiminh1234";
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException("Code");
            }
            CryptLib _crypt = new CryptLib();
            string key = CryptLib.getHashSha256(encryptkey, 32);
            return _crypt.decrypt(code, key, iVector);
        }
        #endregion

        public static string GetUserIP()
        {
            string strIP = String.Empty;
            //HttpRequest httpReq = HttpContext.Current.Request;

            ////test for non-standard proxy server designations of client's IP
            //if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            //{
            //    strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            //}
            //else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            //{
            //    strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            //}
            ////test for host address reported by the server
            //else if((httpReq.UserHostAddress.Length != 0) && ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost")))
            //{
            //    strIP = httpReq.UserHostAddress;
            //}
            ////finally, if all else fails, get the IP from a web scrape of another server
            //else
            //{
            //    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            //    using (WebResponse response = request.GetResponse())
            //    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            //    {
            //        strIP = sr.ReadToEnd();
            //    }
            //    //scrape ip from the html
            //    int i1 = strIP.IndexOf("Address: ") + 9;
            //    int i2 = strIP.LastIndexOf("</body>");
            //    strIP = strIP.Substring(i1, i2 - i1);
            //}
            return strIP;
        }

        public static string GetFormatAmount(decimal? amount, int commaNumber = 0, string culture = "vn-VN")
        {
            string price = string.Empty;
            string comma = "";
            for (int i = 0; i < commaNumber; i++)
            {
                comma += "0";
            }
            if (amount.HasValue)
                price = amount.Value.ToString("#,##0." + comma, new CultureInfo(culture));
            return price;
        }

        public static string GetUserName(int id)
        {
            return (id + 1).ToString("U000000");
        }

        public static int SendEMailSync(string to, string title, string body, string host, string from, string formTitle, string pass)
        {
            try
            {
                string mailfrom = from;
                string passmail = pass;
                MailAddress ma = new MailAddress(mailfrom, formTitle);
                MailAddress maTo = new MailAddress(to);
                using (MailMessage mm = new MailMessage(ma, maTo))
                {
                    mm.Subject = title;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = host;
                    NetworkCredential NetworkCred = new NetworkCredential(mailfrom, passmail);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 80;
                    smtp.Send(mm);
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0; throw;
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string MailFormat(string body = "", string titte = "Change your password", string hostName = "", int status = 0)
        {
            StringBuilder str = new StringBuilder();
            switch (status)
            {
                case 1:
                case 2:
                case 3:
                default:
                    str.Append(string.Format("<h1 style='color: #8A25B1; text-align: center;'>Welcome to you !</h1>"));
                    str.Append(string.Format("<h2 style='color: #2e6c80; text-align: center;'>{0}</h2>", titte));
                    break;
            }
            return str.ToString();
        }

        public static string GetError(int code = 0)
        {
            string meg = string.Empty;
            switch (code)
            {
                case 1:
                    meg = "Email not empty.";
                    break;
                case 2:
                    meg = "Password confirmation is incorrect or less than 6 characters.";
                    break;
                case 3:
                    meg = "Email invalid format.";
                    break;
                case 4:
                    meg = "Email already exists.";
                    break;
                case 5:
                    meg = "Password is incorrect";
                    break;
                case 6:
                    meg = "Token expire";
                    break;
                case 7:
                    meg = "Data not found";
                    break;
                case 8:
                    meg = "";
                    break;
                case 9:
                    meg = "";
                    break;
                default:
                    meg = "System busy.";
                    break;
            }
            return meg;
        }

        public static int LevelStatus(string lv)
        {
            int _lv = 1;
            switch (lv)
            {
                case "2":
                    _lv = 1; break;
                case "4":
                    _lv = 2; break;
                case "8":
                    _lv = 3; break;
                case "16":
                    _lv = 4; break;
                case "32":
                    _lv = 5; break;
                default:
                    _lv = 1; break;
            }
            return _lv;
        }

        public static int WeeksInYear(DateTime time)
        {

            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

    }
}