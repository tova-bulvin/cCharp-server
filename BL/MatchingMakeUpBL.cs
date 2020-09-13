using Entities.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;



namespace BL
{
    public class MatchingMakeUpBL
    {
        public static MatchMakeUpDto MatchRGB(MatchMakeUpDto matchMakeUpDto)
        {
            string matchColors = FindColors(matchMakeUpDto.CompaniesName);//מחזיר רשימת מיקאפ לפי החברות שהתקבלו
            if (matchColors.Length < 2)
                return null;
            MatchMakeUpDto result = GetMakeUp(matchMakeUpDto.Images[0], matchColors);

            return result;
        }

        /// <returns></returns>
        public static MatchMakeUpDto GetMakeUp(string imagePath,string matchColors)
        {
            MatchMakeUpDto matchMakeUpDto = new MatchMakeUpDto();
            try
            {
                WebRequest request = WebRequest.Create("http://makeup4python.herokuapp.com/?image=" + imagePath + "&RGB_numbers="+ matchColors); 
                //WebRequest request = WebRequest.Create("http://localhost:5000/?image=" + imagePath + "&RGB_numbers=" + matchColors);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();  
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                if(responseFromServer.Length>0)//checkif server return response
                { 
                    string idString = responseFromServer.Substring(responseFromServer.LastIndexOf('[')+1);
                    idString=idString.Remove(idString.Length-1);
                    string imageArray = responseFromServer.Remove(responseFromServer.Length - idString.Length-2);
                    List<int> idArray = idString.Split(',').Select(Int32.Parse).ToList(); 
                    matchMakeUpDto.Images = imageArray.TrimStart('[').TrimEnd(']').Split(',');
                    matchMakeUpDto.Images = processImages(matchMakeUpDto.Images);
                    matchMakeUpDto.Details = convertFromIdToProduct(idArray);
                }
                reader.Close();
                response.Close();
                return matchMakeUpDto;
            }
            catch (Exception e)
            {
                List<string> list = new List<string>();
                list.Add(e.Message);
                matchMakeUpDto.Images = list.ToArray();
                return matchMakeUpDto;
            }
        }

       private static string[] processImages(string[] images)
       {
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = images[i].Remove(0, 1);
                images[i] = images[i].Remove(images[i].Length-1,1);
            }
            images[1] = images[1].Remove(0, 1);
            images[2] = images[2].Remove(0, 1);
            return images;
       }

        private static ProductDto[] convertFromIdToProduct(List<int> idArray)
        {
            ProductDto[] products=new ProductDto[3];
            int count = 0;
            foreach(int id in idArray)
            {
                products[count] = new ProductDto();
                products[count] = ProductBL.GetProductsById(id);
                count++;
            }
            return products;
        }

        public static string FindColors(List<string> companies)//מחזירה רשימה של RGB לפי החברות שהתקבלו מהמשתמש 
        {
            List<List<int>> productList = new List<List<int>>();
            foreach (string company in companies)
            {
                productList.AddRange(DAL.ProductDAL.GetRGBProductsByName(company));
            }
            var result = "[";
            foreach (List<int> list in productList)
            {
                result += "[";
                result += string.Join(", ", list);
                result += "],";
            }
            result=result.Remove(result.Length - 1);
            result+= "]";
            return result;
        }
        public static void sendMail(string mail, string massege)
        {
            try
            {
                //SmtpClient client = new SmtpClient();
                //client.Port = 587;
                //client.Host = "smtp.gmail.com";
                //client.EnableSsl = true;
                //client.Timeout = 10000;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential("315171322@mby.co.il", "Student@264");

                //MailMessage mm = new MailMessage("315171322@mby.co.il", mail, "test", "<h1>new message</h1>");
                //mm.IsBodyHtml = true;
                //mm.BodyEncoding = UTF8Encoding.UTF8;
                //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                //client.Send(mm);
                /*string _password = "Student@264";
                string _sender = "315171322@mby.co.il";
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");*/
                string _password="todaD818";
                string _sender= "makeyourmakeup1@gmail.com";
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_sender, _password);
                client.EnableSsl = true;
                client.Credentials = credentials;

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(_sender, mail);
                message.Subject = "your choosen makeup" ;
                message.Body = massege;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
