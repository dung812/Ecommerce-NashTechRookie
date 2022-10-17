using ShoesShop.Domain;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.UI.Models
{
    public class Functions
    {
        //public static string AbsoluteAction(
        //                                    this IUrlHelper url,
        //                                    string actionName,
        //                                    string controllerName,
        //                                    object routeValues = null)
        //{
        //    string scheme = url.ActionContext.HttpContext.Request.Scheme;
        //    return url.Action(actionName, controllerName, routeValues, scheme);
        //}
        public static string AbsoluteAction(string text)
        {
            return "";
        }

        public static void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = "menfashion500@gmail.com";
            var fromEmailDisplayName = "Footwear";
            var fromEmailPassword = "gbjliyjmezubibmc";
            var smtpHost = "smtp.gmail.com";
            var smtpPort = "587";

            bool enabledSsl = true;

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string RandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8]; // Tạo mảng có 8 phần tử
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)]; // random các kí tự trong biến char
            }

            var finalString = new String(stringChars); // Chuyển mảng thành string
            return finalString;
        }

        public static int DiscountedPriceCalulator(int OriginalPrice, int PromotionPercent)
        {
            return OriginalPrice - ((OriginalPrice * PromotionPercent) / 100);
        }

        public static int AverageRatingCalculator(List<CommentProduct> comments)
        {
            var totalComment = comments.Count();
            var SumStarValue = 0;
            var result = 0;
            foreach (var comment in comments)
            {
                SumStarValue += comment.Star;
            }
            result = (int)(SumStarValue / totalComment);

            return result;
        }
    }
}
