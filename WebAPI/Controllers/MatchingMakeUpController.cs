using BL;
using Entities.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://makeup4u.herokuapp.com,http://localhost:4200", headers: "*", methods: "*")]
    public class MatchingMakeUpController : ApiController
    {

        [HttpGet]
        public void sendMail(string mail,string massege)
        {
            MatchingMakeUpBL.sendMail(mail, massege);
            return;
        }
        
        [HttpPost]
        public MatchMakeUpDto MatchRGB(string filename, string companiesName)
        {
            try
            {
                if (companiesName != "")
                {
                    MatchMakeUpDto matchMakeUpDto = new MatchMakeUpDto();

                    //add company list to object
                    matchMakeUpDto.CompaniesName = JsonConvert.DeserializeObject<List<string>>(companiesName);

                    //add image to remote server folders as: shared/SavedPictures
                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~/Shared/SavedPictures/");

                    //remove last imaged from server when there are more 10 files in server
                    string[] files= Directory.GetFiles(serverPath);
                    if (files.Length > 3)
                    {
                        IOrderedEnumerable<string> orders = files.OrderByDescending(d => new FileInfo(d).CreationTime);
                        File.Delete(orders.Last());
                    }

                    //add now-time to image name for get uniqe image name
                    string now = DateTime.Now.ToString().Trim().Replace("/","").Replace(":", "").Replace(" ", "");
                    string[] splitName = filename.Split('.');
                    string lastFilename = splitName[0] + now +'.'+ splitName[1];
                    string fullPath = serverPath + lastFilename;

                    
                    if (HttpContext.Current.Request.Files != null && HttpContext.Current.Request.Files.Count > 0)
                    {
                        var file = HttpContext.Current.Request.Files["img"];
                        file.SaveAs(fullPath);

                        //add image path to list
                        List<string> list = new List<string>();
                        var p = fullPath.Split(new string[] { "Shared" }, StringSplitOptions.None);
                        /*p[0] += "api\\Shared";
                        fullPath = p[0] + p[1];
                        list.Add(fullPath + "/");*/
                        list.Add("http://makeup4userver.somee.com/api/Shared" + p[1]+"/");
                        matchMakeUpDto.Images = list.ToArray();
                        MatchMakeUpDto matchMakeUpDtoRes = MatchingMakeUpBL.MatchRGB(matchMakeUpDto);
                        return matchMakeUpDtoRes;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                MatchMakeUpDto matchMakeUpDto = new MatchMakeUpDto();
                List<string> list = new List<string>();
                list.Add(e.Message);
                list.Add(e.StackTrace);
                matchMakeUpDto.Images = list.ToArray();
                return matchMakeUpDto;
            }
           
            

        }
        
    }
}
