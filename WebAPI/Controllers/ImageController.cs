using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://makeup4u.herokuapp.com,http://localhost:4200", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/shared/SavedPictures/{imageName}/")]
        public HttpResponseMessage GetAll(string imageName)
        {
            //get server-path of image
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Shared/SavedPictures/") + imageName;
            try
            {
                //by default: image format is jpeg
                ImageFormat format = ImageFormat.Jpeg;
                string formatString = "image/jpeg";
                string end = imageName.Split('.')[1];
                
                //check if image format is png
                if (end.ToLower() == "png")
                {
                    format = ImageFormat.Png;
                    formatString = "image/png";
                }
                //send the image by its format
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, format);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(formatString);

                return result;
            }
             catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
